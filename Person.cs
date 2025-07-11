using System;

public interface IPerson
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Address { get; set; }
    DateTime DateOfBirth { get; set; }
    string StarSign { get; set; }
}

public class Person : IPerson
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _address = string.Empty;
    private DateTime _dateOfBirth;
    private string _starSign = string.Empty;

    public string FirstName
    {
        get =>  _firstName;
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Address
    {
        get => _address;
        set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => _dateOfBirth = value;
    }

    public string StarSign
    {
        get => _starSign;
        set => _starSign = value ?? throw new ArgumentNullException(nameof(value));
    }
}