using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Tymchak_shop.Data;
using Tymchak_shop.Data.Interfaces;
using Tymchak_shop.Data.Models;
using Tymchak_shop.ViewModels;

namespace Tymchak_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllShoes _shoesRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;
        private AppDBContent appDBContent;
        
        public HomeController(AppDBContent appDBContent1, IAllShoes shoesRepository, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IEmailService emailService)
        {
            _shoesRepository = shoesRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            appDBContent = appDBContent1;
        }
        public ViewResult Index()
        {
            var obj = new HomeViewModel
            {
                favouriteShoes = _shoesRepository.getFavShoes
            };
            return View(obj);
        }
        public IActionResult DeleteElem(int id)
        {
            Shoes b = appDBContent.Shoes.Find(id);
            appDBContent.Shoes.Remove(b);
            appDBContent.SaveChanges(); //Turn the Pluralization On. Because in my database(AppDBContent) there is a table named Shoes (plural).
            return RedirectToAction("Secret");
        }
        [Authorize]
        public IActionResult Secret()
        {
            var obj = new HomeViewModel
            {
                favouriteShoes = _shoesRepository.getFavShoes
            };
            return View(obj);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login2()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                if (username != "1234ddfSD" | password != "1234ddfSD")
                {

                    return RedirectToAction("Login2");
                }
                var user1 = new IdentityUser
                {
                    UserName = username,
                    Email = "",
                };

                var result = await _userManager.CreateAsync(user1, password);
                var signInResult1 = await _signInManager.PasswordSignInAsync(user1, password, false, false);

                if (signInResult1.Succeeded)
                {
                    return RedirectToAction("Secret");
                }
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Secret");
            }

            return RedirectToAction("Secret");
            //login functionality
            //var user = await _userManager.FindByNameAsync(username);
            /*if (user != null)
            {
                //sign in
                //var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Secret");
                }
            }

            return RedirectToAction("Login2");*/
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            //register functionality

            var user = new IdentityUser
            {
                UserName = username,
                Email = "",
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                //sign in
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(CheckEmail), "Home", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());
                await _emailService.SendAsync("test@test.com", "email verify", $"<a href=\"{link}\">Verify Email</a>", true);
                return RedirectToAction("EmailVerification");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CheckEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return View();
            }

            return BadRequest();
        }
        public IActionResult EmailVerification() => View();
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
