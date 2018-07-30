using System.Web;
using BookALook.Classes;

namespace BookALook.MVC.ViewModel
{
    public class BodiceViewModel
    {
        public BodiceViewModel(Bodice bodice)
        {
            Id = bodice.Id;
            Name = bodice.Name;
            Description = bodice.Description;
            Price = bodice.Price;
            ImageData = bodice.ImageData;
        }

        public BodiceViewModel()
        {            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public byte[] ImageData { get; set; }
        public HttpPostedFileBase UploadImages { get; set; }   
        
        public string Preview { get; set; }     
    }
}