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
    Console.WriteLine("4. Show patient medical card");
    Console.WriteLine("5. Show medical records report");
    Console.WriteLine("6. Show patients");
    Console.WriteLine("7. Show doctors");
    Console.WriteLine("8. Show appointments");
    Console.WriteLine("9. Add diagnosis");
    Console.WriteLine("10. Add lab result");
    Console.WriteLine("11. Add prescription");
    Console.WriteLine("12. Show all medical records");
    Console.WriteLine("13. Show medical records by type");
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

            case "6":
                for (int i = 0; i < clinic.Patients.Count; i++)
                    Console.WriteLine(clinic.Patients[i]);
                break;

            case "7":
                for (int i = 0; i < clinic.Doctors.Count; i++)
                    Console.WriteLine(clinic.Doctors[i]);
                break;

            case "8":
                for (int i = 0; i < clinic.Appointments.Count; i++)
                    Console.WriteLine(clinic.Appointments[i]);
                break;

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

            case "10":
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
                Console.Write("Unit (optional): ");
                string unit = Console.ReadLine() ?? "";
                Console.Write("Numeric result: ");
                if (!double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double value))
                {
                    Console.WriteLine("Invalid numeric value.");
                    break;
                }

                Console.Write("Reference range: ");
                string referenceRange = Console.ReadLine() ?? "";

                Console.Write("Is the result normal? (y/n): ");
                bool isNormal = (Console.ReadLine() ?? "").Trim().ToLowerInvariant() == "y";

                LabResult labResult = new LabResult(patientId, doctorId, date, testName, value, unit, referenceRange, isNormal);
                clinic.MedicalRecords.AddRecord(labResult);

                Console.WriteLine("Lab result added. ID: " + labResult.Id);
                break;
            }

            case "11":
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

                DateTime date = DateTime.Today;

                Console.Write("Medication name: ");
                string medication = Console.ReadLine() ?? "";

                Console.Write("Dosage: ");
                string dosage = Console.ReadLine() ?? "";

                Console.Write("Duration in days: ");
                if (!int.TryParse(Console.ReadLine(), out int durationDays) || durationDays <= 0)
                {
                    Console.WriteLine("Invalid duration.");
                    break;
                }

                Console.Write("Instructions (optional): ");
                string instructions = Console.ReadLine() ?? "";

                Prescription prescription = new Prescription(
                    patientId, doctorId, date, medication, dosage, durationDays, instructions);

                clinic.MedicalRecords.AddRecord(prescription);

                Console.WriteLine("Prescription added. ID: " + prescription.Id);
                break;
            }

            case "12":
                clinic.MedicalRecords.DisplayAllRecords();
                break;

            case "13":
            {
                Console.WriteLine("Types: Діагноз, Аналіз, Рецепт");
                Console.Write("Enter record type: ");
                string recordType = Console.ReadLine() ?? "";

                clinic.MedicalRecords.DisplayRecordsByType(recordType);
                break;
            }

            case "5":
                Console.WriteLine("Total medical records: " + clinic.MedicalRecords.Count);
                Console.WriteLine("Active medical records: " + clinic.MedicalRecords.CountActiveRecords());
                Console.WriteLine("Diagnoses: " + clinic.MedicalRecords.CountRecordsByType("Діагноз"));
                Console.WriteLine("Lab results: " + clinic.MedicalRecords.CountRecordsByType("Аналіз"));
                Console.WriteLine("Prescriptions: " + clinic.MedicalRecords.CountRecordsByType("Рецепт"));
                break;

            case "4":
            {
                Console.Write("Patient ID: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid patient ID.");
                    break;
                }

                if (clinic.Patients.FindById(patientId) == null)
                {
                    Console.WriteLine("Patient was not found.");
                    break;
                }

                clinic.MedicalRecords.DisplayPatientSummary(patientId);
                break;
            }
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
