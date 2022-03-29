using Application.Core;
using Application.Enums;
using Application.Extensions;
using Application.Helpers;

namespace Application.Models;
public class Meeting
{
    private Name _name;
    private Description _description;

    public Name Name
    {
        get { return _name; }
        set { setName(value.Value); }
    }
    public Person ResponsiblePerson { get; set; }
    public Description Description
    {
        get { return _description; }
        set { setDescription(value.Value); }
    }
    public Category Category { get; private set; }
    public Enums.Type Type { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public List<Person> Attendees { get; set; }


    public Meeting()
    {
        _name = new Name();
        _description = new Description();

        ResponsiblePerson = new Person();
        Attendees = new List<Person>();
    }
    public Result setName(string name)
    {
        Result result;

        result = _name.setName(name);

        return result;
    }

    public Result setDescription(string description)
    {
        Result result;

        result = _description.setDescription(description);

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

    public string getName() { return Name.Value; }
}