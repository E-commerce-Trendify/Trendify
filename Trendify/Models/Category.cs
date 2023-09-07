namespace Trendify.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberProduct { get { return Products.Count(); }}
        public  IEnumerable<Product> Products { get; set; }
    }
}
