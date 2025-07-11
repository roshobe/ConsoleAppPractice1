using System;

public class PersonMain
{
    public static void Main(string[] args)
    {
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

        Console.Write("Star sign: ");
        string? starSign = Console.ReadLine();

        Person person = new Person
        {
            FirstName = firstName ?? string.Empty,
            LastName = lastName ?? string.Empty,
            Address = address ?? string.Empty,
            DateOfBirth = dateOfBirth,
            StarSign = starSign ?? string.Empty
        };

        Console.WriteLine("\nPerson created:");
        Console.WriteLine($"Name: {person.FirstName} {person.LastName}");
        Console.WriteLine($"Address: {person.Address}");
        Console.WriteLine($"Date of Birth: {person.DateOfBirth:yyyy-MM-dd}");
        Console.WriteLine($"Star Sign: {person.StarSign}");
        
    }
}