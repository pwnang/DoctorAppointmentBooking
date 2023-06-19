using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.API.Entities
{
    /// <summary>
    /// Class representing a patient.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Unique identifier for the patient.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the patient.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
