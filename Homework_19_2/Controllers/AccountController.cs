using Microsoft.AspNetCore.Mvc;
using Homework_20.Services;
using WebAPIContacts.AuthContactApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Homework_20.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin { ReturnUrl = returnUrl ?? Url.Action("Index", "Home") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountLogin(UserLogin model)
        {
            var result = await _accountService.Login(model);
            if (result != null && result.Token != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.LoginProp),
                new Claim("Token", result.Token)
            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return LocalRedirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Login failed");
            return View(model);
 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountLogout()
        {
            Response.Cookies.Delete("SessionToken");
            await Task.CompletedTask;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountRegister(UserRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = await _accountService.Register(model);
            if (result)
            {
                //await AccountLogin(new UserLogin { 
                //    LoginProp = model.LoginProp,
                //    Password = model.Password
                //});
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Registration failed");
            return View(model);
        }
    }
}
