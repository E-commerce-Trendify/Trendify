using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public HomeController(ILogger<HomeController> logger,ICategory context, IEmail email, SignInManager<AuthUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _email = email;
            _signInManager = signInManager;
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
        


       
    }
}