﻿using Microsoft.AspNetCore.Mvc;
using Trendify.DTOs;
using Trendify.Interface;

namespace Trendify.Controllers
{
    public class AuthController : Controller
    {
        private IUserService userService;

        public AuthController(IUserService service)
        {
            userService = service;
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


            data.Roles = new List<string>() { "Admin" };
            var user = await userService.Register(data, this.ModelState);

            if (!ModelState.IsValid)
            {
                return Redirect("/");
            }

            return View();
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

    }
}
