using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Repositories
{
    public class InMemoryTimeSlotRepository : ITimeSlotRepository
    {
        /// <summary>
        /// In-memory collection of time slots.
        /// </summary>
        private readonly List<TimeSlot> _timeSlots = new();

        /// <summary>
        /// Retrieves a time slot by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot.</param>
        /// <returns>The time slot entity.</returns>
        public async Task<TimeSlot?> GetTimeSlotByIdAsync(Guid id)
        {
            return await Task.FromResult(_timeSlots.FirstOrDefault(t => t.Id == id));
        }

        /// <summary>
        /// Retrieves all time slots.
        /// </summary>
        /// <returns>A collection of time slots.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await Task.FromResult(_timeSlots);
        }

        /// <summary>
        /// Retrieves all available time slots.
        /// </summary>
        /// <returns>A collection of available time slots.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsAsync()
        {
            return await Task.FromResult(_timeSlots.Where(t => !t.IsReserved));
        }

        /// <summary>
        /// Retrieves all time slots for a specific doctor asynchronously.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of time slots for the specified doctor.</returns>
        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorAsync(Guid doctorId)
        {
            return await Task.FromResult(_timeSlots.Where(t => t.DoctorId == doctorId));
        }

        /// <summary>
        /// Retrieves all available time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of available time slots for the specified doctor.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDoctorAsync(Guid doctorId)
        {
            return await Task.FromResult(_timeSlots.Where(t => t.DoctorId == doctorId && !t.IsReserved));
        }

        /// <summary>
        /// Adds a new time slot asynchronously.
        /// </summary>
        /// <param name="timeSlot">The time slot to add.</param>
        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            await Task.Run(() => _timeSlots.Add(timeSlot));
        }

        /// <summary>
        /// Updates an existing time slot asynchronously.
        /// </summary>
        /// <param name="timeSlot">The time slot to update.</param>
        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            var existingTimeSlot = await GetTimeSlotByIdAsync(timeSlot.Id);
            if (existingTimeSlot != null)
            {
                existingTimeSlot.Time = timeSlot.Time;
                existingTimeSlot.DoctorId = timeSlot.DoctorId;
                existingTimeSlot.DoctorName = timeSlot.DoctorName;
                existingTimeSlot.IsReserved = timeSlot.IsReserved;
                existingTimeSlot.Cost = timeSlot.Cost;
            }
        }

        /// <summary>
        /// Deletes a time slot by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot to delete.</param>
        public async Task DeleteTimeSlotAsync(Guid id)
        {
            var timeSlotToRemove = await GetTimeSlotByIdAsync(id);
            if (timeSlotToRemove != null)
            {
                _timeSlots.Remove(timeSlotToRemove);
            }
        }
    }
}
