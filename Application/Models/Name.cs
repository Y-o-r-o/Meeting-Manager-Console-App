using Application.Extensions;

namespace Application.Models;
public class Name
{
    const int MINIMUM_NAME_LENGHT = 6;
    const int MAXIMUM_NAME_LENGHT = 20;

    private string _value;
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (!value.LenghtIsBetween(MINIMUM_NAME_LENGHT, MAXIMUM_NAME_LENGHT))
                throw new ArgumentException($"Name lenght should be between {MINIMUM_NAME_LENGHT} and {MAXIMUM_NAME_LENGHT}.");
            if (value.ContainsSpecialChars())
                throw new ArgumentException("Name should have only letters or numbers.");

            _value = value;
        }
    }

    public Name(string name)
    {
        Value = name;
    }

    public Name() { }

    public static implicit operator string(Name name) => name.Value;

    public override string ToString() => Value;

}
