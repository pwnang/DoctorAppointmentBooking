using DoctorAppointmentBooking.API.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DoctorAppointmentBooking.API.Entities
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
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy hh:mm tt")]
        public DateTime ReservedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Current status of the appointment.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EStatus Status { get; set; } = EStatus.Scheduled;

        /// <summary>
        /// Timestamp when the appointment was updated.
        /// </summary>
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy hh:mm tt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
