using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistrationApp.Data
{
    public class Patient
    {
        [Key]
        public uint user_id { get; set; }
        public string id_number { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

        [ForeignKey("user_id")]
        public Profile Profile { get; set; }
    }
}
