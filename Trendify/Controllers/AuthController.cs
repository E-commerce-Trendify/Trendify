using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models.Entites;

namespace Trendify.Controllers
{
    public class AuthController : Controller
    {
        private IUserService userService;
        private SignInManager<AuthUser> _signInManager;
        private UserManager<AuthUser> _userManager;

        public AuthController(IUserService service, SignInManager<AuthUser> signInManager, UserManager<AuthUser> userManager)
        {
            userService = service;
            _signInManager = signInManager;       
            _userManager = userManager;
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

        [Authorize(Roles = "Admin")] // Ensure only Admins can access this action.
        public IActionResult UserRoles()
        {
            // Retrieve all users
            var users = _userManager.Users.ToList();

            // Create a list of view models containing user details and roles
            var usersWithRoles = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userModel = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = roles.ToList()
                };
                usersWithRoles.Add(userModel);
            }

            return View(usersWithRoles);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Ensure the admin cannot change their own role
            if (User.Identity.Name.Equals(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Cannot change the role of the currently logged-in admin.");
            }

            // Remove the current roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Add the new role
            await _userManager.AddToRoleAsync(user, newRole);

            return RedirectToAction("UserRoles");
        }

    }
}
