using Application.Core;
using Application.Extensions;

namespace Application.Models;
public class Name
{
    const int MINIMUM_NAME_LENGHT = 6;
    const int MAXIMUM_NAME_LENGHT = 20;

    private string _value = string.Empty;
    public string Value
    {
        get { return _value; }
        set { setName(value); }
    }

    public Result setName(string name)
    {
        if (!name.LenghtIsBetween(MINIMUM_NAME_LENGHT, MAXIMUM_NAME_LENGHT))
            return Result.Failure($"Name lenght should be between {MINIMUM_NAME_LENGHT} and {MAXIMUM_NAME_LENGHT}.");
        if (name.ContainsSpecialChars())
            return Result.Failure("Name should have only letters or numbers.");

        _value = name;

        return Result.Success();
    }
}
