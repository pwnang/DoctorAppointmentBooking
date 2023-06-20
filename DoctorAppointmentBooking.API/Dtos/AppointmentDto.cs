using DoctorAppointmentBooking.API.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static DoctorAppointmentBooking.API.Entities.Appointment;

namespace DoctorAppointmentBooking.API.Dtos
{
    public class AppointmentDto
    {
        /// <summary>
        /// Unique identifier for the appointment.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Id of the time slot associated with the appointment.
        /// </summary>
        public Guid? SlotId { get; set; }

        /// <summary>
        /// Id of the patient associated with the appointment.
        /// </summary>
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Timestamp when the appointment was reserved.
        /// </summary>
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy hh:mm tt")]
        public DateTime? ReservedAt { get; set; }

        /// <summary>
        /// Current status of the appointment.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EStatus? Status { get; set; }

        /// <summary>
        /// Timestamp when the appointment was updated.
        /// </summary>
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy hh:mm tt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
