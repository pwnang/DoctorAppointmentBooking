using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Services
{
    /// <summary>
    /// Service interface for managing patients.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>The list of patients.</returns>
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient with the specified ID.</returns>
        Task<Patient?> GetPatientByIdAsync(Guid id);

        /// <summary>
        /// Checks if a patient with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>True if the doctor patient, false otherwise.</returns>
        Task<bool> PatientExistsAsync(Guid id);

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        Task AddPatientAsync(Patient patient);

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="patient">The patient to update.</param>
        Task UpdatePatientAsync(Patient patient);

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        Task DeletePatientAsync(Guid id);
    }
}
