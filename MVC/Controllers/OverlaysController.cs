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
    public class OverlaysController : Controller
    {
        private BookALookContext db = new BookALookContext();

        // GET: Overlays
        public ActionResult Index()
        {
            return View(db.Overlays.ToList());
        }

        // GET: Overlays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Overlay overlay = db.Overlays.Find(id);
            if (overlay == null)
            {
                return HttpNotFound();
            }
            return View(overlay);
        }

        public ActionResult SaveImageData(int overlayId, string imageData)
        {
            Overlay overlay = db.Overlays.FirstOrDefault(b => b.Id == overlayId);
            if (overlay.Id != 0)
            {
                imageData = imageData.Replace('-', '+');
                imageData = imageData.Replace('_', '/');
                imageData = imageData.Replace("data:image/png;base64,", "");
                imageData = imageData.TrimEnd();
                imageData = imageData.TrimStart();
                byte[] newBytes = Convert.FromBase64String(imageData);
                overlay.ImageData = newBytes;
                db.Entry(overlay).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Overlays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Overlays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price")] Overlay overlay)
        {
            if (ModelState.IsValid)
            {
                db.Overlays.Add(overlay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(overlay);
        }

        // GET: Overlays/BodiceDetailsForm/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Overlay overlay = db.Overlays.Find(id);
            OverlayViewModel viewModel = new OverlayViewModel(overlay);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Overlays/BodiceDetailsForm/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price")] Overlay overlay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overlay).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Overlays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Overlay overlay = db.Overlays.Find(id);
            if (overlay == null)
            {
                return HttpNotFound();
            }
            return View(overlay);
        }

        // POST: Overlays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Overlay overlay = db.Overlays.Find(id);
            db.Overlays.Remove(overlay);
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
        public ActionResult Save(OverlayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Overlay overlay = db.Overlays.FirstOrDefault(o => o.Id == viewModel.Id);
                overlay.Name = viewModel.Name;
                overlay.Description = viewModel.Description;
                overlay.Price = viewModel.Price;
                if (viewModel.UploadImages != null)
                {
                    MemoryStream stream = new MemoryStream();
                    viewModel.UploadImages.InputStream.CopyTo(stream);
                    byte[] imageData = stream.ToArray();
                    overlay.ImageData = imageData;
                }
                db.Entry(overlay).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
