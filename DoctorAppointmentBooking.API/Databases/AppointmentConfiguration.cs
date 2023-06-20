using DoctorAppointmentBooking.API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.API.Databases
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.SlotId).IsRequired();
            builder.Property(a => a.PatientId).IsRequired();
            builder.Property(a => a.PatientName).IsRequired();
            builder.Property(a => a.ReservedAt).IsRequired();
            builder.Property(a => a.Status).IsRequired();
            builder.Property(a => a.UpdatedAt).IsRequired();
        }
    }
}
