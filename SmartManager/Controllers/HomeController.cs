//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartManager.Models;
using SmartManager.Models.Users;
using SmartManager.Services.Proccessings.Users;

namespace SmartManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserProcessingService userProcessingService;

        public HomeController(IUserProcessingService userProcessingService)
        {
            this.userProcessingService = userProcessingService;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async ValueTask<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await this.userProcessingService.AddUserAsync(user);

                return RedirectToAction("Login");
            }

            return View(user);
        }



        [HttpPost]
        public async ValueTask<IActionResult> Login(string email, string password)
        {
            var user = await this.userProcessingService
                .RetrieveUserByEmailAndPasswordAsync(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

                return RedirectToAction("Welcome");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Неправильный Email или пароль");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
