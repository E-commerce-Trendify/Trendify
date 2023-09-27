using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;
using Trendify.Data;
using Trendify.Interface;
using Trendify.Models;
using Trendify.Models.Entites;
using Trendify.Services;

namespace Trendify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategory _context;
        private readonly IEmail _email;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IShoppingCart _ShoppingCart;
        private readonly EcommerceDbContext _dbContext;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,
            ICategory context, IEmail email,
            SignInManager<AuthUser> signInManager,
            IShoppingCart shoppingCart,
            EcommerceDbContext dbContext,IConfiguration config
            
            )

        {
            _logger = logger;
            _context = context;
            _email = email;
            _signInManager = signInManager;
            _ShoppingCart = shoppingCart;
           _dbContext = dbContext;
            _config = config;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var allCategoriesandProduct = await _context.GetAllCategories();

            return View(allCategoriesandProduct);
        }
		public IActionResult HomeAdmin()
		{
			return View();
		}
        public IActionResult CartShopping()
        {
            return View();
        }
     
        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Remember(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                CookieOptions cookieOptions = new CookieOptions();

                cookieOptions.Expires = DateTime.Now.AddDays(7);

                HttpContext.Response.Cookies.Append("name", name, cookieOptions);
                return Content("Ok, I saved it");
            }
            return Content("Please provied user name ");


        }
        [Authorize(Roles ="Admin")]
        public IActionResult ThisIsMe()
        {
            string name = HttpContext.Request.Cookies["name"];

            ViewData["name"] = name;
            ViewBag.Name = name;
           
            return View();
        }
        public async Task<IActionResult> SendEmailparches() {

            var user = await _signInManager.UserManager.GetUserAsync(User);
            var email = user.Email;
            // Call your service to add the product to the cart
            
            await _email.SendEmail(email, "The parches", $"<p>{User.Identity.Name} parches is added</p>");
            await _email.SendEmail("a.shaheen20001@gmail.com", $"The user {User.Identity.Name}", $"<p> parches is added</p>");

            return RedirectToAction("index","Home");
        }
        public async Task<IActionResult> GetCartSummaryCart()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            var SummaryCart = _ShoppingCart.GetCartSummaryCart(user);
           return  View(SummaryCart);
        }
        public async Task<IActionResult> CreateOrder()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            var SummaryCart = _ShoppingCart.GetCartSummaryCart(user);
       
            var Orders =  _ShoppingCart.CreatOrder(SummaryCart);
            return Ok("");
        }
        public async Task<IActionResult> ConfirmPayment()
        {
            await CreateOrder();
           await SendEmailparches();

         return View();
        }

        public async Task<IActionResult> Payment()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            var SummaryCart = _ShoppingCart.GetCartSummaryCart(user);


            StripeConfiguration.ApiKey = _config.GetSection("SettingStrip:SecretKey").Get<string>();

            var domain = "https://localhost:44361/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Home/ConfirmPayment",
                CancelUrl = domain + "/",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in SummaryCart.cart.Items)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(item.Price * 100), // 20.50 => 2050
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = item.NameProduct
                        }
                    },
                    Quantity = item.Quantity
                };

                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            var session = service.Create(options);

            var sessionId = session.Id;

            TempData["sessionId"] = sessionId;


            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }



    }
}