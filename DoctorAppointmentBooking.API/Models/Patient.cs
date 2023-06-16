namespace DoctorAppointmentBooking.API.Models
{
    /// <summary>
    /// Class representing a patient.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Unique identifier for the patient.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the patient.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
