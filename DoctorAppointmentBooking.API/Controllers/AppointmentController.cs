namespace DoctorAppointmentBooking.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DoctorAppointmentBooking.API.Dtos;
    using DoctorAppointmentBooking.API.Entities;
    using DoctorAppointmentBooking.API.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly ITimeSlotService _timeSlotService;

        public AppointmentController(IAppointmentService appointmentService, 
            IPatientService patientService, ITimeSlotService timeSlotService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _timeSlotService = timeSlotService;
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="patientId">The ID of the patient associated with the appointment.</param>
        /// <param name="slotId">The ID of the time slot to be reserved.</param>
        /// <returns>The created appointment.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentDto appointmentDto)
        {
            try
            {
                // Retrieve the patient by ID
                var patientId = appointmentDto.PatientId ?? Guid.Empty;
                var patient = await _patientService.GetPatientByIdAsync(patientId);
                if (patient == null)
                {
                    return NotFound($"Patient with ID \"{patientId}\" not found.");
                }

                // Retrieve the time slot by ID
                var slotId = appointmentDto.SlotId ?? Guid.Empty;
                var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(slotId);
                if (timeSlot == null)
                {
                    return NotFound($"Time slot with ID \"{slotId}\" not found.");
                }

                if (timeSlot.IsReserved)
                {
                    return BadRequest($"Time slot with ID \"{slotId}\" is already reserved.");
                }

                timeSlot.IsReserved = true;
                await _timeSlotService.UpdateTimeSlotAsync(timeSlot);

                var timestamp = DateTime.Now;
                appointmentDto.UpdatedAt = timestamp;
                appointmentDto.ReservedAt = timestamp;

                // Create the appointment
                var appointment = await SyncAppointmentDtoWithData(appointmentDto, new Appointment());

                var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
                return Ok(createdAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create appointment: {ex.Message}");
            }
        }

        /// <summary>
        /// Synchronizes the data from the provided AppointmentDto with the existing Appointment instance.
        /// </summary>
        /// <param name="appointmentDto">The AppointmentDto containing the data to synchronize.</param>
        /// <param name="appointment">The existing Appointment instance to synchronize with.</param>
        private async Task<Appointment> SyncAppointmentDtoWithData(AppointmentDto appointmentDto, Appointment appointment)
        {
            if (appointmentDto == null) return appointment;

            if (appointmentDto.SlotId.HasValue)
            {
                // Retrieve the time slot by ID
                var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(appointmentDto.SlotId.Value) ?? 
                    throw new ArgumentException("Invalid time slot ID.");
                appointment.SlotId = timeSlot.Id;
            }

            if (appointmentDto.PatientId.HasValue)
            {
                // Retrieve the patient by ID
                var patient = await _patientService.GetPatientByIdAsync(appointmentDto.PatientId.Value) 
                    ?? throw new ArgumentException("Invalid patient ID.");

                appointment.PatientId = patient.Id;
                appointment.PatientName = patient.Name;
            }

            if (appointmentDto.ReservedAt.HasValue)
            {
                appointment.ReservedAt = appointmentDto.ReservedAt.Value;
            }

            if (appointmentDto.Status.HasValue)
                appointment.Status = appointmentDto.Status.Value;

            if (appointmentDto.UpdatedAt.HasValue)
            {
                appointment.UpdatedAt = appointmentDto.UpdatedAt.Value;
            }

            return appointment;
        }


        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>All appointments.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve appointments: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve appointment: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="appointmentDto">The updated appointment data.</param>
        /// <returns>The updated appointment.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, AppointmentDto appointmentDto)
        {
            try
            {
                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound();
                }

                if (appointmentDto.Id.HasValue && id != appointmentDto.Id.Value)
                {
                    return BadRequest("Appointment ID mismatch.");
                }

                await SyncAppointmentDtoWithData(appointmentDto, existingAppointment);

                await _appointmentService.UpdateAppointmentAsync(existingAppointment);
                return Ok(existingAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update appointment: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            try
            {
                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound();
                }

                await _appointmentService.DeleteAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete appointment: {ex.Message}");
            }
        }
    }

}
