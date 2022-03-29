using Application.Core;
using Application.Enums;
using Application.Extensions;
using Application.Helpers;

namespace Application.Models;
public class Meeting
{
    public Name Name { get; private set; }
    public Person ResponsiblePerson { get; set; }
    public Description Description { get; private set; }
    public Category Category { get; private set; }
    public Enums.Type Type { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public List<Person> Attendees { get; set; }


    public Meeting(){
        Name = new Name();
        Description = new Description();
        Attendees = new List<Person>();
    }
    public Result setName(string name)
    {
        Result result;

        result = Name.setName(name);

        return result;
    }

    public Result setDescription(string description)
    {
        Result result;

        result = Description.setDescription(description);

        return result;
    }

    public Result setCategory(string category)
    {
        Result<Category> result;

        result = category.TryParseToCategory();
        if (result.IsSuccess)
        {
            Category = result.Value;
            return Result.Success();
        }
        return Result.Failure(result.Error);
    }

    public Result setType(string type)
    {
        Result<Enums.Type> result;

        result = type.TryParseToType();
        if (result.IsSuccess)
        {
            Type = result.Value;
            return Result.Success();
        }

        return Result.Failure(result.Error);
    }

    public Result setStartDate(string startDate)
    {
        Result<DateTime> result;

        result = startDate.TryParseToDateTime();
        if (result.IsSuccess)
        {
            StartDate = result.Value;
            return Result.Success();
        }

        return Result.Failure(result.Error);
    }

    public Result setEndDate(string endDate)
    {
        Result<DateTime> result;

        result = endDate.TryParseToDateTime();
        if (result.IsSuccess)
        {
            if ((result.Value < StartDate))
                return Result.Failure("End date can't be before start date.");
            EndDate = result.Value;
            return Result.Success();
        }

        return Result.Failure(result.Error);
    }

    public string getName(){ return Name.Value; }
}