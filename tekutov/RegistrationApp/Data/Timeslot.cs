using System.ComponentModel.DataAnnotations;
namespace RegistrationApp.Data
{
    public class Timeslot
    {
        [Key]
        public uint id { get; set; }
        public uint doctor_id { get; set; }
        public DateTime time { get; set;}
        public string? availability { get; set; }
    }
}
