﻿using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.API.Entities
{
    /// <summary>
    /// Class representing a doctor.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Unique identifier for the doctor.
        /// </summary>
        [Key] 
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the doctor.
        /// </summary>
        [Required] 
        public string Name { get; set; } = string.Empty;
    }
}
