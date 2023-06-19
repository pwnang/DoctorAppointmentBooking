using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace DoctorAppointmentBooking.API.Controllers
{
    [ApiController]
    [Route("api/timeslots")]
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
            IEnumerable<TimeSlot> timeSlots = await _timeSlotService.GetTimeSlotsByDoctorAsync(doctorId);
            return Ok(timeSlots);
        }

        /// <summary>
        /// Adds a new time slot for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <param name="dateTime">The date and time of the time slot.</param>
        /// <returns>The created time slot.</returns>
        [HttpPost("doctors/{doctorId}/add")]
        public async Task<IActionResult> AddTimeSlot(Guid doctorId, [FromBody] JsonElement json)
        {
            if (!json.TryGetProperty("dateTimeString", out JsonElement dateTimeStringElement))
            {
                return BadRequest("Invalid payload format. The 'dateTimeString' field is required.");
            }

            var dateTimeString = dateTimeStringElement.GetString();

            var dateTimeFormat = "dd/MM/yyyy hh:mm tt";
            if (json.TryGetProperty("dateTimeFormat", out JsonElement dateTimeFormatElement))
            {
                dateTimeFormat = dateTimeFormatElement.GetString();
            }

            if (!DateTime.TryParseExact(dateTimeString, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return BadRequest("Invalid date and time format.");
            }

            // Retrieve the doctor by ID
            var doctor = await _doctorService.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            var cost = 5.00m;
            if (json.TryGetProperty("cost", out JsonElement costElement))
            {
                costElement.TryGetDecimal(out cost);
            }

            // Create a new TimeSlot instance
            var timeSlot = new TimeSlot
            {
                DoctorId = doctorId,
                DoctorName = doctor.Name,
                Time = dateTime,
                Cost = cost
            };

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
    }
}
