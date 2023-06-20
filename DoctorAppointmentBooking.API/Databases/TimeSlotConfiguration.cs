using DoctorAppointmentBooking.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointmentBooking.API.Databases
{
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Time).IsRequired();
            builder.Property(t => t.DoctorId).IsRequired();
            builder.Property(t => t.DoctorName);
            builder.Property(t => t.IsReserved).IsRequired();
            builder.Property(t => t.Cost).IsRequired();
        }
    }
}
