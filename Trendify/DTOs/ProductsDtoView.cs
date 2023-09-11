using Trendify.Models;

namespace Trendify.DTOs
{
    public class ProductsDtoView
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl {  get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
