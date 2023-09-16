using System.ComponentModel.DataAnnotations;

namespace Trendify.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Name should consist of  greater than 4 characters.", MinimumLength = 4)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public int NumberProduct { get { return Products.Count(); }}
        public  IEnumerable<Product> Products { get; set; }
    }
}
