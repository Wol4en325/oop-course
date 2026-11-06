using System.Globalization;
using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Interfaces;
using ClinicApp.Managers;

Clinic clinic = new Clinic("City Health Clinic", "Main St. 12");

bool running = true;

while (running)
{
    Console.WriteLine("\n==============================================");
    Console.WriteLine("              CITY HEALTH CLINIC");
    Console.WriteLine("==============================================");
    Console.WriteLine("1. Patients       - registration, search");
    Console.WriteLine("2. Doctors        - staff, schedule");
    Console.WriteLine("3. Appointments   - visits, cancellations");
    Console.WriteLine("4. Medical records - diagnoses, prescriptions");
    Console.WriteLine("5. Billing        - payments, debts");
    Console.WriteLine("6. Report         - overall statistics");
    Console.WriteLine("7. Waiting queue  - patient queue");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                PatientsMenu(clinic);
                break;
            case "2":
                DoctorsMenu(clinic);
                break;
            case "3":
                AppointmentsMenu(clinic);
                break;
            case "4":
                MedicalRecordsMenu(clinic);
                break;
            case "5":
                BillingMenu(clinic);
                break;
            case "6":
                ReportMenu(clinic);
                break;
            case "7":
                WaitingQueueMenu(clinic);
                break;
            case "0":
                running = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
    catch (ArgumentOutOfRangeException e)
    {
        Console.WriteLine("Error: " + e.Message);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine("Error: " + e.Message);
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: Invalid input format.");
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: " + e.Message);
    }
}

void PatientsMenu(Clinic clinic)
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.WriteLine("\n--- Patients ---");
        Console.WriteLine("1. Add patient");
        Console.WriteLine("2. Show patients");
        Console.WriteLine("0. Back");
        Console.Write("Choose option: ");

        switch (Console.ReadLine())
        {
            case "1":
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Last name: ");
                string lastName = Console.ReadLine() ?? "";

                Console.Write("Date of birth (yyyy-MM-dd): ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine() ?? "");

                Console.WriteLine("Blood types: " + string.Join(", ", Enum.GetNames<BloodType>()));
                Console.Write("Blood type: ");
                BloodType bloodType = Enum.Parse<BloodType>(Console.ReadLine() ?? "", true);

                Console.Write("Phone (10 digits): ");
                string phone = Console.ReadLine() ?? "";

                Patient patient = new Patient(firstName, lastName, dateOfBirth, bloodType, phone);
                clinic.Patients.Add(patient);

                Console.WriteLine("Patient added. ID: " + patient.Id);
                break;
            }
            case "2":
                for (int i = 0; i < clinic.Patients.Count; i++)
                    Console.WriteLine(clinic.Patients[i]);
                break;
            case "7":
                WaitingQueueMenu(clinic);
                break;
            case "0":
                menuRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
}

void DoctorsMenu(Clinic clinic)
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.WriteLine("\n--- Doctors ---");
        Console.WriteLine("1. Add doctor");
        Console.WriteLine("2. Show doctors");
        Console.WriteLine("5. Show available hours");
        Console.WriteLine("0. Back");
        Console.Write("Choose option: ");

        switch (Console.ReadLine())
        {
            case "1":
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Last name: ");
                string lastName = Console.ReadLine() ?? "";

                Console.WriteLine("Specialities: " + string.Join(", ", Enum.GetNames<Speciality>()));
                Console.Write("Speciality: ");
                Speciality speciality = Enum.Parse<Speciality>(Console.ReadLine() ?? "", true);

                Console.Write("License number: ");
                string licenseNumber = Console.ReadLine() ?? "";

                Console.Write("Phone (10 digits): ");
                string phone = Console.ReadLine() ?? "";

                Console.Write("Work start hour (0-23): ");
                int start = int.Parse(Console.ReadLine() ?? "");

                Console.Write("Work end hour (1-24): ");
                int end = int.Parse(Console.ReadLine() ?? "");

                WorkSchedule schedule = new WorkSchedule(start, end);
                Doctor doctor = new Doctor(firstName, lastName, speciality, licenseNumber, phone);
                doctor.Schedule = schedule;
                clinic.Doctors.Add(doctor);

                Console.WriteLine("Doctor added. ID: " + doctor.Id);
                break;
            }
            case "2":
                for (int i = 0; i < clinic.Doctors.Count; i++)
                    Console.WriteLine(clinic.Doctors[i]);
                break;
            case "5":
            {
                Console.Write("Doctor ID: ");
                if (!int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    Console.WriteLine("Invalid doctor ID.");
                    break;
                }

                Doctor? doctor = clinic.Doctors.FindById(doctorId);

                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                    break;
                }

                Console.Write("Date (dd.MM.yyyy): ");
                string dateText = Console.ReadLine() ?? "";

                if (!DateTime.TryParseExact(dateText, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    Console.WriteLine("Invalid date. Use dd.MM.yyyy.");
                    break;
                }

                Console.Write("Number of slots: ");
                if (!int.TryParse(Console.ReadLine(), out int slotCount))
                {
                    Console.WriteLine("Invalid number of slots.");
                    break;
                }

                try
                {
                    ISchedulable schedulable = doctor;
                    DateTime[] slots = schedulable.GetAvailableSlots(date, slotCount);

                    Console.WriteLine("Available hours:");
                    foreach (DateTime slot in slots)
                        Console.WriteLine(slot.ToString("HH:mm"));
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                break;
            }
            case "7":
                WaitingQueueMenu(clinic);
                break;
            case "0":
                menuRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
}

