using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookALook.Classes;
using BookALook.DomainModel;
using BookALook.MVC.ViewModel;
using MVC.ViewModel;

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

        public ActionResult Image(int? id)
        {
            Overlay overlay = db.Overlays.Find(id);
            if (overlay == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageVm viewModel = new ImageVm(overlay);
            return View("ImageForm", viewModel);
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

        public ActionResult OverlayDetailsForm(int? id)
        {
            if (id == null)
            {
                WeddingGownItemVm emptyViewModel = new WeddingGownItemVm();
                return View(emptyViewModel);
            }
            Overlay overlay = db.Overlays.Find(id);
            WeddingGownItemVm viewModel = new WeddingGownItemVm(overlay);
            if (viewModel.Id == 0)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OverlayDetailsForm([Bind(Include = "Id,Name,Description,Price")] Overlay overlay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overlay).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveOverlayDetails(WeddingGownItemVm viewModel)
        {
            Overlay overlay = db.Overlays.FirstOrDefault(b => b.Id == viewModel.Id);
            if (overlay == null)
            {
                Overlay newOverlay = viewModel.BaseWeddingGownItem() as Overlay;
                db.Entry(newOverlay).State = EntityState.Added;
                db.Overlays.Add(newOverlay);
                db.SaveChanges();
            }
            else if (overlay.Id != 0)
            {
                overlay.Name = viewModel.Name;
                overlay.Description = viewModel.Description;
                overlay.Price = viewModel.Price;
                db.Entry(overlay).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
