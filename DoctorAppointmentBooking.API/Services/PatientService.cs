using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Repositories;
using System.Numerics;

namespace DoctorAppointmentBooking.API.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>The list of patients.</returns>
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient with the specified ID.</returns>
        public async Task<Patient?> GetPatientByIdAsync(Guid id)
        {
            return await _patientRepository.GetPatientByIdAsync(id);
        }

        /// <summary>
        /// Checks if a patient with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>True if the patient exists, false otherwise.</returns>
        public async Task<bool> PatientExistsAsync(Guid id)
        {
            return await _patientRepository.PatientExistsAsync(id);
        }

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            var exists = await _patientRepository.PatientExistsAsync(patient.Id);
            if (exists)
            {
                throw new InvalidOperationException($"A patient with ID {patient.Id} already exists.");
            }
            return await _patientRepository.AddPatientAsync(patient);
        }

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="patient">The patient to update.</param>
        public async Task<Patient?> UpdatePatientAsync(Patient patient)
        {
            var exists = await _patientRepository.PatientExistsAsync(patient.Id);
            if (!exists)
            {
                throw new InvalidOperationException($"A patient with ID {patient.Id} does not exists.");
            }
            
            return await _patientRepository.UpdatePatientAsync(patient);
        }

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        public async Task DeletePatientAsync(Guid id)
        {
            await _patientRepository.DeletePatientAsync(id);
        }
    }
}
