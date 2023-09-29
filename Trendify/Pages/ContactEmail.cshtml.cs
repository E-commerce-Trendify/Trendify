using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Trendify.Interface;
using Trendify.Models.Entites;

namespace Trendify.Pages
{
    public class ContactEmailModel : PageModel
    {
        private readonly IEmail _IEmail;
        private readonly SignInManager<AuthUser> _signInManager;
        public ContactEmailModel(IEmail IEmail, SignInManager<AuthUser> signInManager)
        {
            _signInManager = signInManager;
            _IEmail = IEmail;
        }

        [BindProperty]
        public Contact Contacts { get; set; }


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Validation failed, return the page with error messages
                return Page();
            }
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var email = user.Email;
            string Email = email;
            string subject = $"The user {User.Identity.Name} have problem ${Contacts.Subject}";
            string htmContent = $"<p>{Contacts.Description}</p>";

           await _IEmail.SendEmail(Email, subject, htmContent);
            // Redirect to a thank you page or some other action
            return RedirectToAction("index","Home");
        }


    }
    public class Contact
    {

        public string Subject { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string Description { get; set; }
    }
}
