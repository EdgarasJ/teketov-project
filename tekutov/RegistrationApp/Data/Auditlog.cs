using System.ComponentModel.DataAnnotations;
namespace RegistrationApp.Data
{
    public class Auditlog
    {
        [Key]
        public uint id { get; set; }
        public uint user_id { get; set; }
        public string Action { get; set; }
        public DateTime time { get; set; }
        public string? info { get; set; }

    }
}
