using DoctorAppointmentBooking.API.Extensions;
using DoctorAppointmentBooking.API.Repositories;
using DoctorAppointmentBooking.API.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

builder.Services.AddHttpLogging(options =>
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All
);

builder.Services.AddCustomDatabaseServices(builder.Configuration);

//builder.Services.AddSingleton<IDoctorRepository, InMemoryDoctorRepository>();
builder.Services.AddScoped<IDoctorRepository, DbDoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

//builder.Services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
builder.Services.AddScoped<IPatientRepository, DbPatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

//builder.Services.AddSingleton<ITimeSlotRepository, InMemoryTimeSlotRepository>();
builder.Services.AddScoped<ITimeSlotRepository, DbTimeSlotRepository>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();

//builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
builder.Services.AddScoped<IAppointmentRepository, DbAppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();
app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
