using DoctorAppointmentBooking.API.Repositories;
using DoctorAppointmentBooking.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDoctorRepository, InMemoryDoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddSingleton<ITimeSlotRepository, InMemoryTimeSlotRepository>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
