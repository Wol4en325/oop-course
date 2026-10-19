using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Models;

Clinic clinic = new Clinic("City Health Clinic", "Main St. 12");

bool running = true;

while (running)
{
    Console.WriteLine("\n=== Clinic Menu ===");
    Console.WriteLine("1. Add patient");
    Console.WriteLine("2. Add doctor");
    Console.WriteLine("3. Make appointment");
    Console.WriteLine("4. Show patients");
    Console.WriteLine("5. Show doctors");
    Console.WriteLine("6. Show appointments");
    Console.WriteLine("7. Add diagnosis");
    Console.WriteLine("8. Add lab result");
    Console.WriteLine("9. Add prescription");
    Console.WriteLine("10. Show all medical records");
    Console.WriteLine("11. Show medical records by type");
    Console.WriteLine("12. Show medical records statistics");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
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

            case "3":
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

                Appointment appointment = new Appointment(patientId, doctorId, scheduledAt, duration);
                clinic.Appointments.Add(appointment);

                Console.WriteLine("Appointment added. ID: " + appointment.Id);
                break;
            }

            case "4":
                for (int i = 0; i < clinic.Patients.Count; i++)
                    Console.WriteLine(clinic.Patients[i]);
                break;

            case "5":
                for (int i = 0; i < clinic.Doctors.Count; i++)
                    Console.WriteLine(clinic.Doctors[i]);
                break;

            case "6":
                for (int i = 0; i < clinic.Appointments.Count; i++)
                    Console.WriteLine(clinic.Appointments[i]);
                break;

            case "7":
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

            case "8":
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

            case "9":
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

            case "10":
                clinic.MedicalRecords.DisplayAllRecords();
                break;

            case "11":
            {
                Console.WriteLine("Types: Diagnosis, Lab Result, Prescription");
                Console.Write("Enter record type: ");
                string recordType = Console.ReadLine() ?? "";

                clinic.MedicalRecords.DisplayRecordsByType(recordType);
                break;
            }

            case "12":
                Console.WriteLine("Total medical records: " + clinic.MedicalRecords.Count);
                Console.WriteLine("Active medical records: " + clinic.MedicalRecords.CountActiveRecords());
                Console.WriteLine("Diagnoses: " + clinic.MedicalRecords.CountRecordsByType("Diagnosis"));
                Console.WriteLine("Lab results: " + clinic.MedicalRecords.CountRecordsByType("Lab Result"));
                Console.WriteLine("Prescriptions: " + clinic.MedicalRecords.CountRecordsByType("Prescription"));
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
        Console.WriteLine("Error: неправильний формат введених даних.");
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: " + e.Message);
    }
}
