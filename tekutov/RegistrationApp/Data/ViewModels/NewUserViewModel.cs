using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Data.ViewModels
{
    public class NewUserViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter user fullname.")]
        public string? fullname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter user email address.")]
        public string? email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter user telephone.")]
        public string? tel_number { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter user address.")]
        public string? address { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter user password.")]
        public string? password { get; set; }
        [Required(ErrorMessage = "Please choose user role.")]
        public uint role_id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter patient identification number.")]
        public string? id_number { get; set; }
        [Required(ErrorMessage = "Please choose doctor category.")]
        public uint category_id { get; set; }
    }
}
