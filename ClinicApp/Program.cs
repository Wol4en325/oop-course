using ClinicApp;

Clinic clinic = new Clinic("City Clinic");

SeedData(clinic);

bool running = true;

while (running)
{
    Console.Clear();

    Console.WriteLine("=== City Clinic ===");
    Console.WriteLine("1. Patients");
    Console.WriteLine("2. Doctors");
    Console.WriteLine("3. Appointments");
    Console.WriteLine("4. Schedule");
    Console.WriteLine("5. Report");
    Console.WriteLine("6. Growable Patient Manager test");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    string choice = Console.ReadLine()!;

    Console.WriteLine();

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
            ShowSchedule(clinic);
            break;

        case "5":
            clinic.GenerateReport();
            Pause();
            break;

        case "6":
            GrowableTest();
            break;

        case "0":
            running = false;
            break;

        default:
            Console.WriteLine("Invalid choice.");
            Pause();
            break;
    }
}

void SeedData(Clinic clinic)
{
    Patient patient1 = new Patient(
        "Ivan",
        "Petrenko",
        new DateTime(1985, 5, 10),
        "A+",
        "0501234567"
    );

    Patient patient2 = new Patient(
        "Olena",
        "Koval",
        new DateTime(1992, 8, 15),
        "B-",
        "0672345678"
    );

    Patient patient3 = new Patient(
        "Maksym",
        "Boyko",
        new DateTime(2010, 3, 20),
        "O+",
        "0933456789"
    );

    Patient patient4 = new Patient("Maria", "Tkachenko");

    Patient patient5 = new Patient();

    patient4.DateOfBirth = new DateTime(1998, 7, 12);
    patient4.BloodType = "AB+";
    patient4.Phone = "0661112233";

    patient5.FirstName = "Test";
    patient5.LastName = "Patient";
    patient5.DateOfBirth = new DateTime(1970, 11, 5);
    patient5.BloodType = "A-";
    patient5.Phone = "0991112233";

    clinic.Patients.Add(patient1);
    clinic.Patients.Add(patient2);
    clinic.Patients.Add(patient3);
    clinic.Patients.Add(patient4);
    clinic.Patients.Add(patient5);

    Doctor doctor1 = new Doctor(
        "Oleg",
        "Sydorenko",
        "Cardiology",
        "LIC-001",
        "0441234567"
    );

    Doctor doctor2 = new Doctor(
        "Natalia",
        "Moroz",
        "Neurology",
        "LIC-002",
        "0442345678"
    );

    Doctor doctor3 = new Doctor(
        "Andriy",
        "Vlasenko",
        "Pediatrics",
        "LIC-003",
        "0443456789"
    );

    doctor1.WorkStartHour = 8;
    doctor1.WorkEndHour = 16;

    doctor2.WorkStartHour = 9;
    doctor2.WorkEndHour = 18;

    clinic.Doctors.Add(doctor1);
    clinic.Doctors.Add(doctor2);
    clinic.Doctors.Add(doctor3);

    DateTime tomorrow = DateTime.Today.AddDays(1);
    DateTime dayAfterTomorrow = DateTime.Today.AddDays(2);

    clinic.Appointments.Book(
        patient1.Id,
        doctor1.Id,
        tomorrow.AddHours(10),
        30
    );

    clinic.Appointments.Book(
        patient2.Id,
        doctor2.Id,
        tomorrow.AddHours(11),
        45
    );

    clinic.Appointments.Book(
        patient3.Id,
        doctor3.Id,
        dayAfterTomorrow.AddHours(9),
        20
    );
}

