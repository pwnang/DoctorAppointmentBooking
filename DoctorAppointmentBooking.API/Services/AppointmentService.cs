using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Repositories;

namespace DoctorAppointmentBooking.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment to create.</param>
        /// <returns>The created appointment.</returns>
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.AddAppointmentAsync(appointment);
            return appointment;
        }

        /// <summary>
        /// Retrieves the appointments associated with a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>The appointments associated with the specified patient.</returns>
        public async Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(Guid patientId)
        {
            return await _appointmentRepository.GetAppointmentsByPatientAsync(patientId);
        }

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>All appointments.</returns>
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment with the specified ID.</returns>
        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        /// <summary>
        /// Checks if an appointment with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>True if the appointment exists, false otherwise.</returns>
        public async Task<bool> AppointmentExistsAsync(Guid id)
        {
            return await _appointmentRepository.AppointmentExistsAsync(id);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment">The appointment to update.</param>
        /// <returns>The updated appointment.</returns>
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.UpdateAppointmentAsync(appointment);
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        public async Task DeleteAppointmentAsync(Guid id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
        }
    }
}
