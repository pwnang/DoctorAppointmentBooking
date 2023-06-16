namespace DoctorAppointmentBooking.API.Models
{
    /// <summary>
    /// Class representing a time slot.
    /// </summary>
    public class TimeSlot
    {
        /// <summary>
        /// Unique identifier for the time slot.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Start time of the time slot.
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of the doctor associated with the time slot.
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Name of the doctor associated with the time slot.
        /// </summary>
        public string DoctorName { get; set; } = string.Empty;

        /// <summary>
        /// Flag to indicate if the time slot is reserved.
        /// </summary>
        public bool IsReserved { get; set; } = false;

        /// <summary>
        /// Cost for the consulation with the doctor.
        /// </summary>
        public decimal Cost { get; set; } = 5.00m;
    }
}
