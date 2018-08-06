using System;
using System.Data.Entity;
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
    public class BodicesController : Controller
    {
        private BookALookContext db = new BookALookContext();

        // GET: Bodices
        public ActionResult Index()
        {
            return View(db.Bodices.ToList());
        }

        public ActionResult ImageForm(int? id)
        {
            Bodice bodice = db.Bodices.Find(id);
            if (bodice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageVm viewModel = new ImageVm(bodice);
            return View("ImageForm", viewModel);
        }

        public ActionResult SaveImageData(int bodiceId, string imageData)
        {
            try
            {
                Bodice bodice = db.Bodices.FirstOrDefault(b => b.Id == bodiceId);
                if (bodice != null)
                {
                    imageData = imageData.Replace('-', '+');
                    imageData = imageData.Replace('_', '/');
                    imageData = imageData.Replace("data:image/png;base64,", "");
                    imageData = imageData.TrimEnd();
                    imageData = imageData.TrimStart();
                    byte[] newBytes = Convert.FromBase64String(imageData);
                    bodice.ImageData = newBytes;
                    db.Entry(bodice).State = EntityState.Modified;
                    db.SaveChanges();
                    this.AddNotification("Saved image", NotificationType.SUCCESS);
                    return RedirectToAction("BodiceDetailsForm", new { id = bodice.Id });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.AddNotification("Error: " + e.Message, NotificationType.ERROR);
            }
            return RedirectToAction("Index");
        }

        public ActionResult BodiceDetailsForm(int? id)
        {
            if (id == null)
            {
                WeddingGownItemVm emptyViewModel = new WeddingGownItemVm();
                return View(emptyViewModel);
            }
            Bodice bodice = db.Bodices.Find(id);
            WeddingGownItemVm viewModel = new WeddingGownItemVm(bodice);
            if (viewModel.Id == 0)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult SaveBodiceDetails(WeddingGownItemVm viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bodice bodice = db.Bodices.FirstOrDefault(b => b.Id == viewModel.Id);
                    if (bodice == null)
                    {
                        Bodice newBodice = viewModel.BaseWeddingGownItem() as Bodice;
                        db.Entry(newBodice).State = EntityState.Added;
                        db.Bodices.Add(newBodice);
                        db.SaveChanges();
                    }
                    else if (bodice.Id != 0)
                    {
                        bodice.Name = viewModel.Name;
                        bodice.Description = viewModel.Description;
                        bodice.Price = viewModel.Price;
                        db.Entry(bodice).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    this.AddNotification("Saved details", NotificationType.SUCCESS);
                    return RedirectToAction("BodiceDetailsForm", new { id = viewModel.Id });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.AddNotification("Error: " + e.Message, NotificationType.ERROR);
            }
            return RedirectToAction("Index");
        }
    }
}
