using Microsoft.AspNetCore.Identity;

namespace Trendify.Models.Entites
{
    public class AuthUser : IdentityUser
    {
        public List<Order> Orders { get; set; }
    }
}