void AppointmentsMenu(Clinic clinic)
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.WriteLine("\n--- Appointments ---");
        Console.WriteLine("1. Make appointment");
        Console.WriteLine("2. Show appointments");
        Console.WriteLine("8. Cancel all appointments for a patient");
        Console.WriteLine("9. Filter appointments by type");
        Console.WriteLine("0. Back");
        Console.Write("Choose option: ");

        switch (Console.ReadLine())
        {
            case "1":
            {
                Console.Write("Patient ID: ");
                int patientId = int.Parse(Console.ReadLine() ?? "");

                Console.Write("Doctor ID: ");
                int doctorId = int.Parse(Console.ReadLine() ?? "");

                if (clinic.Patients.FindById(patientId) == null ||
                    clinic.Doctors.FindById(doctorId) == null)
                {
                    Console.WriteLine("Patient or doctor was not found.");
                    break;
                }

                Console.Write("Appointment date and time (yyyy-MM-dd HH:mm): ");
                DateTime scheduledAt = DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Duration in minutes: ");
                int duration = int.Parse(Console.ReadLine() ?? "");

                Console.WriteLine("Appointment types:");
                Console.WriteLine("1. Regular");
                Console.WriteLine("2. Urgent");
                Console.WriteLine("3. Specialist");
                Console.Write("Choose type: ");
                string appointmentType = Console.ReadLine() ?? "";

                Appointment appointment;

                switch (appointmentType)
                {
                    case "2":
                        Console.Write("Urgency reason: ");
                        string urgencyNote = Console.ReadLine() ?? "";
                        appointment = clinic.Appointments.BookUrgent(patientId, doctorId, scheduledAt, urgencyNote, duration);
                        break;

                    case "3":
                        Console.Write("Consultation topic: ");
                        string topic = Console.ReadLine() ?? "";
                        appointment = clinic.Appointments.BookSpecialist(patientId, doctorId, scheduledAt, topic, duration);
                        break;

                    default:
                        appointment = clinic.Appointments.Book(patientId, doctorId, scheduledAt, duration);
                        break;
                }

                Console.Write("Is this a VIP patient? (y/n): ");
                string vipChoice = Console.ReadLine() ?? "";

                if (vipChoice.ToLower() == "y")
                {
                    appointment.DiscountMultiplier = 0.8m;
                }

                Console.WriteLine("Appointment added. ID: " + appointment.Id);
                Console.WriteLine(appointment);

                if (appointment is UrgentAppointment urgentAppointment)
                {
                    Appointment baseAppointment = urgentAppointment;
                    Console.WriteLine("Priority through Appointment: " + baseAppointment.GetPriority());
                    Console.WriteLine("Priority through UrgentAppointment: " + urgentAppointment.GetPriority());
                }
                break;
            }
            case "2":
                for (int i = 0; i < clinic.Appointments.Count; i++)
                    Console.WriteLine(clinic.Appointments[i]);
                break;

            case "9":
            {
                Console.WriteLine("1. Regular appointments");
                Console.WriteLine("2. Urgent appointments");
                Console.WriteLine("3. Specialist appointments");
                Console.Write("Choose type: ");

                string type = Console.ReadLine() ?? "";
                Appointment[] filteredAppointments = Array.Empty<Appointment>();

                switch (type)
                {
                    case "1":
                        filteredAppointments = clinic.Appointments.GetRegular();
                        break;
                    case "2":
                        filteredAppointments = clinic.Appointments.GetUrgent();
                        break;
                    case "3":
                        filteredAppointments = clinic.Appointments.GetSpecialist();
                        break;
                    default:
                        Console.WriteLine("Unknown appointment type.");
                        break;
                }

                if (type == "1" || type == "2" || type == "3")
                {
                    if (filteredAppointments.Length == 0)
                    {
                        Console.WriteLine("No appointments found.");
                    }
                    else
                    {
                        foreach (Appointment item in filteredAppointments)
                        {
                            clinic.Appointments.DisplayAppointment(item);
                        }
                    }
                }

                break;
            }
            case "8":
            {
                Console.Write("Patient ID: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid patient ID.");
                    break;
                }

                Appointment[] appointments = clinic.Appointments.GetByPatient(patientId);

                Console.Write("Cancellation reason (press Enter for none): ");
                string reason = Console.ReadLine() ?? "";

                int cancelled = AppointmentManager.CancelAll(appointments, reason);
                Console.WriteLine("Cancelled appointments: " + cancelled);
                break;
            }
            case "7":
                WaitingQueueMenu(clinic);
                break;
            case "0":
                menuRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
}