void PatientsMenu(Clinic clinic)
{
    bool running = true;

    while (running)
    {
        Console.Clear();

        Console.WriteLine("=== Patients ===");
        Console.WriteLine("1. Show all");
        Console.WriteLine("2. Find by ID");
        Console.WriteLine("3. Find by name");
        Console.WriteLine("4. Add patient");
        Console.WriteLine("5. Remove patient");
        Console.WriteLine("6. Statistics");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string choice = Console.ReadLine()!;

        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                Pause();
                break;

            case "2":
                Console.Write("Enter patient ID: ");
                int id = ReadInt();

                Patient? patient = clinic.Patients.FindById(id);

                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                }
                else
                {
                    Console.WriteLine(patient);
                }

                Pause();
                break;

            case "3":
                Console.Write("Enter name: ");
                string name = Console.ReadLine()!;

                Patient[] patients = clinic.Patients.FindByName(name);

                if (patients.Length == 0)
                {
                    Console.WriteLine("No patients found.");
                }
                else
                {
                    for (int i = 0; i < patients.Length; i++)
                    {
                        Console.WriteLine(patients[i]);
                    }
                }

                Pause();
                break;

            case "4":
                Console.Write("First name: ");
                string firstName = Console.ReadLine()!;

                Console.Write("Last name: ");
                string lastName = Console.ReadLine()!;

                Console.Write("Birth year: ");
                int year = ReadInt();

                Console.Write("Birth month: ");
                int month = ReadInt();

                Console.Write("Birth day: ");
                int day = ReadInt();

                Console.Write("Blood type: ");
                string bloodType = Console.ReadLine()!;

                Console.Write("Phone: ");
                string phone = Console.ReadLine()!;

                Patient newPatient = new Patient(
                    firstName,
                    lastName,
                    new DateTime(year, month, day),
                    bloodType,
                    phone
                );

                clinic.Patients.Add(newPatient);
                Pause();
                break;

            case "5":
                Console.Write("Enter patient ID: ");
                int removeId = ReadInt();

                if (clinic.Patients.Remove(removeId))
                {
                    Console.WriteLine("Patient removed.");
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }

                Pause();
                break;

            case "6":
                clinic.Patients.DisplayStats();
                Pause();
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Invalid choice.");
                Pause();
                break;
        }
    }
}

void DoctorsMenu(Clinic clinic)
{
    bool running = true;

    while (running)
    {
        Console.Clear();

        Console.WriteLine("=== Doctors ===");
        Console.WriteLine("1. Show all");
        Console.WriteLine("2. Find by ID");
        Console.WriteLine("3. Find by speciality");
        Console.WriteLine("4. Add doctor");
        Console.WriteLine("5. Remove doctor");
        Console.WriteLine("6. Statistics");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string choice = Console.ReadLine()!;

        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                Pause();
                break;

            case "2":
                Console.Write("Enter doctor ID: ");
                int id = ReadInt();

                Doctor? doctor = clinic.Doctors.FindById(id);

                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                }
                else
                {
                    Console.WriteLine(doctor);
                }

                Pause();
                break;

            case "3":
                Console.Write("Enter speciality: ");
                string speciality = Console.ReadLine()!;

                Doctor[] doctors = clinic.Doctors.FindBySpeciality(speciality);

                if (doctors.Length == 0)
                {
                    Console.WriteLine("No doctors found.");
                }
                else
                {
                    for (int i = 0; i < doctors.Length; i++)
                    {
                        Console.WriteLine(doctors[i]);
                    }
                }

                Pause();
                break;

            case "4":
                Console.Write("First name: ");
                string firstName = Console.ReadLine()!;

                Console.Write("Last name: ");
                string lastName = Console.ReadLine()!;

                Console.Write("Speciality: ");
                string newSpeciality = Console.ReadLine()!;

                Console.Write("License number: ");
                string license = Console.ReadLine()!;

                Console.Write("Phone: ");
                string phone = Console.ReadLine()!;

                Doctor newDoctor = new Doctor(
                    firstName,
                    lastName,
                    newSpeciality,
                    license,
                    phone
                );

                clinic.Doctors.Add(newDoctor);
                Pause();
                break;

            case "5":
                Console.Write("Enter doctor ID: ");
                int removeId = ReadInt();

                if (clinic.Doctors.Remove(removeId))
                {
                    Console.WriteLine("Doctor removed.");
                }
                else
                {
                    Console.WriteLine("Doctor not found.");
                }

                Pause();
                break;

            case "6":
                clinic.Doctors.DisplayStats();
                Pause();
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Invalid choice.");
                Pause();
                break;
        }
    }
}

