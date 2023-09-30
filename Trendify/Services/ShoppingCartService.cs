using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using Trendify.Data;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models;
using Trendify.Models.Entites;

namespace Trendify.Services
{
	public class ShoppingCartService : IShoppingCart
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly EcommerceDbContext _context;


        public ShoppingCartService(IHttpContextAccessor httpContextAccessor,EcommerceDbContext context)
		{

			_httpContextAccessor = httpContextAccessor;
			_context= context;

		}

		public ShoppingCart GetCartForUser(string userId)
		 {
           
            // Retrieve the cart data from the user's cookie
            string cartCookieName = $"Cart_{userId}";
			string cartDataJson = _httpContextAccessor.HttpContext.Request.Cookies[cartCookieName];

			// Deserialize the JSON data into a shopping cart object
			if (!string.IsNullOrEmpty(cartDataJson))
			{
				return JsonConvert.DeserializeObject<ShoppingCart>(cartDataJson);
			}

			// If the cart cookie doesn't exist, create a new empty cart
			return new ShoppingCart { Items = new List<CartItem>() };
		}
        public SummaryCartDto GetCartSummaryCart(AuthUser user,OrderInfo info)
        {
			ShoppingCart myCart;
            // Retrieve the cart data from the user's cookie
            string cartCookieName = $"Cart_{user.UserName}";
            string cartDataJson = _httpContextAccessor.HttpContext.Request.Cookies[cartCookieName];

            // Deserialize the JSON data into a shopping cart object
            if (!string.IsNullOrEmpty(cartDataJson))
            {
                myCart = JsonConvert.DeserializeObject<ShoppingCart>(cartDataJson);
            }
			else
			{
                myCart = new ShoppingCart { Items = new List<CartItem>() };
            }

            // If the cart cookie doesn't exist, create a new empty cart

            var Summary = new SummaryCartDto()
			{
				cart = myCart,
				User = user,
				OrderInfo = info
			};
			return Summary;
        }
      
      
        public async Task CreatOrder(SummaryCartDto summrayOrder)
		{
			var Order = new Order()
			{
				ShoppingCarts = summrayOrder.cart.Items,
				userId=summrayOrder.User.Id,	
				Quantity = summrayOrder.cart.NumberCart,
				TotalPrice =summrayOrder.cart.TotalPrice,
				Date = DateTime.Now,
				Address = summrayOrder.OrderInfo.Address,
				City=summrayOrder.OrderInfo.City,
				State =summrayOrder.OrderInfo.State,
				Zip = summrayOrder.OrderInfo.Zip
			};
			await _context.Orders.AddAsync(Order);
			await _context.SaveChangesAsync();

		}
        
        

        public async Task AddToCart(string userId, int productId, int quantity)
		{
			ShoppingCart cart = GetCartForUser(userId);
            cart.TotalPrice = CalculateTotalPrice(cart);
            var product = await _context.Products.FindAsync(productId);
			CartItem existingItem = cart.Items.FirstOrDefault(item => item.ProductID == productId);
			if (existingItem!=null)
			{
				existingItem.Quantity += quantity;

			}
			else
			{
				CartItem newCart = new CartItem() { 
				
					Quantity = quantity,
					ProductID = productId,
					Description =product.Description,
					ImageUrl = product.ImageUrl,
					NameProduct = product.Name,
					Price = product.Price,
					
				};
				cart.Items.Add(newCart);
			}
			
			
			// Implement logic to add the product to the cart
			// For example, check if the product is already in the cart and update the quantity

			// Serialize the updated cart and store it in the user's cookie
			UpdateCartCookie(userId, cart, Get_httpContextAccessor());
		}
		public async Task RemoveShoppingCarts(string userId)
		{
			ShoppingCart cart = GetCartForUser(userId);


			cart.Items.Clear();
			UpdateCartCookie(userId, cart, Get_httpContextAccessor());


        }

            public int GetTotalItemCount(string userId)
        {
            var cart = GetCartForUser(userId);
			////    var Total = cart.Items.Count();
			var Total = 0;
            foreach (var item in cart.Items)
            {
               // Calculate the price for each item and add it to the total
                Total += item.Quantity;
            }
            return Total;
        }
        public decimal GetTotalPrice(string userId)
        {
            var cart = GetCartForUser(userId);
            return cart.Items.Sum(item => item.Price * item.Quantity);
        }
        private decimal CalculateTotalPrice(ShoppingCart cart)
        {
            decimal totalPrice = 0;

            foreach (var item in cart.Items)
            {
                // Calculate the price for each item and add it to the total
                totalPrice += item.Price * item.Quantity;
            }

            return totalPrice;
        }

        public async Task RemoveFromCart(string userId, int productId)
		{
			ShoppingCart cart = GetCartForUser(userId);

			// Implement logic to remove the product from the cart
			var exsistItme = cart.Items.FirstOrDefault(item => item.ProductID == productId);
            if (exsistItme!=null)
			{
				cart.Items.Remove(exsistItme);
                cart.TotalPrice = CalculateTotalPrice(cart);

                UpdateCartCookie(userId, cart, Get_httpContextAccessor());

			}
			// Serialize the updated cart and store it in the user's cookie
		}

        private IHttpContextAccessor Get_httpContextAccessor()
        {
            return _httpContextAccessor;
        }

        private void UpdateCartCookie(string userId, ShoppingCart cart, IHttpContextAccessor _httpContextAccessor)
		{
            cart.TotalPrice = CalculateTotalPrice(cart);

            // Serialize the cart data to JSON
            string cartDataJson = JsonConvert.SerializeObject(cart);

			// Create or update the cart cookie for the user
			string cartCookieName = $"Cart_{userId}";
			CookieOptions cookieOptions = new CookieOptions
			{
				Expires = DateTime.UtcNow.AddDays(7)
			};
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cartCookieName, cartDataJson, cookieOptions);
		}
	}
}
