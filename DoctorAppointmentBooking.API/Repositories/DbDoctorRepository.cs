using DoctorAppointmentBooking.API.Databases;
using DoctorAppointmentBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.API.Repositories
{
    public class DbDoctorRepository : IDoctorRepository
    {
        private readonly CustomDatabaseContext _db;

        public DbDoctorRepository(CustomDatabaseContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();

            return doctor;
        }

        /// <inheritdoc />
        public async Task DeleteDoctorAsync(Guid id)
        {
            var doctor = await _db.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _db.Doctors.Remove(doctor);
                await _db.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task<bool> DoctorExistsAsync(Guid id)
        {
            return await _db.Doctors.AnyAsync(d => d.Id == id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _db.Doctors.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _db.Doctors.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<Doctor?> UpdateDoctorAsync(Doctor doctor)
        {
            var existingDoctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == doctor.Id);
            if (existingDoctor != null)
            {
                existingDoctor.Name = doctor.Name;
                await _db.SaveChangesAsync();
            }
            return existingDoctor;
        }
    }
}
