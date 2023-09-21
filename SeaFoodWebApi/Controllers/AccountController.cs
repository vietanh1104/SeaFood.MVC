using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Application.Command.UserCommand;
using Newtonsoft.Json;
using System.Security.Policy;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SeaFoodWebApi.Controllers
{
    [ApiController]
    [Route("/api/account/")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger; 

        public AccountController(IMediator mediator, ILogger<AccountController> logger, IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login-cookie")]
        [ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> LoginCookie(UserLoginCommand request)
        {
            try
            {
                var user = await _mediator.Send(new UserLoginCommand
                {
                    username = request.username,
                    password = request.password
                });

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
                    IsPersistent = request.rememberPassword,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Problem(detail: "Logging successfully!", statusCode: 200);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while signing in with request = {JsonConvert.SerializeObject(request)}, " +
                    $"message = {ex.Message}");
                return Problem(detail: "Bad request", statusCode: 400);
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginCommand request)
        {
            try
            {
                var user = await _mediator.Send(new UserLoginCommand
                {
                    username = request.username,
                    password = request.password
                });

                var authClaims = new List<Claim>
                {
                    new Claim("userid", user.Id.ToString())
                };

                if (user.IsAdminUser == true)
                {
                    authClaims.Add(new Claim("role", "user"));
                }
                else
                {
                    authClaims.Add(new Claim("role", "admin"));
                }

                var expTime = _configuration.GetValue<int>("JWT:TokenValidityInMinutes");
                var absExpTime = DateTime.Now.AddMinutes(expTime);
                var token = CreateToken(authClaims, absExpTime);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpIn = expTime
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while signing in with request = {JsonConvert.SerializeObject(request)}, " +
                    $"message = {ex.Message}");
                return Problem(detail: "Bad request", statusCode: 400);
            }

        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims, DateTime absExpTime)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                expires: absExpTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        [HttpPost("register")]
		public async Task<IActionResult> Register(UserAddCommand request)
        {
            try
            {
                var user = await _mediator.Send(request);

                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while signing up with request = {JsonConvert.SerializeObject(request)}, " +
                   $"message = {ex.Message}");
                return Problem(detail: "Bad request", statusCode: 400);
            }
            
        }
        [HttpPost("check-authen")]
        [Authorize]
        public IActionResult CheckAuthen(UserAddCommand request)
        {
            return Ok(new { message = "success" });

        }
        [HttpPost("logout-cookie")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Logout()
        {

            if (User.Identity is not null && !User.Identity.IsAuthenticated)
            {
                return Problem(detail: "Bad request", statusCode: 400);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Problem(detail: "Logout successfully", statusCode: 200);
        }
    }
}