void AppointmentsMenu(Clinic clinic)
{
    bool running = true;

    while (running)
    {
        Console.Clear();

        Console.WriteLine("=== Appointments ===");
        Console.WriteLine("1. Show upcoming");
        Console.WriteLine("2. Show by patient");
        Console.WriteLine("3. Show by doctor");
        Console.WriteLine("4. Show by date");
        Console.WriteLine("5. Book appointment");
        Console.WriteLine("6. Cancel appointment");
        Console.WriteLine("7. Complete appointment");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string choice = Console.ReadLine()!;

        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetUpcoming()
                );

                Pause();
                break;

            case "2":
                clinic.Patients.DisplayAll();

                Console.Write("Enter patient ID: ");
                int patientId = ReadInt();

                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetByPatient(patientId)
                );

                Pause();
                break;

            case "3":
                clinic.Doctors.DisplayAll();

                Console.Write("Enter doctor ID: ");
                int doctorId = ReadInt();

                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetByDoctor(doctorId)
                );

                Pause();
                break;

            case "4":
                Console.Write("Year: ");
                int year = ReadInt();

                Console.Write("Month: ");
                int month = ReadInt();

                Console.Write("Day: ");
                int day = ReadInt();

                DateTime date = new DateTime(year, month, day);

                clinic.Appointments.DisplayList(
                    clinic.Appointments.GetByDate(date)
                );

                Pause();
                break;

            case "5":
                clinic.Patients.DisplayAll();
                Console.WriteLine();

                clinic.Doctors.DisplayAll();
                Console.WriteLine();

                Console.Write("Patient ID: ");
                int newPatientId = ReadInt();

                Console.Write("Doctor ID: ");
                int newDoctorId = ReadInt();

                Console.Write("Year: ");
                int appointmentYear = ReadInt();

                Console.Write("Month: ");
                int appointmentMonth = ReadInt();

                Console.Write("Day: ");
                int appointmentDay = ReadInt();

                Console.Write("Hour: ");
                int hour = ReadInt();

                Console.Write("Minute: ");
                int minute = ReadInt();

                Console.Write("Duration in minutes: ");
                int duration = ReadInt();

                clinic.Appointments.Book(
                    newPatientId,
                    newDoctorId,
                    new DateTime(
                        appointmentYear,
                        appointmentMonth,
                        appointmentDay,
                        hour,
                        minute,
                        0
                    ),
                    duration
                );

                Pause();
                break;

            case "6":
                Console.Write("Appointment ID: ");
                int cancelId = ReadInt();

                Console.Write("Reason: ");
                string reason = Console.ReadLine()!;

                if (clinic.Appointments.Cancel(cancelId, reason))
                {
                    Console.WriteLine("Appointment cancelled.");
                }
                else
                {
                    Console.WriteLine("Appointment not found or already completed/cancelled.");
                }

                Pause();
                break;

            case "7":
                Console.Write("Appointment ID: ");
                int completeId = ReadInt();

                if (clinic.Appointments.Complete(completeId))
                {
                    Console.WriteLine("Appointment completed.");
                }
                else
                {
                    Console.WriteLine("Appointment not found or already completed/cancelled.");
                }

                Pause();
                break;

            case "0":
                running = false;
                break;

            default:
                Console.WriteLine("Invalid choice.");
                Pause();
                break;
        }
    }
}

void ShowSchedule(Clinic clinic)
{
    Console.Clear();

    Console.WriteLine("=== Schedule ===");

    Console.Write("Year: ");
    int year = ReadInt();

    Console.Write("Month: ");
    int month = ReadInt();

    Console.Write("Day: ");
    int day = ReadInt();

    DateTime date = new DateTime(year, month, day);

    clinic.DisplaySchedule(date);

    Pause();
}

void GrowableTest()
{
    Console.Clear();

    GrowablePatientManager manager = new GrowablePatientManager();

    Console.WriteLine("=== Growable Patient Manager Test ===");
    Console.WriteLine("Adding 20 patients...");
    Console.WriteLine();

    for (int i = 1; i <= 20; i++)
    {
        Patient patient = new Patient(
            "Test",
            "Patient" + i,
            new DateTime(1990, 1, 1).AddYears(i),
            "A+",
            "05000000" + i.ToString("D2")
        );

        manager.Add(patient);
    }

    Console.WriteLine();
    Console.WriteLine("=== FindById Test ===");

    Patient? found = manager.FindById(10);

    if (found == null)
    {
        Console.WriteLine("FindById(10) -> not found");
    }
    else
    {
        Console.WriteLine("FindById(10) -> " + found.FullName);
    }

    Patient? missing = manager.FindById(99);

    if (missing == null)
    {
        Console.WriteLine("FindById(99) -> not found");
    }
    else
    {
        Console.WriteLine("FindById(99) -> " + missing.FullName);
    }

    Console.WriteLine();
    Console.WriteLine("=== Remove Test ===");

    if (manager.Remove(10))
    {
        Console.WriteLine("Patient 10 removed.");
    }
    else
    {
        Console.WriteLine("Patient 10 not found.");
    }

    Console.WriteLine("Count: " + manager.Count);
    Console.WriteLine("Capacity: " + manager.Capacity);

    manager.DisplayAll();

    Pause();
}

int ReadInt()
{
    string input = Console.ReadLine()!;

    int value;

    if (int.TryParse(input, out value))
    {
        return value;
    }

    return 0;
}

void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}
