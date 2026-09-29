using ClinicApp;

GrowablePatientManager manager = new GrowablePatientManager();

Console.WriteLine("=== Growable Patient Manager Test ===");
Console.WriteLine("Initial capacity: " + manager.Capacity);

for (int i = 1; i <= 20; i++)
{
    Patient patient = new Patient(
        "Patient",
        i.ToString(),
        new DateTime(1990, 1, 1).AddYears(i),
        "A+",
        "05000000" + i.ToString("D2")
    );

    manager.Add(patient);
}

manager.DisplayAll();

Console.WriteLine();
Console.WriteLine("=== Find By ID ===");

Patient? foundPatient = manager.FindById(10);

if (foundPatient != null)
{
    Console.WriteLine(foundPatient);
}
else
{
    Console.WriteLine("Patient not found.");
}

Console.WriteLine();
Console.WriteLine("=== Remove ===");

bool removed = manager.Remove(10);

if (removed)
{
    Console.WriteLine("Patient removed.");
}
else
{
    Console.WriteLine("Patient not found.");
}

Console.WriteLine("Count: " + manager.Count);
Console.WriteLine("Capacity: " + manager.Capacity);

manager.DisplayAll();
