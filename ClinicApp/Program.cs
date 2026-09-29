using ClinicApp;

Clinic clinic = new Clinic("City Clinic");

Patient patient1 = new Patient(
    "John",
    "Smith",
    new DateTime(1985, 5, 12),
    "A+",
    "0501234567"
);

Patient patient2 = new Patient(
    "Alex",
    "Brown",
    new DateTime(2002, 8, 20),
    "O+",
    "0671234567"
);

Patient patient3 = new Patient(
    "Anna",
    "Wilson",
    new DateTime(2010, 3, 15),
    "B+",
    "0937654321"
);

clinic.Patients.Add(patient1);
clinic.Patients.Add(patient2);
clinic.Patients.Add(patient3);

Doctor doctor1 = new Doctor(
    "John",
    "Williams",
    "Cardiology",
    "LIC-001",
    "0441234567"
);

Doctor doctor2 = new Doctor(
    "Sarah",
    "Miller",
    "Neurology",
    "LIC-002",
    "0442345678"
);

Doctor doctor3 = new Doctor(
    "Andrew",
    "Davis",
    "Pediatrics",
    "LIC-003",
    "0443456789"
);

doctor1.WorkStartHour = 8;
doctor1.WorkEndHour = 16;

doctor2.WorkStartHour = 9;
doctor2.WorkEndHour = 18;

doctor3.WorkStartHour = 8;
doctor3.WorkEndHour = 17;

clinic.Doctors.Add(doctor1);
clinic.Doctors.Add(doctor2);
clinic.Doctors.Add(doctor3);

DateTime tomorrow = DateTime.Today.AddDays(1);

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
    tomorrow.AddDays(1).AddHours(9),
    20
);

Console.WriteLine();
Console.WriteLine("=== Clinic Patients ===");
clinic.Patients.DisplayAll();

Console.WriteLine();
Console.WriteLine("=== Clinic Doctors ===");
clinic.Doctors.DisplayAll();

Console.WriteLine();
Console.WriteLine("=== Tomorrow Schedule ===");
clinic.DisplaySchedule(tomorrow);

Console.WriteLine();
Console.WriteLine("=== Next Day Schedule ===");
clinic.DisplaySchedule(tomorrow.AddDays(1));

clinic.GenerateReport();
