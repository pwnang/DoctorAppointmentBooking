using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
        public DateTime? Time { get; set; }

        /// <summary>
        /// Gets or sets the format string for the time representation in the time slot.
        /// </summary>
        public string TimeFormat { get; set; } = "dd/MM/yyyy hh:mm tt";

        /// <summary>
        /// Gets or sets the string representation of the time for the time slot.
        /// Setting this property attempts to parse the provided string using the specified time format
        /// and updates the underlying Time property if the parsing is successful.
        /// </summary>
        public string? TimeString 
        { 
            get => Time?.ToString(TimeFormat); 
            set
            {
                // only valid value will update the Time property
                if (string.IsNullOrWhiteSpace(value)) return;
                if (!DateTime.TryParseExact(value, TimeFormat, 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, 
                    out DateTime parsedTime)) return;

                Time = parsedTime;
            }
        }

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
