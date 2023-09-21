using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DemoWebApi.Models.Account
{
    public class RegisterViewModel
    {
        public string? DisplayName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                   ErrorMessage = "The password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one digit, and one special character (@, $, !, %, *, ?, or &)")]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatPassword { get; set; } = null;
        public string? Email { get; set; }
        public string Mobile { get; set; } = null!;
    }
}
