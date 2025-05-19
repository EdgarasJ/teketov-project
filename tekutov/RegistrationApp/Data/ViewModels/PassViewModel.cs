using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Data.ViewModels
{
    public class PassViewModel
    {
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your old password.")]
        public string? old_password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your new password.")]
        public string? new_password { get; set; }
        
    }
}
