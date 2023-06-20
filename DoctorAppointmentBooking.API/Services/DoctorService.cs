namespace DoctorAppointmentBooking.API.Services
{
    using DoctorAppointmentBooking.API.Entities;
    using DoctorAppointmentBooking.API.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorService"/> class.
        /// </summary>
        /// <param name="doctorRepository">The doctor repository.</param>
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        /// <summary>
        /// Retrieves a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>The doctor entity.</returns>
        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }

        /// <summary>
        /// Checks if a doctor with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>True if the doctor exists, false otherwise.</returns>
        public async Task<bool> DoctorExistsAsync(Guid id)
        {
            return await _doctorRepository.DoctorExistsAsync(id);
        }

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>A collection of all doctors.</returns>
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }

        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        /// <returns>The added doctor.</returns>
        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            var exists = await _doctorRepository.DoctorExistsAsync(doctor.Id);
            if (exists)
            {
                throw new InvalidOperationException($"A doctor with ID {doctor.Id} already exists.");
            }
            return await _doctorRepository.AddDoctorAsync(doctor);
        }

        /// <summary>
        /// Updates an existing doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor to update.</param>
        /// <returns>The updated doctor or null if doctor was not found.</returns>
        public async Task<Doctor?> UpdateDoctorAsync(Doctor doctor)
        {
            var exists = await _doctorRepository.DoctorExistsAsync(doctor.Id);
            if (!exists)
            {
                throw new InvalidOperationException($"A doctor with ID {doctor.Id} does not exists.");
            }
            return await _doctorRepository.UpdateDoctorAsync(doctor);
        }

        /// <summary>
        /// Deletes a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        public async Task DeleteDoctorAsync(Guid id)
        {
            await _doctorRepository.DeleteDoctorAsync(id);
        }
    }
}
