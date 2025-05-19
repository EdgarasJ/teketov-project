using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Data.ViewModels
{
    public class ProfileViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your fullname.")]
        public string? fullname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your email address.")]
        public string? email{ get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your telephone.")]
        public string? tel_number { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Your address.")]
        public string? address { get; set; }
    }
}
