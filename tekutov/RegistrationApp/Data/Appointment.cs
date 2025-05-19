using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistrationApp.Data
{
    public class Appointment
    {
        [Key]
        public uint id { get; set; }
        public uint patient_id { get; set; }
        public uint doctor_id { get; set; }
        public uint timeslot_id { get; set; }
        public string reason { get; set; }
        public string status { get; set; }

        [ForeignKey("timeslot_id")]
        public Timeslot Timeslot { get; set; }

        [ForeignKey("patient_id")]
        public Patient Patient { get; set; }

        [ForeignKey("doctor_id")]
        public virtual Doctor Doctor { get; set; }
    }
}
