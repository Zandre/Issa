using System.Web;
using BookALook.Classes;

namespace BookALook.MVC.ViewModel
{
    public class SkirtViewModel
    {
        public SkirtViewModel(Skirt skirt)
        {
            Id = skirt.Id;
            Name = skirt.Name;
            Description = skirt.Description;
            Price = skirt.Price;
            ImageData = skirt.ImageData;
        }

        public SkirtViewModel()
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