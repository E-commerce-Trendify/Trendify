using System.ComponentModel.DataAnnotations.Schema;
using Trendify.Models.Entites;

namespace Trendify.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<CartItem> ShoppingCarts { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string userId { get; set; }
        public AuthUser user { get; set; }
      
    }
}
