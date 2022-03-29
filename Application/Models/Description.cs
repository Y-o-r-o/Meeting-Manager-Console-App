using Application.Core;
using Application.Extensions;

namespace Application.Models;

public class Description
{
    const int MINIMUM_DESCRIPTION_LENGHT = 0;
    const int MAXIMUM_DESCRIPTION_LENGHT = 200;

    public string Value { get; private set; }

    public Result setDescription(string name)
    {
        if (!name.LenghtIsBetween(MINIMUM_DESCRIPTION_LENGHT, MAXIMUM_DESCRIPTION_LENGHT))
            return Result.Failure($"Description lenght should be between {MINIMUM_DESCRIPTION_LENGHT} and {MAXIMUM_DESCRIPTION_LENGHT}.");

        Value = name;

        return Result.Success();
    }
}
