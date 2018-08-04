using System.Web;
using BookALook.Classes;

namespace BookALook.MVC.ViewModel
{
    public class WeddingGownItemVm
    {
        public WeddingGownItemVm(BaseWeddingGownItem baseWeddingGownItem)
        {
            Id = baseWeddingGownItem.Id;
            Name = baseWeddingGownItem.Name;
            Description = baseWeddingGownItem.Description;
            Price = baseWeddingGownItem.Price;
            ImageData = baseWeddingGownItem.ImageData;
        }

        public WeddingGownItemVm()
        {            
        }

        public BaseWeddingGownItem BaseWeddingGownItem()
        {
            BaseWeddingGownItem baseWeddingGownItem = new BaseWeddingGownItem()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                ImageData = this.ImageData
            };
            return baseWeddingGownItem;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public byte[] ImageData { get; set; }
    }
}