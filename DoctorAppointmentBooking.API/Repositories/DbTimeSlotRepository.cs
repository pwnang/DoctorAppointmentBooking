using DoctorAppointmentBooking.API.Databases;
using DoctorAppointmentBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.API.Repositories
{
    public class DbTimeSlotRepository : ITimeSlotRepository
    {
        private readonly CustomDatabaseContext _db;

        public DbTimeSlotRepository(CustomDatabaseContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<TimeSlot?> GetTimeSlotByIdAsync(Guid id)
        {
            return await _db.TimeSlots.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _db.TimeSlots.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsAsync()
        {
            return await _db.TimeSlots.Where(t => !t.IsReserved).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorAsync(Guid doctorId)
        {
            return await _db.TimeSlots.Where(t => t.DoctorId == doctorId).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDoctorAsync(Guid doctorId)
        {
            return await _db.TimeSlots.Where(t => t.DoctorId == doctorId && !t.IsReserved).ToListAsync();
        }

        /// <inheritdoc />
        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            timeSlot.Time = timeSlot.Time.ToUniversalTime();

            _db.TimeSlots.Add(timeSlot);
            await _db.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            var existingTimeSlot = await GetTimeSlotByIdAsync(timeSlot.Id);
            if (existingTimeSlot != null)
            {
                existingTimeSlot.Time = timeSlot.Time.ToUniversalTime();
                existingTimeSlot.DoctorId = timeSlot.DoctorId;
                existingTimeSlot.DoctorName = timeSlot.DoctorName;
                existingTimeSlot.IsReserved = timeSlot.IsReserved;
                existingTimeSlot.Cost = timeSlot.Cost;
                await _db.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task DeleteTimeSlotAsync(Guid id)
        {
            var timeSlotToRemove = await GetTimeSlotByIdAsync(id);
            if (timeSlotToRemove != null)
            {
                _db.TimeSlots.Remove(timeSlotToRemove);
                await _db.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TimeSlot>> GetDoctorReservedTimeSlotsAsync(Guid doctorId)
        {
            return await _db.TimeSlots.Where(t => t.DoctorId == doctorId && t.IsReserved).ToListAsync();
        }
    }
}
