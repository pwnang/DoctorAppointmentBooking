using DoctorAppointmentBooking.API.Databases;
using DoctorAppointmentBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.API.Repositories
{
    public class DbPatientRepository : IPatientRepository
    {
        private readonly CustomDatabaseContext _db;

        public DbPatientRepository(CustomDatabaseContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _db.Patients.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Patient?> GetPatientByIdAsync(Guid id)
        {
            return await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <inheritdoc />
        public async Task<bool> PatientExistsAsync(Guid id)
        {
            return await _db.Patients.AnyAsync(p => p.Id == id);
        }

        /// <inheritdoc />
        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
            return patient;
        }

        /// <inheritdoc />
        public async Task<Patient?> UpdatePatientAsync(Patient patient)
        {
            var existingPatient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);
            if (existingPatient != null)
            {
                existingPatient.Name = patient.Name;
                await _db.SaveChangesAsync();
            }
            return existingPatient;
        }

        /// <inheritdoc />
        public async Task DeletePatientAsync(Guid id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                _db.Patients.Remove(patient);
                await _db.SaveChangesAsync();
            }
        }
    }
}
