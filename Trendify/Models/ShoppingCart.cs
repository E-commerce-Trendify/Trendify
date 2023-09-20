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

		public int Quantity { get; set; }
		
		public int ProductId { get; set; }

		public Product Product { get; set; }
	}
}
