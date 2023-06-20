using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Repositories
{
    /// <summary>
    /// Repository interface for managing appointments.
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Adds a new appointment to the repository.
        /// </summary>
        /// <param name="appointment">The appointment to add.</param>
        /// <returns>The added appointment.</returns>
        Task<Appointment> AddAppointmentAsync(Appointment appointment);

        /// <summary>
        /// Retrieves the appointments associated with a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>The appointments associated with the specified patient.</returns>
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId);

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>All appointments.</returns>
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment with the specified ID.</returns>
        Task<Appointment?> GetAppointmentByIdAsync(Guid id);

        /// <summary>
        /// Checks if an appointment with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>True if the appointment exists, false otherwise.</returns>
        Task<bool> AppointmentExistsAsync(Guid id);

        /// <summary>
        /// Updates an existing appointment in the repository.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateAppointmentAsync(Appointment appointment);

        /// <summary>
        /// Deletes an appointment from the repository by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteAppointmentAsync(Guid id);
    }
}
