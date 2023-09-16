using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Trendify.DTOs;

namespace Trendify.Interface
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUserDto data, ModelStateDictionary modelState);

        public Task<UserDto> Authentication(string username, string password);

        public Task<UserDto> GetUser(ClaimsPrincipal principal);
    }
}
