using Application.Core;
using Application.Extensions;

namespace Application.Models;
public class Name
{
    const int MINIMUM_NAME_LENGHT = 6;
    const int MAXIMUM_NAME_LENGHT = 20;

    public string Value { get; }

    public Name(string name)
    {
        if (!name.LenghtIsBetween(MINIMUM_NAME_LENGHT, MAXIMUM_NAME_LENGHT))
            throw new ArgumentException($"Name lenght should be between {MINIMUM_NAME_LENGHT} and {MAXIMUM_NAME_LENGHT}.");
        if (name.ContainsSpecialChars())
            throw new ArgumentException("Name should have only letters or numbers.");

        Value = name;
    }

    public static implicit operator string(Name name) => name.Value;

    public override string ToString() => Value;
    
}
