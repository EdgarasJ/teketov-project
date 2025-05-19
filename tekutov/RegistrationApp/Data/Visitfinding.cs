using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistrationApp.Data
{
    public class Visitfinding
    {
        [Key]
        public uint appointment_id { get; set; }
        public string findings { get; set; }
        public string prescription { get; set; }
        public string? fallowup { get; set; }

        [ForeignKey("appointment_id")]
        public virtual Appointment Appointment { get; set; }
    }
}
