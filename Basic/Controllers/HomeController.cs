using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"Bob@fmail.com"),
                new Claim("Grandm.Says","Very nice boi."),
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob ciak"),
                new Claim("DrivingLicense","A+"),
            };
            var grandmaIndentity = new ClaimsIdentity(grandmaClaims, "grandma identity");
            var licenseIndentity = new ClaimsIdentity(licenseClaims, "driver identity");

            var userPriciple = new ClaimsPrincipal(new[] { grandmaIndentity, licenseIndentity });

            HttpContext.SignInAsync(userPriciple);
            return RedirectToAction("Index");
        }
    }
}
