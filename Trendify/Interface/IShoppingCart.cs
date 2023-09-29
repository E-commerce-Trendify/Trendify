using Trendify.DTOs;
using Trendify.Models;
using Trendify.Models.Entites;

namespace Trendify.Interface
{
	public interface IShoppingCart
	{
		ShoppingCart GetCartForUser(string userId);
		Task AddToCart(string userId, int productId, int quantity);
		Task RemoveFromCart(string userId, int productId);

         int GetTotalItemCount(string userId);

        decimal GetTotalPrice(string userId);
		SummaryCartDto GetCartSummaryCart(AuthUser user,OrderInfo info);

		Task CreatOrder(SummaryCartDto summaryCartDto);

	    Task RemoveShoppingCarts(string userId);

    }
}
