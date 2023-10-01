using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trendify.DTOs;
using Trendify.Interface;

namespace Trendify.Views.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _services;
        [BindProperty]

        public LoginData users { get; set; }
    
        public LoginModel(IUserService service)
        {
            _services = service;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
               var user = await _services.Authentication(users.Username, users.Password, this.ModelState);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (user != null)
                {
                    foreach (var role in user.Roles)
                    {
                        if (role == "Admin")
                            return RedirectToAction("Index", "Home");
                    }

                }

            return RedirectToAction("Index", "Home");




        }
    }
}
