﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SetUpMapsHere.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetUpMapsHere.Controllers
{
    public class UserController : Controller
    {
        private SignInManager<IdentityUser> SignInManager { get; set; }
        private UserManager<IdentityUser> UserManager { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.RoleManager = roleManager;
        }

        [HttpGet("/User/Login")]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpGet("/User/Register")]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost("/User/Login")]
        public async Task<IActionResult> Login(Models.User.Login login)
        {
            var signin = await SignInManager.PasswordSignInAsync(login.Username, login.Password, true, false);
            if (signin.Succeeded)
                return Redirect("/");
            else return Redirect("/User/Login");
        }
        [HttpPost("/User/Register")]
        public async Task<IActionResult> Register(Models.User.Register register)
        {
            
            if (register.Password == register.ConfirmPassword)
            {
                string roleName = "User";
                if (UserManager.Users.Count() == 0)
                {
                    roleName = "Administrator";
                }
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await RoleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName
                    });
                }

                var result = await UserManager.CreateAsync(new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                     
                    UserName = register.Username,
                    Email = register.Email,
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890"
                }, register.Password);

                await UserManager.AddToRoleAsync(UserManager.Users.FirstOrDefault(x => x.UserName == register.Username), roleName);
                if (result.Succeeded)
                    return Redirect("/");
                else return Redirect("/User/Register");
            }
            else return Redirect("/User/Register");
        }

        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}