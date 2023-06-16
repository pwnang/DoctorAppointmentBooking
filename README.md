# Doctor Appointment Booking

## Doctor Availability:

- As a doctor, I want to be able to list my slots and add new slot
- As a doctor, I want to be able to add new slot
- A single time slot should have the following:
    - Id: Guid
    - Time: Date → 22/02/2023 04:30 pm
    - DoctorId: Guid
    - DoctorName: string
    - IsReserved: bool
    - Cost: Decimal

## Appointment Booking:

- As a patient, I want to be able to view all doctors' available (only) slots
- As a patient, I want to be able to book an appointment on a free slot
- An Appointment should have the following:
    - Id: Guid
    - SlotId: Guid
    - PatientId: Guid
    - PatientName: string
    - ReservedAt: Date

## Appointment Management: (optional)

- As a doctor, I want to be able to view my upcoming appointments.
- As a doctor, I want to be able to mark appointments as completed or cancel them if necessary.

## Data Persistence:

- Use Entity Framework Core (EF Core) as the data access technology to interact
with the database.
- Design and implement the necessary models and database context to store
appointment details and any additional required information.
