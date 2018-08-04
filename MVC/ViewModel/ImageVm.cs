using BookALook.Classes;

namespace MVC.ViewModel
{
    public class ImageVm
    {
        public BaseWeddingGownItem _baseWeddingGownItem;

        public ImageVm(BaseWeddingGownItem baseWeddingGownItem)
        {
            _baseWeddingGownItem = baseWeddingGownItem;

            Id = baseWeddingGownItem.Id;
            Name = baseWeddingGownItem.Name;
            ImageData = baseWeddingGownItem.ImageData;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        public int ItemType
        {
            get
            {
                if (_baseWeddingGownItem is Bodice)
                {
                    return 1;
                }
                if (_baseWeddingGownItem is Skirt)
                {
                    return 2;
                }
                if (_baseWeddingGownItem is Overlay)
                {
                    return 3;
                }

                return -1;
            }
        }
    }
}