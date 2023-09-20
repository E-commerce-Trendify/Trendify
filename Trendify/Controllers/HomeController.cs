using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trendify.Interface;
using Trendify.Models;

namespace Trendify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategory _context;


        public HomeController(ILogger<HomeController> logger,ICategory context)
        {
            _logger = logger;
            _context = context;
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

       
    }
}