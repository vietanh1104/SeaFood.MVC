using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DemoWebApi.Configuration
{
    public static class AuthenticationStartup
    {
        public static IServiceCollection AddAuthenticationStartup(this IServiceCollection services, IConfiguration configuration)
        {
            string secretKey = configuration.GetValue<string>("SecretKey")
                ?? throw new ArgumentException("Missing secret key");

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = configuration["Authentication:CookieName"];
                options.LoginPath = configuration["Authentication:LoginRedirect"];
                options.LogoutPath = configuration["Authentication:LogoutPath"];
                options.AccessDeniedPath = configuration["Authentication:AccessDeniedPath"];
                options.ExpireTimeSpan = TimeSpan.FromMinutes(
                    double.Parse(configuration["Authentication:CookieTimeoutMinutes"] ?? "60") 
                );
                options.SlidingExpiration = bool.Parse(configuration["Authentication:SlidingExpiration"] ?? "60");
            });
			services.AddAuthorization();
			return services;
        }
    }
}
