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
        private readonly IDoctorService _doctorService;

        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, 
            IPatientService patientService, ITimeSlotService timeSlotService, 
            IDoctorService doctorService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _timeSlotService = timeSlotService;
            _doctorService = doctorService;
            _logger = logger;
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

                SendConfirmation(createdAppointment, timeSlot);

                return Ok(createdAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create appointment: {ex.Message}");
            }
        }

        private void SendConfirmation(Appointment appointment, TimeSlot timeSlot)
        {
            SendPatientConfirmation(appointment, timeSlot);
            SendDoctorConfirmation(appointment, timeSlot);
        }

        private void SendPatientConfirmation(Appointment appointment, TimeSlot timeSlot)
        {
            var dateOnly = timeSlot.Time.ToString("dd/MM/yyyy");
            var timeOnly = timeSlot.Time.ToString("hh:mm tt");

            _logger.LogInformation($@"
                Dear {appointment.PatientName},

                We are pleased to confirm your upcoming appointment with {timeSlot.DoctorName} at our medical center.
            
                Appointment Details:
                Date: {dateOnly}
                Time: {timeOnly}

                If you need to reschedule or cancel your appointment, please let us know at least 24 hours in advance.

                We look forward to seeing you on {dateOnly}. If you have any questions or require further assistance, feel free to contact us.
            ");
        }

        private void SendDoctorConfirmation(Appointment appointment, TimeSlot timeSlot)
        {
            var dateOnly = timeSlot.Time.ToString("dd/MM/yyyy");
            var timeOnly = timeSlot.Time.ToString("hh:mm tt");

            _logger.LogInformation($@"
                Dear {timeSlot.DoctorName},

                You have a new appointment scheduled with {appointment.PatientName} at our medical center.

                Appointment Details:
                Date: {dateOnly}
                Time: {timeOnly}

                Please login to our system if you need to make any changes to the appointment.

                We appreciate your commitment to patient care and look forward to seeing you on {dateOnly}.
            ");
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

                appointmentDto.UpdatedAt = DateTime.Now;

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
        /// Retrieves all appointments associated with a specific doctor.
        /// </summary>
        /// <param name="doctorId">The ID of the doctor.</param>
        /// <returns>A collection of appointments associated with the specified doctor.</returns>
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorUpcomingAppointmentsAsync(Guid doctorId)
        {
            var doctorExists = await _doctorService.DoctorExistsAsync(doctorId);
            if (!doctorExists)
            {
                return NotFound($"Doctor with ID {doctorId} not found.");
            }

            var timeSlots = await _timeSlotService.GetDoctorReservedTimeSlotsAsync(doctorId);
            var appointments = await _appointmentService.GetUpcomingAppointmentsByTimeSlotsAsync(timeSlots
                .ToList()
                .Select(timeSlot => timeSlot.Id));
            
            return Ok(appointments);
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
