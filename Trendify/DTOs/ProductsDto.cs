namespace Trendify.DTOs
{
    public class ProductsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryID { get; set; }

    }
}
