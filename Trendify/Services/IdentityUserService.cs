using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models.Entites;

namespace Trendify.Services
{
    public class IdentityUserService : IUserService
    {

        private UserManager<AuthUser> _userManager;
        private SignInManager<AuthUser> _signInManager;


        public IdentityUserService(UserManager<AuthUser> manager, SignInManager<AuthUser> signInManager)
        {
            _userManager = manager;
            _signInManager = signInManager;
        }

        public async Task<UserDto> Authentication(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);

                return new UserDto()
                {
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }

            return null;
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
            public async Task<UserDto> Register(RegisterUserDto data, ModelStateDictionary modelState)
        {
            var user = new AuthUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, data.Roles);

                return new UserDto()
                {
                   Id=user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                };
                /*IList<string> Roles = new List<string>();
                Roles.Add("Admin");
                await _userManager.AddToRolesAsync(user, Roles);
                return new UserDto
                {
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };*/
            }

            foreach (var error in result.Errors) 
            {
                var errorKey = error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                     error.Code.Contains("Email") ? nameof(data.Email) :
                     "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
