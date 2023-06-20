using DoctorAppointmentBooking.API.Databases;
using DoctorAppointmentBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.API.Repositories
{
    public class DbAppointmentRepository : IAppointmentRepository
    {
        private readonly CustomDatabaseContext _db;

        public DbAppointmentRepository(CustomDatabaseContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            appointment.ReservedAt = appointment.ReservedAt.ToUniversalTime();
            appointment.UpdatedAt = appointment.UpdatedAt.ToUniversalTime();

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();

            return appointment;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId)
        {
            return await _db.Appointments.Where(a => a.PatientId == patientId).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _db.Appointments.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            return await _db.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsByTimeSlotsAsync(IEnumerable<Guid> timeSlotIds)
        {
            return await _db.Appointments
                .Where(a => a.Status == Appointment.EStatus.Scheduled && timeSlotIds.Contains(a.SlotId))
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool> AppointmentExistsAsync(Guid id)
        {
            return await _db.Appointments.AnyAsync(a => a.Id == id);
        }

        /// <inheritdoc />
        public async Task<Appointment?> UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await GetAppointmentByIdAsync(appointment.Id);
            if (existingAppointment != null)
            {
                existingAppointment.SlotId = appointment.SlotId;
                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.PatientName = appointment.PatientName;
                existingAppointment.ReservedAt = appointment.ReservedAt.ToUniversalTime();
                existingAppointment.Status = appointment.Status;
                existingAppointment.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();

                return existingAppointment;
            }

            return null;
        }

        /// <inheritdoc />
        public async Task DeleteAppointmentAsync(Guid id)
        {
            var appointment = await GetAppointmentByIdAsync(id);
            if (appointment != null)
            {
                _db.Appointments.Remove(appointment);
                await _db.SaveChangesAsync();
            }
        }
    }
}
