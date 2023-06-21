# Beginner Microservices Training

## Business Requirements:

Your application should adhere to the following business requirements:

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

### API

- As a doctor, I want to be able to list my slots:

```powershell
### Get all time slots for doctor with {doctorId}
GET /api/timeslot/doctors/{doctorId}

### Get all available time slots for doctor with {doctorId}
GET /api/timeslot/doctors/{doctorId}/available

### Get all reserved time slots for doctor with {doctorId}
GET /api/timeslot/doctors/{doctorId}/reserved
```

- As a doctor, I want to be able to add new slot:

```powershell
### Add time slot for doctor with {doctorId}
POST /api/timeslot/doctors/{doctorId}/add
Content-Type: application/json

{
  "time": "21/06/2023 12:00 PM",
  "cost": 5.00
}
```

## Appointment Booking:

- As a patient, I want to be able to view all doctors' available (only) slots
- As a patient, I want to be able to book an appointment on a free slot
- An Appointment should have the following:
    - Id: Guid
    - SlotId: Guid
    - PatientId: Guid
    - PatientName: string
    - ReservedAt: Date

### API

- As a patient, I want to be able to view all doctors' available (only) slots:

```powershell
### Get all available time slots for any doctor
GET api/timeslot/available
```

- As a patient, I want to be able to book an appointment on a free slot:

```powershell
### Create an appointment
POST /api/appointment
Content-Type: application/json

{
  "slotId": "f6fd0552-6449-4e52-9113-0398e3a2d32e",
  "patientId": "7f671378-f0d2-46a1-bb2e-750b64b2d9f5"
}
```

## Appointment Management: (optional)

- As a doctor, I want to be able to view my upcoming appointments.
- As a doctor, I want to be able to mark appointments as completed or cancel them if necessary.

### API

- As a doctor, I want to be able to view my upcoming appointments:

```powershell
### Get upcoming (not marked completed/cancelled) appointments 
### for doctor with {doctorId}
GET /api/appointment/doctor/{doctorId}
```

- As a doctor, I want to be able to mark appointments as completed or cancel them if necessary:

```powershell
### Update appointment data, in this case, would like to update its 'status' property
### Available status: "Scheduled", "Completed", and "Cancelled"
PUT /api/appointment/{appointmentId}
Content-Type: application/json

{
  "status": "Completed"
}
```

## Data Persistence:

- Use Entity Framework Core (EF Core) as the data access technology to interact
with the database.
- Design and implement the necessary models and database context to store
appointment details and any additional required information.