void MedicalRecordsMenu(Clinic clinic)
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.WriteLine("\n--- Medical Records ---");
        Console.WriteLine("1. Add diagnosis");
        Console.WriteLine("2. Add lab result");
        Console.WriteLine("3. Add prescription");
        Console.WriteLine("4. Show all medical records");
        Console.WriteLine("5. Show medical records by type");
        Console.WriteLine("6. Show medical records statistics");
        Console.WriteLine("0. Back");
        Console.Write("Choose option: ");

        switch (Console.ReadLine())
        {
            case "1":
            {
                if (!TryGetRecordPeople(clinic, out int patientId, out int doctorId))
                    break;

                Console.Write("Date (yyyy-MM-dd): ");
                DateTime date = DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Diagnosis code: ");
                string code = Console.ReadLine() ?? "";

                Console.Write("Description: ");
                string description = Console.ReadLine() ?? "";

                Console.Write("Is chronic? (y/n): ");
                bool isChronic = (Console.ReadLine() ?? "").Trim().Equals("y", StringComparison.OrdinalIgnoreCase);

                Diagnosis diagnosis = new Diagnosis(patientId, doctorId, date, code, description, isChronic);
                clinic.MedicalRecords.AddRecord(diagnosis);

                Console.WriteLine("Diagnosis added. ID: " + diagnosis.Id);
                break;
            }
            case "2":
            {
                if (!TryGetRecordPeople(clinic, out int patientId, out int doctorId))
                    break;

                Console.Write("Date (yyyy-MM-dd): ");
                DateTime date = DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Test name: ");
                string testName = Console.ReadLine() ?? "";

                Console.Write("Result: ");
                string result = Console.ReadLine() ?? "";

                Console.Write("Unit (optional): ");
                string unit = Console.ReadLine() ?? "";

                LabResult labResult = new LabResult(patientId, doctorId, date, testName, result, unit);
                clinic.MedicalRecords.AddRecord(labResult);

                Console.WriteLine("Lab result added. ID: " + labResult.Id);
                break;
            }
            case "3":
            {
                if (!TryGetRecordPeople(clinic, out int patientId, out int doctorId))
                    break;

                Console.Write("Date (yyyy-MM-dd): ");
                DateTime date = DateTime.Parse(Console.ReadLine() ?? "");

                Console.Write("Medication name: ");
                string medication = Console.ReadLine() ?? "";

                Console.Write("Instructions: ");
                string instructions = Console.ReadLine() ?? "";

                Console.Write("End date (yyyy-MM-dd, empty if none): ");
                string endDateText = Console.ReadLine() ?? "";
                DateTime? endDate = string.IsNullOrWhiteSpace(endDateText)
                    ? null
                    : DateTime.Parse(endDateText);

                Prescription prescription = new Prescription(
                    patientId, doctorId, date, medication, instructions, endDate);

                clinic.MedicalRecords.AddRecord(prescription);

                Console.WriteLine("Prescription added. ID: " + prescription.Id);
                break;
            }
            case "4":
                clinic.MedicalRecords.DisplayAllRecords();
                break;
            case "5":
            {
                Console.WriteLine("Types: Diagnosis, Lab Result, Prescription");
                Console.Write("Enter record type: ");
                string recordType = Console.ReadLine() ?? "";
                clinic.MedicalRecords.DisplayRecordsByType(recordType);
                break;
            }
            case "6":
                ShowMedicalRecordStatistics(clinic);
                break;
            case "7":
                WaitingQueueMenu(clinic);
                break;
            case "0":
                menuRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
}

bool TryGetRecordPeople(Clinic clinic, out int patientId, out int doctorId)
{
    Console.Write("Patient ID: ");
    if (!int.TryParse(Console.ReadLine(), out patientId))
    {
        Console.WriteLine("Invalid patient ID.");
        doctorId = 0;
        return false;
    }

    Console.Write("Doctor ID: ");
    if (!int.TryParse(Console.ReadLine(), out doctorId))
    {
        Console.WriteLine("Invalid doctor ID.");
        return false;
    }

    if (clinic.Patients.FindById(patientId) == null ||
        clinic.Doctors.FindById(doctorId) == null)
    {
        Console.WriteLine("Patient or doctor was not found.");
        return false;
    }

    return true;
}

