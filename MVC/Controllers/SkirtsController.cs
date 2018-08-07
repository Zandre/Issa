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
    public class SkirtsController : Controller
    {
        private BookALookContext db = new BookALookContext();

        public ActionResult Index()
        {
            return View(db.Skirts.ToList());
        }

        public ActionResult ImageForm(int? id)
        {
            Skirt skirt = db.Skirts.Find(id);
            if (skirt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageVm viewModel = new ImageVm(skirt);
            return View("ImageForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveImageData(int id, string imageData)
        {
            try
            {
                Skirt skirt = db.Skirts.FirstOrDefault(b => b.Id == id);
                if (skirt != null)
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
                    this.AddNotification("Saved image", NotificationType.SUCCESS);
                    return RedirectToAction("SkirtDetailsForm", new { id = skirt.Id});
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.AddNotification(e.Message, NotificationType.ERROR);
            }
            return RedirectToAction("Index");
        }

        public ActionResult SkirtDetailsForm(int? id)
        {
            if (id == null)
            {
                WeddingGownItemVm emptyViewModel = new WeddingGownItemVm();
                return View(emptyViewModel);
            }
            Skirt skirt = db.Skirts.Find(id);
            WeddingGownItemVm viewModel = new WeddingGownItemVm(skirt);
            if (viewModel.Id == 0)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }  

        [HttpPost]
        public ActionResult SaveSkirtDetails(WeddingGownItemVm viewModel)
        {
            try
            {
                Skirt skirt = db.Skirts.FirstOrDefault(b => b.Id == viewModel.Id);
                if (skirt == null)
                {
                    Skirt newSkirt = viewModel.BaseWeddingGownItem() as Skirt;
                    db.Entry(newSkirt).State = EntityState.Added;
                    db.Skirts.Add(newSkirt);
                    db.SaveChanges();
                }
                else if (skirt.Id != 0)
                {
                    skirt.Name = viewModel.Name;
                    skirt.Description = viewModel.Description;
                    skirt.Price = viewModel.Price;
                    db.Entry(skirt).State = EntityState.Modified;
                    db.SaveChanges();
                }
                this.AddNotification("Saved details", NotificationType.SUCCESS);
                return RedirectToAction("SkirtDetailsForm", new { id = viewModel.Id });
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
