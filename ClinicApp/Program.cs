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

PatientManager patientManager = new PatientManager();

patientManager.Add(patient1);
patientManager.Add(patient2);
patientManager.Add(patient3);
patientManager.Add(patient4);
patientManager.Add(patient5);

patientManager.DisplayAll();
patientManager.DisplayStats();

Console.WriteLine();
Console.WriteLine("=== Search ===");

Patient[] foundPatients = patientManager.FindByName("John");

for (int i = 0; i < foundPatients.Length; i++)
{
    Console.WriteLine(foundPatients[i]);
}

Console.WriteLine();
Console.WriteLine("=== Find By ID ===");

Patient? foundPatient = patientManager.FindById(3);

if (foundPatient == null)
{
    Console.WriteLine("Patient not found.");
}
else
{
    Console.WriteLine(foundPatient);
}

Console.WriteLine();
Console.WriteLine("=== Remove ===");

bool removed = patientManager.Remove(2);

if (removed)
{
    Console.WriteLine("Patient removed.");
}
else
{
    Console.WriteLine("Patient not found.");
}

patientManager.DisplayAll();

DoctorManager doctorManager = new DoctorManager();

doctorManager.Add(doctor1);
doctorManager.Add(doctor2);
doctorManager.Add(doctor3);
doctorManager.Add(doctor4);

doctorManager.DisplayAll();
doctorManager.DisplayStats();

Console.WriteLine();
Console.WriteLine("=== Search By Speciality ===");

Doctor[] foundDoctors = doctorManager.FindBySpeciality("Cardiology");

for (int i = 0; i < foundDoctors.Length; i++)
{
    Console.WriteLine(foundDoctors[i]);
}

Console.WriteLine();
Console.WriteLine("=== Find Doctor By ID ===");

Doctor? foundDoctor = doctorManager.FindById(3);

if (foundDoctor == null)
{
    Console.WriteLine("Doctor not found.");
}
else
{
    Console.WriteLine(foundDoctor);
}

Console.WriteLine();
Console.WriteLine("=== Remove Doctor ===");

bool doctorRemoved = doctorManager.Remove(4);

if (doctorRemoved)
{
    Console.WriteLine("Doctor removed.");
}
else
{
    Console.WriteLine("Doctor not found.");
}

doctorManager.DisplayAll();

Console.WriteLine();
Console.WriteLine("=== Appointments ===");

Appointment appointment1 = new Appointment(
    patient1.Id,
    doctor1.Id,
    DateTime.Now.AddDays(1).Date.AddHours(10),
    30
);

Appointment appointment2 = new Appointment(
    patient2.Id,
    doctor2.Id,
    DateTime.Now.AddDays(1).Date.AddHours(11),
    45
);

Appointment appointment3 = new Appointment(
    patient3.Id,
    doctor3.Id,
    DateTime.Now.AddDays(2).Date.AddHours(9),
    20
);

Console.WriteLine(appointment1);
Console.WriteLine(appointment2);
Console.WriteLine(appointment3);

Console.WriteLine();
Console.WriteLine("=== Appointment Status ===");

bool cancelled = appointment1.Cancel("Patient could not come.");

if (cancelled)
{
    Console.WriteLine("Appointment 1 cancelled.");
}

bool completed = appointment2.Complete();

if (completed)
{
    Console.WriteLine("Appointment 2 completed.");
}

bool secondCancel = appointment1.Cancel("Another reason.");

if (secondCancel)
{
    Console.WriteLine("Appointment 1 cancelled again.");
}
else
{
    Console.WriteLine("Appointment 1 cannot be cancelled again.");
}

Console.WriteLine();
Console.WriteLine(appointment1);
Console.WriteLine(appointment2);
Console.WriteLine(appointment3);
