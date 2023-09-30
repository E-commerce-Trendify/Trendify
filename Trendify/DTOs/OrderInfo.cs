using Stripe;

namespace Trendify.DTOs
{
    public class OrderInfo
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

    }
}