void ShowMedicalRecordStatistics(Clinic clinic)
{
    Console.WriteLine("Total medical records: " + clinic.MedicalRecords.Count);
    Console.WriteLine("Active medical records: " + clinic.MedicalRecords.CountActiveRecords());
    Console.WriteLine("Diagnoses: " + clinic.MedicalRecords.CountRecordsByType("Diagnosis"));
    Console.WriteLine("Lab results: " + clinic.MedicalRecords.CountRecordsByType("Lab Result"));
    Console.WriteLine("Prescriptions: " + clinic.MedicalRecords.CountRecordsByType("Prescription"));
}

void BillingMenu(Clinic clinic)
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.WriteLine("\n--- Billing ---");
        Console.WriteLine("1. Patient debt");
        Console.WriteLine("2. All unpaid appointments");
        Console.WriteLine("3. Pay appointment");
        Console.WriteLine("4. Total clinic debt");
        Console.WriteLine("0. Back");
        Console.Write("Choose option: ");

        switch (Console.ReadLine())
        {
            case "1":
            {
                Console.Write("Patient ID: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid patient ID.");
                    break;
                }

                IPayable[] unpaid = clinic.Billing.GetUnpaidByPatient(patientId);

                Console.WriteLine("Unpaid appointments for patient #" + patientId + ":");
                clinic.Billing.DisplayUnpaid(unpaid);
                Console.WriteLine("Debt: " + clinic.Billing.GetPatientDebt(patientId).ToString("F2") + " UAH");
                break;
            }
            case "2":
                clinic.Billing.DisplayUnpaid(clinic.Billing.GetAllUnpaid());
                break;
            case "3":
            {
                Console.Write("Appointment ID: ");
                if (!int.TryParse(Console.ReadLine(), out int appointmentId))
                {
                    Console.WriteLine("Invalid appointment ID.");
                    break;
                }

                if (clinic.Billing.PayAppointment(appointmentId))
                    Console.WriteLine("Appointment paid.");
                else
                    Console.WriteLine("Payment failed: appointment not found, already paid or cancelled.");

                break;
            }
            case "4":
                Console.WriteLine("Total clinic debt: " + clinic.Billing.GetTotalDebt().ToString("F2") + " UAH");
                break;
            case "7":
                WaitingQueueMenu(clinic);
                break;
            case "0":
                menuRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
}

void ReportMenu(Clinic clinic)
{
    Console.WriteLine("\n--- Clinic Report ---");
    Console.WriteLine("Patients: " + clinic.Patients.Count);
    Console.WriteLine("Doctors: " + clinic.Doctors.Count);
    Console.WriteLine("Appointments: " + clinic.Appointments.Count);
    ShowMedicalRecordStatistics(clinic);
    Console.WriteLine("Total clinic debt: " + clinic.Billing.GetTotalDebt().ToString("F2") + " UAH");
}



void WaitingQueueMenu(Clinic clinic)
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.WriteLine("\n--- Waiting Queue ---");
        Console.WriteLine("1. Add patient to queue");
        Console.WriteLine("2. Show next patient");
        Console.WriteLine("3. Call next patient");
        Console.WriteLine("4. Show all patients in queue");
        Console.WriteLine("5. Show queue count");
        Console.WriteLine("0. Back");
        Console.Write("Choose option: ");

        switch (Console.ReadLine())
        {
            case "1":
            {
                Console.Write("Patient ID: ");

                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid patient ID.");
                    break;
                }

                Patient? patient = clinic.Patients.FindById(patientId);

                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    break;
                }

                clinic.WaitingPatients.Enqueue(patient);
                Console.WriteLine("Patient added to waiting queue.");
                break;
            }
            case "2":
            {
                if (clinic.WaitingPatients.IsEmpty)
                {
                    Console.WriteLine("Waiting queue is empty.");
                    break;
                }

                Console.WriteLine("Next patient: " + clinic.WaitingPatients.Peek());
                break;
            }
            case "3":
            {
                if (clinic.WaitingPatients.IsEmpty)
                {
                    Console.WriteLine("Waiting queue is empty.");
                    break;
                }

                Patient nextPatient = clinic.WaitingPatients.Dequeue();
                Console.WriteLine("Calling patient: " + nextPatient);
                break;
            }
            case "4":
            {
                Patient[] patients = clinic.WaitingPatients.ToArray();

                if (patients.Length == 0)
                {
                    Console.WriteLine("Waiting queue is empty.");
                    break;
                }

                for (int i = 0; i < patients.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + patients[i]);
                }

                break;
            }
            case "5":
                Console.WriteLine("Patients in queue: " + clinic.WaitingPatients.Count);
                break;
            case "0":
                menuRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
}
