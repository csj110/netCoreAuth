using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(true);
            if (user != null)
            {
                //sign user
                var signResult = await _signManager.PasswordSignInAsync(user, password, false, false).ConfigureAwait(true);
                if (signResult.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
            };
            var result = await _userManager.CreateAsync(user, password).ConfigureAwait(true);
            if (result.Succeeded)
            {
                //sign user here
                var signResult = await _signManager.PasswordSignInAsync(user, password, false, false).ConfigureAwait(true);
                if (signResult.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync().ConfigureAwait(true);
            return RedirectToAction("index");
        }

    }
}
