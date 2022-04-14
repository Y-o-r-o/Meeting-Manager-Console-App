using Application.Core;
using Application.Extensions;

namespace Application.Models;

public class Description
{
    const int MINIMUM_DESCRIPTION_LENGHT = 0;
    const int MAXIMUM_DESCRIPTION_LENGHT = 200;

    public string Value { get; }

    public Description ()
    {
        Value = string.Empty;
    }

    public Description (string name)
    {
        if (!name.LenghtIsBetween(MINIMUM_DESCRIPTION_LENGHT, MAXIMUM_DESCRIPTION_LENGHT))
            throw new ArgumentException($"Description lenght should be between {MINIMUM_DESCRIPTION_LENGHT} and {MAXIMUM_DESCRIPTION_LENGHT}.");

        Value = name;
    }

    public static implicit operator string(Description description) => description.Value;

    public override string ToString() => Value;
}