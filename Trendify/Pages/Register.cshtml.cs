using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using Trendify.DTOs;
using Trendify.Interface;

namespace Trendify.Views.Pages
{

    public class RegisterModel : PageModel
    {
        private readonly IUserService _services;
        [BindProperty]
        public RegisterUserDto data { get; set; }

        public RegisterModel(IUserService service)
        {
            _services = service;
           
            
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() {
            
            data.Roles = new List<string>() { "Customer" };
            await _services.Register(data, this.ModelState);


          return  Redirect("Login");
                         
        }
    }
}
