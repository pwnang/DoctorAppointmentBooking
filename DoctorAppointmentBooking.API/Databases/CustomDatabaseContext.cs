using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DoctorAppointmentBooking.API.Databases
{
    public class CustomDatabaseContext : DbContext
    {
        public DbSet<Entities.Appointment> Appointments { get; set; }
        public DbSet<Entities.Doctor> Doctors { get; set; }
        public DbSet<Entities.Patient> Patients { get; set; }
        public DbSet<Entities.TimeSlot> TimeSlots { get; set; }

        public CustomDatabaseContext(DbContextOptions<CustomDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
