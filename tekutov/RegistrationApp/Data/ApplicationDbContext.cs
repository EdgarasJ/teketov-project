using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RegistrationApp.Data
{


    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Auditlog> Auditlogs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<Visitfinding> Visitfindings { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
    
}
