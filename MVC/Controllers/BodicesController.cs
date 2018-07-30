﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.WebPages;
using BookALook.Classes;
using BookALook.DomainModel;
using BookALook.MVC.ViewModel;

namespace BookALook.MVC.Controllers
{
    public class BodicesController : Controller
    {
        private BookALookContext db = new BookALookContext();

        // GET: Bodices
        public ActionResult Index()
        {
            return View(db.Bodices.ToList());
        }

        // GET: Bodices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodice bodice = db.Bodices.Find(id);
            if (bodice == null)
            {
                return HttpNotFound();
            }
            return View(bodice);
        }

        // GET: Bodices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bodices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price")] Bodice bodice)
        {
            if (ModelState.IsValid)
            {
                db.Bodices.Add(bodice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bodice);
        }

        // GET: Bodices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodice bodice = db.Bodices.Find(id);
            BodiceViewModel viewModel = new BodiceViewModel(bodice);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Bodices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price")] Bodice bodice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodice).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Bodices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodice bodice = db.Bodices.Find(id);
            if (bodice == null)
            {
                return HttpNotFound();
            }
            return View(bodice);
        }

        // POST: Bodices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bodice bodice = db.Bodices.Find(id);
            db.Bodices.Remove(bodice);
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

        public ActionResult Save(BodiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Bodice bodice = db.Bodices.FirstOrDefault(b => b.Id == viewModel.Id);
                if (bodice.Id != 0)
                {
                    bodice.Name = viewModel.Name;
                    bodice.Description = viewModel.Description;
                    bodice.Price = viewModel.Price;
                    if (viewModel.UploadImages != null)
                    {
                        MemoryStream stream = new MemoryStream();
                        viewModel.UploadImages.InputStream.CopyTo(stream);
                        byte[] imageData = stream.ToArray();
                        bodice.ImageData = imageData;
                    }
                    db.Entry(bodice).State = EntityState.Modified;
                    db.SaveChanges();
                }                
            }
            return RedirectToAction("Index");
        }
    }
}