namespace Trendify.Component
{
	using Microsoft.AspNetCore.Mvc;
	using Trendify.Interface;
	using Trendify.Models;
	public class ShoppingCartViewComponent : ViewComponent
	{
		private readonly IShoppingCart _shoppingCartService;

		public ShoppingCartViewComponent(IShoppingCart shoppingCartService)
		{
			_shoppingCartService = shoppingCartService;
		}

		public IViewComponentResult Invoke()
		{
			// Get the current user's ID
			string userId = User.Identity.Name;

			// Retrieve the shopping cart for the user
			ShoppingCart cart = _shoppingCartService.GetCartForUser(userId);

            // Pass the cart to the view component
            return View(cart);
		}

	}
}
