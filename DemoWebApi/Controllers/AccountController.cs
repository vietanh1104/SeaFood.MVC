using DemoWebApi.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Application.Command.UserCommand;
using WebApp.Shared.Extensions;

namespace DemoWebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger; 

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                // If the user is already authenticated, redirect them to the dashboard or another page.
                return RedirectToAction("Index", "SeaFood");
            }

            if (ModelState.IsValid)
            {
                var user = await _mediator.Send(new UserLoginCommand
                {
                    username = model.Username,
                    passwordHash = model.Password
                });

				//var token =  JwtHelper.GenerateJwtToken(Guid.NewGuid().ToString() , userName: model.Username, "demo-123123", 120);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                    // Add more claims as needed
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // You can set additional properties like IsPersistent (remember me).
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // If authentication succeeds, you can redirect the user to the desired page.
                return RedirectToAction("Index","SeaFood");
            }

            // If authentication fails, return the view with validation errors.
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = new UserAddCommand
                {
                    DisplayName = model.DisplayName,
                    Username = model.Username,
                    Mobile = model.Mobile,
                    Email = model.Email
                };

                request.PasswordHash = PasswordExtensions.GetPasswordHash(model.Password);

                await _mediator.Send(request);

                return View("RegisterSuccessfully");
            }
            // If authentication fails, return the view with validation errors.
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult RegisterSuccessfully()
        {
            return View();
        }
        
        public IActionResult RegisterFail()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			}
            return View("Login"); // Redirect to the login page or a specific page after logout.
        }
    }
}
