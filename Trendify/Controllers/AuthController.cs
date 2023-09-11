using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models.Entites;

namespace Trendify.Controllers
{
    public class AuthController : Controller
    {
        private IUserService userService;
        private SignInManager<AuthUser> _signInManager;

        public AuthController(IUserService service, SignInManager<AuthUser> signInManager)
        {
            userService = service;
            _signInManager = signInManager;       
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Signup(RegisterUserDto data)
        {
         //   data.Roles = new List<string>() { "Admin" };

            var user = await userService.Register(data, this.ModelState);

            if (!ModelState.IsValid)
            {
                return View();
            }

            return Redirect("/");
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Authenticate(LoginData loginData)
        {

            var user = await userService.Authentication(loginData.Username, loginData.Password);

            if (user == null)
            {
                this.ModelState.AddModelError("InvalidLogin", "Invalid login attempt");

                return RedirectToAction("Index");
            }

            return Redirect("/");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

    }
}
