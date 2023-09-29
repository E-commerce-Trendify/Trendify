using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;

namespace Trendify.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public List<CartItem> Items { get; set; }

        public int NumberCart { get { return Items.Count(); } }
        public decimal TotalPrice { get; set; }

    }

    public class CartItem
	{
		public int CartItemId { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
