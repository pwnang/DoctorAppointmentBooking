using DoctorAppointmentBooking.API.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace DoctorAppointmentBooking.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository interface for managing doctor entities.
    /// </summary>
    public interface IDoctorRepository
    {
        /// <summary>
        /// Retrieves a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>The doctor entity.</returns>
        Task<Doctor?> GetDoctorByIdAsync(Guid id);

        /// <summary>
        /// Checks if a doctor with the specified ID exists asynchronously.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>True if a doctor with the specified ID exists; otherwise, false.</returns>
        Task<bool> DoctorExistsAsync(Guid id);

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>A collection of all doctors.</returns>
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        Task AddDoctorAsync(Doctor doctor);

        /// <summary>
        /// Updates an existing doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor to update.</param>
        Task UpdateDoctorAsync(Doctor doctor);

        /// <summary>
        /// Deletes a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        Task DeleteDoctorAsync(Guid id);
    }

}
