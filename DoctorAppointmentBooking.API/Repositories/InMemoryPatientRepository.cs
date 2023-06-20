using DoctorAppointmentBooking.API.Entities;

namespace DoctorAppointmentBooking.API.Repositories
{
    /// <summary>
    /// In-memory repository for managing patient entities.
    /// </summary>
    public class InMemoryPatientRepository : IPatientRepository
    {
        /// <summary>
        /// List of patients stored in memory.
        /// </summary>
        private readonly List<Patient> _patients = new();

        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>The list of patients.</returns>
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await Task.FromResult<IEnumerable<Patient>>(_patients);
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient with the specified ID.</returns>
        public async Task<Patient?> GetPatientByIdAsync(Guid id)
        {
            return await Task.Run(() => _patients.FirstOrDefault(p => p.Id == id));
        }

        /// <summary>
        /// Checks if a patient with the specified ID exists asynchronously.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>True if a patient with the specified ID exists; otherwise, false.</returns>
        public async Task<bool> PatientExistsAsync(Guid id)
        {
            return await Task.FromResult(_patients.Any(d => d.Id == id));
        }

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            await Task.Run(() => _patients.Add(patient));
            return patient;
        }

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="patient">The patient to update.</param>
        public async Task<Patient?> UpdatePatientAsync(Patient patient)
        {
            var existingPatient = await Task.Run(() => _patients.FirstOrDefault(p => p.Id == patient.Id));
            if (existingPatient != null)
            {
                // Update the patient properties
                existingPatient.Name = patient.Name;

                return existingPatient;
            }

            return null;
        }

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        public async Task DeletePatientAsync(Guid id)
        {
            await Task.Run(() =>
            {
                var patient = _patients.FirstOrDefault(p => p.Id == id);
                if (patient != null)
                {
                    _patients.Remove(patient);
                }
            });
        }
    }
}
