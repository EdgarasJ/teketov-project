using System.ComponentModel.DataAnnotations;
namespace RegistrationApp.Data
{
    public class Role
    {
        [Key]
        public uint id { get; set; }
        public string name { get; set; }
    }
}
