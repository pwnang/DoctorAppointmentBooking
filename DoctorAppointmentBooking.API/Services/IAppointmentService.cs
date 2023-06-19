using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Services
{
    /// <summary>
    /// Service interface for managing appointments.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment to create.</param>
        /// <returns>The created appointment.</returns>
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);

        /// <summary>
        /// Retrieves the appointments associated with a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>The appointments associated with the specified patient.</returns>
        Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(Guid patientId);

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
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        /// <returns>The updated appointment.</returns>
        Task UpdateAppointmentAsync(Appointment appointment);

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        Task DeleteAppointmentAsync(Guid id);
    }

}
