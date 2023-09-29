using Trendify.Models;
using Trendify.Models.Entites;

namespace Trendify.DTOs
{
    public class SummaryCartDto
    {
        public ShoppingCart cart { get; set; }
        public AuthUser User { get; set; }
        public OrderInfo OrderInfo { get; set; }
    }
}
