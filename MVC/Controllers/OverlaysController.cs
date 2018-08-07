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
using MVC.Extensions;

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

        public ActionResult ImageForm(int? id)
        {
            Overlay overlay = db.Overlays.Find(id);
            if (overlay == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageVm viewModel = new ImageVm(overlay);
            return View("ImageForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveImageData(int id, string imageData)
        {
            try
            {
                Overlay overlay = db.Overlays.FirstOrDefault(b => b.Id == id);
                if (overlay != null)
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
                    this.AddNotification("Saved image", NotificationType.SUCCESS);
                    return RedirectToAction("OverlayDetailsForm", new { id = overlay.Id });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.AddNotification(e.Message, NotificationType.ERROR);
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

        public ActionResult SaveOverlayDetails(WeddingGownItemVm viewModel)
        {
            try
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
                this.AddNotification("Saved details", NotificationType.SUCCESS);
                return RedirectToAction("OverlayDetailsForm", new { id = viewModel.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.AddNotification(e.Message, NotificationType.ERROR);
            }
            return RedirectToAction("Index");
        }
    }
}
