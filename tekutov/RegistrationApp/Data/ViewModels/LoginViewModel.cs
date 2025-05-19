using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your email address.")]
        public string? email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your password.")]
        public string? password { get; set; }
    }
}
