using Application.Extensions;

namespace Application.Models;

public class Description
{
    const int MINIMUM_DESCRIPTION_LENGHT = 0;
    const int MAXIMUM_DESCRIPTION_LENGHT = 200;

    private string _value;
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (!value.LenghtIsBetween(MINIMUM_DESCRIPTION_LENGHT, MAXIMUM_DESCRIPTION_LENGHT))
                throw new ArgumentException($"Description lenght should be between {MINIMUM_DESCRIPTION_LENGHT} and {MAXIMUM_DESCRIPTION_LENGHT}.");

            _value = value;
        }
    }

    public Description(string name)
    {
        Value = name;
    }

    public Description() { }

    public static implicit operator string(Description description) => description.Value;

    public override string ToString() => Value;
}