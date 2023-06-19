using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorController"/> class.
        /// </summary>
        /// <param name="doctorService">The doctor service.</param>
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Retrieves a doctor by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>The doctor entity.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(Guid id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        /// <summary>
        /// Retrieves all doctors.
        /// </summary>
        /// <returns>A collection of all doctors.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Adds a new doctor.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        /// <returns>The created doctor entity.</returns>
        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor(Doctor doctor)
        {
            await _doctorService.AddDoctorAsync(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        /// <summary>
        /// Updates an existing doctor.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to update.</param>
        /// <param name="doctor">The updated doctor entity.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest("Doctor ID mismatch.");
            }

            if (!await _doctorService.DoctorExistsAsync(id))
            {
                return NotFound();
            }

            await _doctorService.UpdateDoctorAsync(doctor);
            return Ok(doctor);
        }

        /// <summary>
        /// Checks if a doctor exists with the specified ID.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>An IActionResult representing the result of the check.</returns>
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CheckDoctorExists(Guid id)
        {
            bool doctorExists = await _doctorService.DoctorExistsAsync(id);
            return Ok(doctorExists);
        }

        /// <summary>
        /// Deletes a doctor by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            if (!await _doctorService.DoctorExistsAsync(id))
            {
                return NotFound();
            }

            await _doctorService.DeleteDoctorAsync(id);

            var response = new
            {
                Message = $"Doctor with {id} was deleted successfully,"
            };

            return Ok(response);
        }
    }
}
