using System.Linq;
using System.Web.Mvc;
using BookALook.Classes;
using BookALook.DomainModel;
using BookALook.MVC.ViewModel;

namespace BookALook.MVC.Controllers
{
    public class HomeController : Controller
    {
        private BookALookContext _context = new BookALookContext();

        public ActionResult Index()
        {
            var bodices = _context.Bodices.ToList();
            var skirts = _context.Skirts.ToList();
            var overlays = _context.Overlays.ToList();

            var viewModel = new DressViewModel
            {
                Bodices = bodices,
                Skirts = skirts,
                Overlays = overlays
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult BodiceDetails(int id)
        {            
            WeddingGownItemVm viewModel = new WeddingGownItemVm(_context.Bodices.FirstOrDefault(b => b.Id == id));
            return PartialView("/Views/Shared/PartialBodiceView.cshtml", viewModel);
        }

        public ActionResult SkirtDetails(int id)
        {
            WeddingGownItemVm viewModel = new WeddingGownItemVm(_context.Skirts.FirstOrDefault(b => b.Id == id));
            return PartialView("/Views/Shared/PartialSkirtView.cshtml", viewModel);
        }

        public ActionResult OverlayDetails(int id)
        {
            WeddingGownItemVm viewModel = new WeddingGownItemVm(_context.Overlays.FirstOrDefault(b => b.Id == id));
            return PartialView("/Views/Shared/PartialOverlayView.cshtml", viewModel);
        }
    }
}