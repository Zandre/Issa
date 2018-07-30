using System.Linq;
using System.Web.Mvc;
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
            return PartialView("/Views/Shared/PartialBodiceView.cshtml", _context.Bodices.FirstOrDefault(b => b.Id == id));
        }

        public ActionResult SkirtDetails(int id)
        {
            return PartialView("/Views/Shared/PartialSkirtView.cshtml", _context.Skirts.FirstOrDefault(s => s.Id == id));
        }

        public ActionResult OverlayDetails(int id)
        {
            return PartialView("/Views/Shared/PartialOverlayView.cshtml", _context.Overlays.FirstOrDefault(o => o.Id == id));
        }
    }
}