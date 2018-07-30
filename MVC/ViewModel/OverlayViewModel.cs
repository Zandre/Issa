using System.Web;
using BookALook.Classes;

namespace BookALook.MVC.ViewModel
{
    public class OverlayViewModel
    {
        public OverlayViewModel(Overlay overlay)
        {
            Id = overlay.Id;
            Name = overlay.Name;
            Description = overlay.Description;
            Price = overlay.Price;
            ImageData = overlay.ImageData;
        }

        public OverlayViewModel()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public byte[] ImageData { get; set; }
        public HttpPostedFileBase UploadImages { get; set; }
    }
}