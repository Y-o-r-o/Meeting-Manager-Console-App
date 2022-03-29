using Application.Core;
using Application.Extensions;

namespace Application.Models;

public class Description
{
    const int MINIMUM_DESCRIPTION_LENGHT = 0;
    const int MAXIMUM_DESCRIPTION_LENGHT = 200;

    private string _value = string.Empty;
    public string Value
    {
        get { return _value; }
        set { setDescription(value); }
    }

    public Result setDescription(string name)
    {
        if (!name.LenghtIsBetween(MINIMUM_DESCRIPTION_LENGHT, MAXIMUM_DESCRIPTION_LENGHT))
            return Result.Failure($"Description lenght should be between {MINIMUM_DESCRIPTION_LENGHT} and {MAXIMUM_DESCRIPTION_LENGHT}.");

        _value = name;

        return Result.Success();
    }
}
