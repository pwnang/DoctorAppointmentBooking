using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Repositories;

namespace DoctorAppointmentBooking.API.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public TimeSlotService(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        /// <summary>
        /// Retrieves a time slot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot.</param>
        /// <returns>The time slot entity.</returns>
        public async Task<TimeSlot?> GetTimeSlotByIdAsync(Guid id)
        {
            return await _timeSlotRepository.GetTimeSlotByIdAsync(id);
        }

        /// <summary>
        /// Retrieves all time slots.
        /// </summary>
        /// <returns>A collection of time slots.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _timeSlotRepository.GetAllTimeSlotsAsync();
        }

        /// <summary>
        /// Retrieves all available time slots.
        /// </summary>
        /// <returns>A collection of available time slots.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsAsync()
        {
            return await _timeSlotRepository.GetAllAvailableTimeSlotsAsync();
        }

        /// <summary>
        /// Retrieves all time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of time slots for the specified doctor.</returns>
        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorAsync(Guid doctorId)
        {
            return await _timeSlotRepository.GetTimeSlotsByDoctorAsync(doctorId);
        }

        /// <summary>
        /// Retrieves all available time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of available time slots for the specified doctor.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDoctorAsync(Guid doctorId)
        {
            return await _timeSlotRepository.GetAvailableTimeSlotsByDoctorAsync(doctorId);
        }

        /// <summary>
        /// Adds a new time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to add.</param>
        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            // Check for time clashes with existing time slots
            var existingTimeSlots = await _timeSlotRepository.GetTimeSlotsByDoctorAsync(timeSlot.DoctorId);
            bool hasTimeClash = existingTimeSlots.Any(t => t.Time == timeSlot.Time);

            if (hasTimeClash)
            {
                throw new ArgumentException("A time slot already exists at the specified time.");
            }

            if (timeSlot.Time < DateTime.Now)
            {
                throw new ArgumentException("The time slot must be in the future.");
            }

            await _timeSlotRepository.AddTimeSlotAsync(timeSlot);
        }

        /// <summary>
        /// Updates an existing time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to update.</param>
        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
        }

        /// <summary>
        /// Updates multiple time slots in the repository.
        /// </summary>
        /// <param name="timeSlots">The collection of time slots to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateTimeSlotsAsync(IEnumerable<TimeSlot> timeSlots)
        {
            foreach (var timeSlot in timeSlots)
            {
                await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
            }
        }

        /// <summary>
        /// Deletes a time slot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot to delete.</param>
        public async Task DeleteTimeSlotAsync(Guid id)
        {
            await _timeSlotRepository.DeleteTimeSlotAsync(id);
        }
    }
}
