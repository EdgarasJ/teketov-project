using System.ComponentModel.DataAnnotations;
namespace RegistrationApp.Data
{
    public class Category
    {
        [Key]
        public uint id { get; set; }
        public string name { get; set; }
    }
}
