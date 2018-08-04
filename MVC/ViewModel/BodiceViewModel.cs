using System.Web;
using BookALook.Classes;

namespace BookALook.MVC.ViewModel
{
    public class BodiceViewModel
    {
        public BodiceViewModel(BaseWeddingGownItem baseWeddingGownItem)
        {
            Id = baseWeddingGownItem.Id;
            Name = baseWeddingGownItem.Name;
            Description = baseWeddingGownItem.Description;
            Price = baseWeddingGownItem.Price;
        }

        public BodiceViewModel()
        {            
        }

        public Bodice Bodice()
        {
            Bodice bodice = new Bodice()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price
            };
            return bodice;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }      
    }
}