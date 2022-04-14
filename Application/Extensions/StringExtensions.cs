using Application.Core;

namespace Application.Extensions;

public static class StringExtensions
{
    public static bool ContainsSpecialChars(this string str)
    {
        return str.Any(c => !char.IsLetterOrDigit(c));
    }

    public static bool LenghtIsBetween(this string str, int minLenght, int maxLenght)
    {
        return (str.Length >= minLenght && str.Length <= maxLenght);
    }

    

    public static Result<DateOnly> TryParseToDateOnly(this string str)
    {
        DateOnly dateOnly; 
        try {
            dateOnly = DateOnly.Parse(str);
        } catch (FormatException){
            return Result<DateOnly>.Failure("Given string didn.t matched date format.");
        }
        return Result<DateOnly>.Success(dateOnly);
    }
}