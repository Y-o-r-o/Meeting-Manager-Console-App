using Application.Extensions;
using Application.Models;

namespace Application.Meetings.Core;



public abstract class FilterCriteria
{
    public string Input { get; set; }

    public abstract IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings);
}

public class DescriptionFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        return from meeting in meetings where meeting.Description.Value.Contains(Input) select meeting;
    }
}

public class ResponsiblePersonFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        return from meeting in meetings where meeting.ResponsiblePerson.Username.Equals(Input) select meeting;
    }
}

public class CategoryFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        return from meeting in meetings where meeting.Category.Equals(Input) select meeting;
    }
}

public class TypeFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        return from meeting in meetings where meeting.Type.Equals(Input) select meeting;
    }
}

public class StartDateFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        var validStartDate = Input.TryParseToDateOnly();
        if (!validStartDate.IsSuccess)
            throw new ArgumentException("Wrong date format.");
        return from meeting in meetings where DateOnly.FromDateTime(meeting.FromToDateTime.StartDate) >= validStartDate.Value select meeting;
    }
}

public class EndDateFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        var validEndDate = Input.TryParseToDateOnly();
        if (!validEndDate.IsSuccess)
            throw new ArgumentException("Wrong date format.");
        return from meeting in meetings where DateOnly.FromDateTime(meeting.FromToDateTime.EndDate) <= validEndDate.Value select meeting;
    }
}

public class AttendeesCountFilterCriteria : FilterCriteria
{
    public override IEnumerable<Meeting> Filter(IEnumerable<Meeting> meetings)
    {
        int count;
        if (Int32.TryParse(Input, out count))
            throw new ArgumentException("Can't parse non-number symbols.");
        return from meeting in meetings where meeting.Attendees.Count == count select meeting;
    }
}
