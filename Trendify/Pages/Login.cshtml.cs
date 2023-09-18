using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trendify.DTOs;
using Trendify.Interface;

namespace Trendify.Views.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _services;
        public UserDto user { get; set; }
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(IUserService service)
        {
            _services = service;


        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {

            user = await _services.Authentication(UserName, Password);

            foreach(var role in user.Roles) {
                if (role =="Admin" )
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Products", "Customer");

        }
    }
}
