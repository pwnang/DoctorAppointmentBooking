using DoctorAppointmentBooking.API.Repositories;
using DoctorAppointmentBooking.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDoctorRepository, InMemoryDoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddSingleton<ITimeSlotRepository, InMemoryTimeSlotRepository>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();

builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
