using DoctorAppointmentBooking.API.Databases;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentBooking.API.Extensions
{
    public static class CustomDatabaseContextExtensions
    {
        public static IServiceCollection AddCustomDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomDatabaseContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });

            return services;
        }
    }
}
