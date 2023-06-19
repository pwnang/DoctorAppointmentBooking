using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Services
{
    /// <summary>
    /// Service interface for managing doctors.
    /// </summary>
    public interface IDoctorService
    {
        /// <summary>
        /// Retrieves a list of all doctors.
        /// </summary>
        /// <returns>The list of doctors.</returns>
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        /// <summary>
        /// Retrieves a doctor by their ID.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>The doctor with the specified ID.</returns>
        Task<Doctor?> GetDoctorByIdAsync(Guid id);

        /// <summary>
        /// Checks if a doctor with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>True if the doctor exists, false otherwise.</returns>
        Task<bool> DoctorExistsAsync(Guid id);

        /// <summary>
        /// Adds a new doctor.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        Task AddDoctorAsync(Doctor doctor);

        /// <summary>
        /// Updates an existing doctor.
        /// </summary>
        /// <param name="doctor">The doctor to update.</param>
        Task UpdateDoctorAsync(Doctor doctor);

        /// <summary>
        /// Deletes a doctor by their ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        Task DeleteDoctorAsync(Guid id);
    }
}
