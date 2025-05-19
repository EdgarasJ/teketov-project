using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistrationApp.Data
{
    public class Doctor
    {
        [Key]
        public uint user_id { get; set; }
        public uint category_id { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

        [ForeignKey("category_id")]
        public Category Category { get; set; }
    }
}
