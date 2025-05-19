using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Data
{
    public class User
    {
        [Key]
        public uint id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public uint role_id { get; set; }

        public ICollection<Patient> Patients { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
