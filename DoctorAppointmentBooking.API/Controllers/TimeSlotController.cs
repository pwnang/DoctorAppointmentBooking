using DoctorAppointmentBooking.API.Dtos;
using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly IDoctorService _doctorService;

        public TimeSlotController(ITimeSlotService timeSlotService, IDoctorService doctorService)
        {
            _timeSlotService = timeSlotService;
            _doctorService = doctorService;
        }

        /// <summary>
        /// Retrieves a time slot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot.</param>
        /// <returns>The time slot.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlot>> GetTimeSlotById(Guid id)
        {
            TimeSlot? timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);

            if (timeSlot == null)
            {
                return NotFound();
            }

            return Ok(timeSlot);
        }

        /// <summary>
        /// Retrieves all time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of time slots for the specified doctor.</returns>
        [HttpGet("doctors/{doctorId}")]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetTimeSlotsByDoctor(Guid doctorId)
        {
            var exists = await _doctorService.DoctorExistsAsync(doctorId);
            if (!exists)
            {
                return NotFound($"Doctor with ID \"{doctorId}\" does not exist.");
            }

            var timeSlots = await _timeSlotService.GetTimeSlotsByDoctorAsync(doctorId);
            return Ok(timeSlots);
        }

        /// <summary>
        /// Retrieves all available time slots for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>A collection of available time slots for the specified doctor.</returns>
        [HttpGet("doctors/{doctorId}/available")]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAvailableTimeSlotsByDoctor(Guid doctorId)
        {
            var exists = await _doctorService.DoctorExistsAsync(doctorId);
            if (!exists)
            {
                return NotFound($"Doctor with ID \"{doctorId}\" does not exist.");
            }

            var availableTimeSlots = await _timeSlotService.GetAvailableTimeSlotsByDoctorAsync(doctorId);
            return Ok(availableTimeSlots);
        }

        /// <summary>
        /// Retrieves all reserved time slots associated with a specific doctor.
        /// </summary>
        /// <param name="doctorId">The ID of the doctor.</param>
        /// <returns>A collection of reserved time slots for the specified doctor.</returns>
        [HttpGet("doctors/{doctorId}/reserved")]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetDoctorReservedTimeSlotsAsync(Guid doctorId)
        {
            var doctorExists = await _doctorService.DoctorExistsAsync(doctorId);
            if (!doctorExists)
            {
                return NotFound($"Doctor with ID {doctorId} not found.");
            }

            var reservedTimeSlots = await _timeSlotService.GetDoctorReservedTimeSlotsAsync(doctorId);
            return Ok(reservedTimeSlots);
        }

        /// <summary>
        /// Adds a new time slot for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <param name="dateTime">The date and time of the time slot.</param>
        /// <returns>The created time slot.</returns>
        [HttpPost("doctors/{doctorId}/add")]
        public async Task<IActionResult> AddTimeSlot(Guid doctorId, [FromBody] TimeSlotDto timeSlotDto)
        {
            var exists = await _doctorService.DoctorExistsAsync(doctorId);
            if (!exists)
            {
                return NotFound($"Doctor with ID \"{doctorId}\" does not exist.");
            }

            timeSlotDto.DoctorId = doctorId;

            // Create a new TimeSlot instance
            var timeSlot = await SyncTimeSlotDtoWithData(timeSlotDto, new TimeSlot());

            try
            {
                await _timeSlotService.AddTimeSlotAsync(timeSlot);
                return Ok($"Time slot \"{timeSlot.Id}\" was added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while adding a time slot: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing time slot.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot.</param>
        /// <param name="timeSlotDto">The time slot data to update.</param>
        /// <returns>The updated time slot.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot(Guid id, TimeSlotDto timeSlotDto)
        {
            // Check if the time slot exists
            var existingTimeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);
            if (existingTimeSlot == null)
            {
                return NotFound();
            }

            // if ID was set in Dto, assume it is for validation purpose since
            // we do not allow user to update existing ID manually
            if (timeSlotDto.Id.HasValue && id != timeSlotDto.Id.Value)
            {
                return BadRequest("Time slot ID mismatch.");
            }

            await SyncTimeSlotDtoWithData(timeSlotDto, existingTimeSlot);

            // Update the time slot
            try
            {
                await _timeSlotService.UpdateTimeSlotAsync(existingTimeSlot);
                return Ok(existingTimeSlot);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating the time slot: {ex.Message}");
            }
        }

        /// <summary>
        /// Synchronizes the properties of a TimeSlotDto with an existing TimeSlot instance.
        /// </summary>
        /// <param name="timeSlotDto">The TimeSlotDto containing the updated values.</param>
        /// <param name="timeSlot">The TimeSlot instance to be updated.</param>
        private async Task<TimeSlot> SyncTimeSlotDtoWithData(TimeSlotDto timeSlotDto, TimeSlot timeSlot)
        {
            if (timeSlotDto == null) return timeSlot;

            // Sync the properties from the DTO to the existing TimeSlot
            if (timeSlotDto.Time.HasValue)
            {
                timeSlot.Time = timeSlotDto.Time.Value;
            }

            if (timeSlotDto.DoctorId.HasValue)
            {
                // Retrieve the doctor by ID
                var doctor = await _doctorService.GetDoctorByIdAsync(timeSlotDto.DoctorId.Value);
                if (doctor == null)
                {
                    throw new ArgumentException("Doctor not found.");
                }

                timeSlot.DoctorId = doctor.Id;
                timeSlot.DoctorName = doctor.Name;
            }

            if (timeSlotDto.IsReserved.HasValue)
            {
                timeSlot.IsReserved = timeSlotDto.IsReserved.Value;
            }

            if (timeSlotDto.Cost.HasValue)
            {
                timeSlot.Cost = timeSlotDto.Cost.Value;
            }

            return timeSlot;
        }

        /// Deletes a time slot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the time slot to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSlot(Guid id)
        {
            // Check if the time slot exists
            var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
            {
                return NotFound();
            }

            try
            {
                await _timeSlotService.DeleteTimeSlotAsync(id);
                return Ok("Time slot deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while deleting the time slot: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all time slots.
        /// </summary>
        /// <returns>A collection of time slots.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAllTimeSlots()
        {
            var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();
            return Ok(timeSlots);
        }

        /// <summary>
        /// Retrieves all available time slots.
        /// </summary>
        /// <returns>A collection of available time slots.</returns>
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAllAvailableTimeSlots()
        {
            IEnumerable<TimeSlot> availableTimeSlots = await _timeSlotService.GetAllAvailableTimeSlotsAsync();
            return Ok(availableTimeSlots);
        }
    }
}
