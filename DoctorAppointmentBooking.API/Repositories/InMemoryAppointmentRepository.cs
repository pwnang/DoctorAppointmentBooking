using DoctorAppointmentBooking.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentBooking.API.Repositories
{
    public class InMemoryAppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _appointments = new();

        /// <summary>
        /// Adds a new appointment to the repository.
        /// </summary>
        /// <param name="appointment">The appointment to add.</param>
        /// <returns>The added appointment.</returns>
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            _appointments.Add(appointment);

            return await Task.FromResult(appointment);
        }

        /// <summary>
        /// Retrieves all appointments associated with a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>The appointments associated with the specified patient.</returns>
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId)
        {
            return await Task.FromResult(_appointments.Where(a => a.PatientId == patientId));
        }

        /// <summary>
        /// Retrieves all appointments in the repository.
        /// </summary>
        /// <returns>All appointments.</returns>
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await Task.FromResult(_appointments);
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment with the specified ID.</returns>
        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            return await Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));
        }

        /// <summary>
        /// Checks if an appointment with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>True if the appointment exists, false otherwise.</returns>
        public async Task<bool> AppointmentExistsAsync(Guid id)
        {
            return await Task.FromResult(_appointments.Any(a => a.Id == id));
        }

        /// <summary>
        /// Updates an existing appointment in the repository.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await GetAppointmentByIdAsync(appointment.Id);
            if (existingAppointment != null)
            {
                existingAppointment.SlotId = appointment.SlotId;
                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.PatientName = appointment.PatientName;
                existingAppointment.ReservedAt = appointment.ReservedAt;
                existingAppointment.Status = appointment.Status;
                existingAppointment.UpdatedAt = DateTime.Now;
            }
        }

        /// <summary>
        /// Deletes an appointment from the repository.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        public async Task DeleteAppointmentAsync(Guid id)
        {
            var appointment = await GetAppointmentByIdAsync(id);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
            }
        }
    }
}
