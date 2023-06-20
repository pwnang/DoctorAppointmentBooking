using DoctorAppointmentBooking.API.Converters;
using Newtonsoft.Json;

namespace DoctorAppointmentBooking.API.Dtos
{
    /// <summary>
    /// Data Transfer Object for a time slot.
    /// </summary>
    public class TimeSlotDto
    {
        /// <summary>
        /// Unique identifier for the time slot.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Start time of the time slot.
        /// </summary>
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy hh:mm tt")]
        public DateTime? Time { get; set; }

        /// <summary>
        /// Id of the doctor associated with the time slot.
        /// </summary>
        public Guid? DoctorId { get; set; }

        /// <summary>
        /// Flag to indicate if the time slot is reserved.
        /// </summary>
        public bool? IsReserved { get; set; }

        /// <summary>
        /// Cost for the consulation with the doctor.
        /// </summary>
        public decimal? Cost { get; set; }
    }
}
