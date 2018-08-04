using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookALook.Classes;
using BookALook.DomainModel;
using BookALook.MVC.ViewModel;

namespace BookALook.MVC.Controllers
{
    public class SkirtsController : Controller
    {
        private BookALookContext db = new BookALookContext();

        // GET: Skirts
        public ActionResult Index()
        {
            return View(db.Skirts.ToList());
        }

        // GET: Skirts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skirt skirt = db.Skirts.Find(id);
            if (skirt == null)
            {
                return HttpNotFound();
            }
            return View(skirt);
        }

        public ActionResult SaveImageData(int skirtId, string imageData)
        {
            Skirt skirt = db.Skirts.FirstOrDefault(b => b.Id == skirtId);
            if (skirt.Id != 0)
            {
                imageData = imageData.Replace('-', '+');
                imageData = imageData.Replace('_', '/');
                imageData = imageData.Replace("data:image/png;base64,", "");
                imageData = imageData.TrimEnd();
                imageData = imageData.TrimStart();
                byte[] newBytes = Convert.FromBase64String(imageData);
                skirt.ImageData = newBytes;
                db.Entry(skirt).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Skirts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skirts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price")] Skirt skirt)
        {
            if (ModelState.IsValid)
            {
                db.Skirts.Add(skirt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skirt);
        }

        // GET: Skirts/BodiceDetailsForm/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skirt skirt = db.Skirts.Find(id);
            SkirtViewModel viewModel = new SkirtViewModel(skirt);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Skirts/BodiceDetailsForm/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price")] Skirt skirt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skirt).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Skirts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skirt skirt = db.Skirts.Find(id);
            if (skirt == null)
            {
                return HttpNotFound();
            }
            return View(skirt);
        }

        // POST: Skirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skirt skirt = db.Skirts.Find(id);
            db.Skirts.Remove(skirt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Save(SkirtViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Skirt skirt = db.Skirts.FirstOrDefault(s => s.Id == viewModel.Id);
                skirt.Name = viewModel.Name;
                skirt.Description = viewModel.Description;
                skirt.Price = viewModel.Price;
                if (viewModel.UploadImages != null)
                {
                    MemoryStream stream = new MemoryStream();
                    viewModel.UploadImages.InputStream.CopyTo(stream);
                    byte[] imageData = stream.ToArray();
                    skirt.ImageData = imageData;
                }
                db.Entry(skirt).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
