namespace DoctorAppointmentBooking.API.Repositories
{
    using DoctorAppointmentBooking.API.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// In-memory implementation of the doctor repository.
    /// </summary>
    public class InMemoryDoctorRepository : IDoctorRepository
    {
        // Stores the collection of doctors in memory
        private readonly List<Doctor> _doctors;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDoctorRepository"/> class.
        /// </summary>
        public InMemoryDoctorRepository()
        {
            _doctors = new List<Doctor>();
        }

        /// <summary>
        /// Retrieves a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>The doctor entity.</returns>
        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await Task.Run(() => _doctors.FirstOrDefault(d => d.Id == id));
        }

        /// <summary>
        /// Checks if a doctor with the specified ID exists asynchronously.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>True if a doctor with the specified ID exists; otherwise, false.</returns>
        public Task<bool> DoctorExistsAsync(Guid id)
        {
            return Task.FromResult(_doctors.Any(d => d.Id == id));
        }

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>A collection of all doctors.</returns>
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await Task.Run(() => _doctors.ToList());
        }

        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await Task.Run(() => _doctors.Add(doctor));
        }

        /// <summary>
        /// Updates an existing doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor to update.</param>
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await Task.Run(() =>
            {
                var existingDoctor = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
                if (existingDoctor != null)
                {
                    existingDoctor.Name = doctor.Name;
                }
            });
        }

        /// <summary>
        /// Deletes a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        public async Task DeleteDoctorAsync(Guid id)
        {
            await Task.Run(() =>
            {
                var doctorToDelete = _doctors.FirstOrDefault(d => d.Id == id);
                if (doctorToDelete != null)
                {
                    _doctors.Remove(doctorToDelete);
                }
            });
        }
    }
}
