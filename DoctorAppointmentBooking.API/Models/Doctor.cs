namespace DoctorAppointmentBooking.API.Models
{
    /// <summary>
    /// Class representing a doctor.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Unique identifier for the doctor.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the doctor.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
