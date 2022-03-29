using Application.Core;
using Application.Enums;

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

    // This would be better solution, but sadly i didn't figuret out this to work.
    //
    // public static async Result<T> TryParseEnum<T>(this string str) where T : Enum
    // {
    //     var isEnumParsed = Enum.TryParse(str, true, out T parsed);
    //     return isEnumParsed ?
    //         Result<T>.Success(parsed) :
    //         Result<T>.Failure("Given string didn't matched any existing ___.");
    // }

    public static Result<Category> TryParseToCategory(this string str)
    {
        var isEnumParsed = Enum.TryParse(str, true, out Category parsed);
        return isEnumParsed ?
            Result<Category>.Success(parsed) :
            Result<Category>.Failure("Given string didn't matched any existing category.");
    }

    public static Result<Enums.Type> TryParseToType(this string str)
    {
        var isEnumParsed = Enum.TryParse(str, true, out Enums.Type parsed);
        return isEnumParsed ?
            Result<Enums.Type>.Success(parsed) :
            Result<Enums.Type>.Failure("Given string didn't matched any existing type.");
    }

    public static Result<DateTime> TryParseToDateTime(this string str)
    {
        DateTime dateTime; 
        try {
            dateTime = DateTime.Parse(str);
        } catch (FormatException){
            return Result<DateTime>.Failure("Given string didn.t matched date format.");
        }
        return Result<DateTime>.Success(dateTime);
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