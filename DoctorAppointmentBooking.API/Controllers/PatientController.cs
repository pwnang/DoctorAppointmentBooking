﻿using DoctorAppointmentBooking.API.Entities;
using DoctorAppointmentBooking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="patientService">The patient service.</param>
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>The list of patients.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(Guid id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        /// <returns>The created patient entity.</returns>
        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            await _patientService.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="patient">The updated patient data.</param>
        /// <returns>The updated patient.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest("Patient ID mismatch.");
            }

            if (!await _patientService.PatientExistsAsync(id))
            {
                return NotFound();
            }

            await _patientService.UpdatePatientAsync(patient);
            return Ok(patient);
        }

        /// <summary>
        /// Checks if a patient exists with the specified ID.
        /// </summary>
        /// <param name="patientId">The unique identifier of the patient.</param>
        /// <returns>An IActionResult representing the result of the check.</returns>
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CheckPatientExists(Guid id)
        {
            bool patientExists = await _patientService.PatientExistsAsync(id);
            return Ok(patientExists);
        }

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            if (!await _patientService.PatientExistsAsync(id))
            {
                return NotFound();
            }

            await _patientService.DeletePatientAsync(id);

            var response = new
            {
                Message = $"Patient with ID {id} was deleted successfully."
            };

            return Ok(response);
        }
    }
}
