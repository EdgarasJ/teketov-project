using System.Numerics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationApp.Data
{
    public class Profile
    {
        [Key]
        public uint user_id {  get; set; }
        public string name { get; set; }
        public string tel_number { get; set; }
        public string address { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

    }
}
