namespace DoctorAppointmentBooking.API.Models
{
    /// <summary>
    /// Class representing an appointment.
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Enum representing the status of an appointment.
        /// </summary>
        public enum EStatus
        {
            /// <summary>
            /// Appointment is reserved/scheduled.
            /// </summary>
            Scheduled,
            /// <summary>
            /// Appointment is completed.
            /// </summary>
            Completed,
            /// <summary>
            /// Appointment is cancelled.
            /// </summary>
            Cancelled
        }

        /// <summary>
        /// Unique identifier for the appointment.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id of the time slot associated with the appointment.
        /// </summary>
        public Guid SlotId { get; set; }

        /// <summary>
        /// Id of the patient associated with the appointment.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Name of the patient associated with the appointment.
        /// </summary>
        public string PatientName { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the appointment was reserved.
        /// </summary>
        public DateTime ReserverdAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Current status of the appointment.
        /// </summary>
        public EStatus Status { get; set; } = EStatus.Scheduled;

        /// <summary>
        /// Timestamp when the appointment was updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
