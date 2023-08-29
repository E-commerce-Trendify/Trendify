using Trendify.Models;

namespace Trendify.DTOs
{
    public class CategoryDtoView
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
