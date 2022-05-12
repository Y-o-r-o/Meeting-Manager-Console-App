using Application.Models;

namespace Application.Extensions;

public static class MeetingListExtensions
{
    public static Meeting? GetMeetingByName(this List<Meeting> meetings, string name)
    {
        Name validName;
        Meeting? meeting;
        try
        {
            validName = new Name(name);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.ToString());
        }
        
        meeting = meetings.FirstOrDefault(meeting => meeting.Name.Equals(validName));

        return meeting;
    }

    public static List<Meeting> GetMeetingsByDescription(this List<Meeting> meetings, string description)
    {
        return meetings.FindAll(meeting => meeting.Description.Value.Contains(description));
    }

    public static List<Meeting> GetMeetingsByResponsiblePerson(this List<Meeting> meetings, string responsiblePersonName)
    {
        return meetings.FindAll(meeting => meeting.ResponsiblePerson.Username.Equals(responsiblePersonName));
    }
    public static List<Meeting> GetMeetingByCategory(this List<Meeting> meetings, string category)
    {
        return meetings.FindAll(meeting => meeting.Category.Equals(category));
    }
    public static List<Meeting> GetMeetingByType(this List<Meeting> meetings, string type)
    {
        return meetings.FindAll(meeting => meeting.Type.Equals(type));
    }
    public static List<Meeting> GetMeetingByStartDate(this List<Meeting> meetings, string startDate)
    {
        var validStartDate = startDate.TryParseToDateOnly();
        if (!validStartDate.IsSuccess)
            throw new ArgumentException("Wrong date format.");
        return meetings.FindAll(meeting => DateOnly.FromDateTime(meeting.FromToDateTime.StartDate) >= validStartDate.Value);
    }
    public static List<Meeting> GetMeetingByEndDate(this List<Meeting> meetings, string endDate)
    {
        var validEndDate = endDate.TryParseToDateOnly();
        if (!validEndDate.IsSuccess)
            throw new ArgumentException("Wrong date format.");
        return meetings.FindAll(meeting => DateOnly.FromDateTime(meeting.FromToDateTime.EndDate) <= validEndDate.Value);
    }

    public static List<Meeting> GetMeetingByAttendeesCount(this List<Meeting> meetings, string countStr)
    {
        int count;
        if (Int32.TryParse(countStr, out count))
            throw new ArgumentException("Can't parse non-number symbols.");
        return meetings.FindAll(meeting => meeting.Attendees.Count == count);
    }

    public static void Print(this List<Meeting> meetings)
    {
        int count = 1;
        foreach (Meeting meeting in meetings)
        {
            Console.WriteLine(count.ToString() + ". " + meeting.Name);
            count++;
        }
    }
}
