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
        Console.WriteLine("Помилка: " + e.Message);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine("Помилка: " + e.Message);
    }
    catch (FormatException)
    {
        Console.WriteLine("Помилка: неправильний формат введених даних.");
    }
    catch (Exception e)
    {
        Console.WriteLine("Помилка: " + e.Message);
    }
}
