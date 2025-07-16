using System;
using System.IO;

public class PersonMain
{
    public static void Main(string[] args)
    {
        string peoplesDir = @"C:\Peoples";
        string peopleFile = Path.Combine(peoplesDir, "people.txt");
        if (!Directory.Exists(peoplesDir))
        {
            Directory.CreateDirectory(peoplesDir);
        }
        if (!File.Exists(peopleFile))
        {
            File.WriteAllText(peopleFile, string.Empty);
        }

        Console.WriteLine("Create a new person:");

        Console.Write("First name: ");
        string? firstName = Console.ReadLine();

        Console.Write("Last name: ");
        string? lastName = Console.ReadLine();

        Console.Write("Address: ");
        string? address = Console.ReadLine();

        Console.Write("Date of birth (yyyy-MM-dd): ");
        DateTime dateOfBirth;
        while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
        {
            Console.Write("Invalid date. Please enter again (yyyy-MM-dd): ");
        }

        int age = DateTime.Now.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;

        bool? parentConsent = null;
        if (age < 16)
        {
            Console.WriteLine("You must be at least 16 years old to register.");
            return;
        }
        else if (age == 17)
        {
            Console.Write("Do you have parental consent? (yes/no): ");
            string? consentInput = Console.ReadLine()?.ToLower();
            parentConsent = consentInput == "yes";
            if (parentConsent == false)
            {
                Console.WriteLine("You cannot register without parental consent.");
                return;
            }
        }

        Console.Write("Star sign: ");
        string? starSign = Console.ReadLine();

        Guid personId = Guid.NewGuid();
        Person person = new Person
        {
            FirstName = firstName ?? string.Empty,
            LastName = lastName ?? string.Empty,
            Address = address ?? string.Empty,
            DateOfBirth = dateOfBirth,
            StarSign = starSign ?? string.Empty,
            ParentConsent = parentConsent
        };

        // Check for duplicate
        string personLine = $"{personId},{person.FirstName},{person.LastName},{person.Address},{person.DateOfBirth:yyyy-MM-dd},{person.StarSign},{person.ParentConsent}";
        bool isDuplicate = false;
        foreach (var line in File.ReadAllLines(peopleFile))
        {
            var parts = line.Split(',');
            if (parts.Length >= 7 &&
                parts[1] == person.FirstName &&
                parts[2] == person.LastName &&
                parts[3] == person.Address &&
                parts[4] == person.DateOfBirth.ToString("yyyy-MM-dd") &&
                parts[5] == person.StarSign &&
                parts[6] == (person.ParentConsent?.ToString() ?? ""))
            {
                isDuplicate = true;
                break;
            }
        }
        if (isDuplicate)
        {
            Console.WriteLine("A person with these details already exists. Registration aborted.");
            return;
        }

        Console.Write("Do you have a spouse to register? (yes/no): ");
        string? hasSpouse = Console.ReadLine()?.ToLower();
        if (hasSpouse == "yes")
        {
            Console.WriteLine("Enter spouse details:");
            Console.Write("First name: ");
            string? spouseFirstName = Console.ReadLine();
            Console.Write("Last name: ");
            string? spouseLastName = Console.ReadLine();
            Console.Write("Address: ");
            string? spouseAddress = Console.ReadLine();
            Console.Write("Date of birth (yyyy-MM-dd): ");
            DateTime spouseDob;
            while (!DateTime.TryParse(Console.ReadLine(), out spouseDob))
            {
                Console.Write("Invalid date. Please enter again (yyyy-MM-dd): ");
            }
            Console.Write("Star sign: ");
            string? spouseStarSign = Console.ReadLine();
            Guid spouseId = Guid.NewGuid();
            Person spouse = new Person
            {
                FirstName = spouseFirstName ?? string.Empty,
                LastName = spouseLastName ?? string.Empty,
                Address = spouseAddress ?? string.Empty,
                DateOfBirth = spouseDob,
                StarSign = spouseStarSign ?? string.Empty
            };
            person.Spouse = spouse;
            string spouseFile = Path.Combine(peoplesDir, $"{spouse.FirstName}_{spouse.LastName}_{spouseId}.txt");
            using (StreamWriter sw = new StreamWriter(spouseFile))
            {
                sw.WriteLine($"Name: {spouse.FirstName} {spouse.LastName}");
                sw.WriteLine($"Address: {spouse.Address}");
                sw.WriteLine($"Date of Birth: {spouse.DateOfBirth:yyyy-MM-dd}");
                sw.WriteLine($"Star Sign: {spouse.StarSign}");
            }
        }

        // Save person to people.txt as a single line
        using (StreamWriter writer = new StreamWriter(peopleFile, append: true))
        {
            writer.WriteLine(personLine);
        }

        Console.WriteLine($"\nPerson registered successfully. Details saved to people.txt in C:\\Peoples");
    }
}