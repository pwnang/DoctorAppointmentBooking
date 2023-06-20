using Newtonsoft.Json.Converters;

namespace DoctorAppointmentBooking.API.Converters
{
    // https://stackoverflow.com/questions/18635599/specifying-a-custom-datetime-format-when-serializing-with-json-net
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter(string dateTimeFormat)
        {
            base.DateTimeFormat = dateTimeFormat;
        }
    }
}
