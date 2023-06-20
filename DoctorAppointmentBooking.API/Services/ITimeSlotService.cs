using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Services
{
    /// <summary>
    /// Service interface for managing time slot entities.
    /// </summary>
    public interface ITimeSlotService
    {
        /// <summary>
        /// Retrieves a time slot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot.</param>
        /// <returns>The time slot entity.</returns>
        Task<TimeSlot?> GetTimeSlotByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all time slots.
        /// </summary>
        /// <returns>A collection of time slots.</returns>
        Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync();

        /// <summary>
        /// Retrieves all available time slots.
        /// </summary>
        /// <returns>A collection of available time slots.</returns>
        Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsAsync();

        /// <summary>
        /// Retrieves all time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of time slots for the specified doctor.</returns>
        Task<IEnumerable<TimeSlot>> GetTimeSlotsByDoctorAsync(Guid doctorId);

        /// <summary>
        /// Retrieves all available time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of available time slots for the specified doctor.</returns>
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDoctorAsync(Guid doctorId);

        /// <summary>
        /// Retrieves all reserved time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of reserved time slots for the specified doctor.</returns>
        Task<IEnumerable<TimeSlot>> GetDoctorReservedTimeSlotsAsync(Guid doctorId);

        /// <summary>
        /// Adds a new time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to add.</param>
        Task AddTimeSlotAsync(TimeSlot timeSlot);

        /// <summary>
        /// Updates an existing time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to update.</param>
        Task UpdateTimeSlotAsync(TimeSlot timeSlot);

        /// <summary>
        /// Updates multiple time slots in the repository.
        /// </summary>
        /// <param name="timeSlots">The collection of time slots to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateTimeSlotsAsync(IEnumerable<TimeSlot> timeSlots);


        /// <summary>
        /// Deletes a time slot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot to delete.</param>
        Task DeleteTimeSlotAsync(Guid id);
    }
}
