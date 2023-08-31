using System.ComponentModel.DataAnnotations;

namespace Trendify.DTOs
{
    public class ProductsDto
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public int CategoryID { get; set; }

    }
}
