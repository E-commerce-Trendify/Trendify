using Trendify.Models;

namespace Trendify.Interface
{
	public interface IShoppingCart
	{
		ShoppingCart GetCartForUser(string userId);
		Task AddToCart(string userId, int productId, int quantity);
		Task RemoveFromCart(string userId, int productId);

         int GetTotalItemCount(string userId);

        decimal GetTotalPrice(string userId);
        
    }
}
