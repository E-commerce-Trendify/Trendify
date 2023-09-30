using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models.Entites;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        /// <summary>
        /// Authenticates a user based on their username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A UserDto representing the authenticated user or null if authentication fails.</returns>
		public async Task<UserDto> Authentication(string username, string password,ModelStateDictionary models)
		{

            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            var user = await _userManager.FindByNameAsync(username);

            if (result.Succeeded)
            {
                //// user = await _userManager.FindByNameAsync(username);
                return new UserDto()
                {
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }

            if (result.IsLockedOut)
            {
                models.AddModelError(nameof(username), "Account is locked out");
            }
            else if (result.IsNotAllowed)
            {
                models.AddModelError(nameof(username), "User is not allowed to sign in");
            }
            else
            {
                if (user == null)
                {
                    models.AddModelError("users.Username", "User name is not correct");
                }
                else
                {
                    models.AddModelError("users.Password", "Password is not correct");

                }
            }

            return null;

        }
        /// <summary>
        /// Retrieves a UserDto based on the claims principal.
        /// </summary>
        /// <param name="principal">The claims principal of the user.</param>
        /// <returns>A UserDto representing the user associated with the claims principal.</returns>
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
        /// <summary>
        /// Registers a new user with the provided registration data.
        /// </summary>
        /// <param name="data">The registration data.</param>
        /// <param name="modelState">The model state to add errors in case of registration failure.</param>
        /// <returns>A UserDto representing the registered user or null if registration fails.</returns>
        public async Task<UserDto> Register(RegisterUserDto data, ModelStateDictionary modelState)
        {


            //var existingUserByName = await _userManager.FindByNameAsync(data.Username);
            //if (existingUserByName != null)
            //{
            //    modelState.AddModelError("Username", "Username is already taken");
            //}

            //// Validate Email
            //var existingUserByEmail = await _userManager.FindByEmailAsync(data.Email);
            //if (existingUserByEmail != null)
            //{
            //    modelState.AddModelError("Email", "Email is already taken");
            //}

            //// Validate PhoneNumber
            //var existingUserByPhoneNumber = await _userManager.FindByNameAsync(data.PhoneNumber);
            //if (existingUserByPhoneNumber != null)
            //{
            //    modelState.AddModelError("PhoneNumber", "Phone number is already taken");
            //}

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
                error.Code.Contains("PhoneNumber") ? nameof(data.PhoneNumber) : "";
                modelState.AddModelError("data."+errorKey, error.Description);
            }

            return null;
        }
    }
}
