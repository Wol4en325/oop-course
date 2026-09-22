using ClinicApp;

Patient patient1 = new Patient();
patient1.FirstName = "John";
patient1.LastName = "Smith";
patient1.DateOfBirth = new DateTime(1985, 5, 12);
patient1.BloodType = "A+";
patient1.Phone = "0501234567";

Patient patient2 = new Patient("Mary", "Johnson");

Patient patient3 = new Patient(
    "Alex",
    "Brown",
    new DateTime(2002, 8, 20),
    "O+",
    "0671234567"
);

Patient patient4 = new Patient();
patient4.FirstName = "Anna";
patient4.LastName = "Wilson";
patient4.DateOfBirth = new DateTime(2010, 3, 15);
patient4.BloodType = "B+";
patient4.Phone = "0937654321";

Patient patient5 = new Patient(
    "Peter",
    "Taylor",
    new DateTime(1955, 11, 3),
    "AB+",
    "0995554433"
);

Console.WriteLine("=== Patients ===");
Console.WriteLine(patient1);
Console.WriteLine(patient2);
Console.WriteLine(patient3);
Console.WriteLine(patient4);
Console.WriteLine(patient5);

Doctor doctor1 = new Doctor(
    "John",
    "Williams",
    "Cardiology",
    "LIC-001",
    "0441234567"
);
doctor1.WorkStartHour = 8;
doctor1.WorkEndHour = 16;

Doctor doctor2 = new Doctor(
    "Sarah",
    "Miller",
    "Neurology",
    "LIC-002",
    "0442345678"
);
doctor2.WorkStartHour = 9;
doctor2.WorkEndHour = 18;

Doctor doctor3 = new Doctor(
    "Andrew",
    "Davis",
    "Pediatrics",
    "LIC-003",
    "0443456789"
);

Doctor doctor4 = new Doctor("Emma", "Wilson", "Dentistry");

Console.WriteLine();
Console.WriteLine("=== Doctors ===");
Console.WriteLine(doctor1);
Console.WriteLine(doctor2);
Console.WriteLine(doctor3);
Console.WriteLine(doctor4);
