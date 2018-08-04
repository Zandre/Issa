using BookALook.Classes;

namespace MVC.ViewModel
{
    public class ImageVm
    {
        public ImageVm(Bodice bodice)
        {
            Id = bodice.Id;
            Name = bodice.Name;
            ImageData = bodice.ImageData;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
    }
}