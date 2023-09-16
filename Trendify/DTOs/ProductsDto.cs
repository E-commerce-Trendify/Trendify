using System.ComponentModel.DataAnnotations;

namespace Trendify.DTOs
{
    public class ProductsDto
    {

        public int ProductId { get; set; }
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public string? ImageURL { get; set; }

    }
}
