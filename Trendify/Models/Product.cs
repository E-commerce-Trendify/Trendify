using System.ComponentModel.DataAnnotations;

namespace Trendify.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
	}
}
