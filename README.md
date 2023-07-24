# Microservices Training

# Intermediate Level Assessment

## Description

You are tasked The objective of this assessment project is to build upon the existing Doctor
Appointment Booking System and incorporate additional concepts such as unit and integration
testing, authorization, logging, and modular monolith architecture.

## Requirements:

Enhance the Doctor Appointment Booking System by incorporating the following additional
features/concepts:

1. **Appointment Confirmation:**
   1. Once a patient schedules an appointment, the system should send a confirmation
      notification to both the patient and the doctor.
   2. The confirmation notification should include the appointment details, such as the
      patient's name, appointment time, and Doctor's name.
   3. For the sake of this assessment, the notification could be just a Log message
2. **Authentication and Authorization:**
   1. Implement an authentication mechanism to secure the application's APIs.
   2. Only authorized users should be able to access the appointment booking and
      management functionality.
3. **Logging:**
   1. Enhance the logging functionality to capture detailed information about the
      system's activities.
   2. Utilize the Serilog logging framework to log in to the console as well as in seq
   3. Include log messages for significant events, errors, and important user actions.
4. **Modular Monolith Architecture:**
   1. Refactor the existing system into a modular monolith architecture
   2. The system should consist of four modules each with diff architecture as follows:
      1. **Booking Module:** to handle patient booking (Clean architecture and DDD)
      2. **Notification Module:** to send notifications once an appointment is
         scheduled (Simplest architecture possible)
      3. **Management Module:** to manage doctor slots (Traditional Layered
         Architecture)
      4. **Authentication Module:** to handle authentication and authorization
         (Simplest architecture possible)

## Deliverables

Modify and push to the same GitHub/Gitlab repo you used

Ensure that your commits are done incrementally and reflect your progress throughout the
development process.

Make regular and meaningful commitments to showcase your implementation approach and
demonstrate how you have broken down the project into smaller steps.

## Evaluation Criteria

Your project will be evaluated based on the following criteria:

1. Correct implementation of all the required business requirements.
2. Code quality, including readability, maintainability, and adherence to best practices.
3. Effective implementation of the business logic and error handling.

Good luck!

#

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

```
### Get all time slots for doctor with {doctorId}
GET /api/timeslot/doctors/{doctorId}

### Get all available time slots for doctor with {doctorId}
GET /api/timeslot/doctors/{doctorId}/available

### Get all reserved time slots for doctor with {doctorId}
GET /api/timeslot/doctors/{doctorId}/reserved

```

- As a doctor, I want to be able to add new slot:

```
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

```
### Get all available time slots for any doctor
GET api/timeslot/available

```

- As a patient, I want to be able to book an appointment on a free slot:

```
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

```
### Get upcoming (not marked completed/cancelled) appointments
### for doctor with {doctorId}
GET /api/appointment/doctor/{doctorId}

```

- As a doctor, I want to be able to mark appointments as completed or cancel them if necessary:

```
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
