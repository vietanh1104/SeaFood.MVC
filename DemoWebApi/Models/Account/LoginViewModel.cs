using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "{0} is required.")]
        public string Password { get; set; } = null!;
        public bool IsRememberMe { get; set; } = false;
    }
}
