using Application.Models;

namespace Application.Extensions;

public static class MeetingListExtensions
{
    public static Meeting? GetByName(this IEnumerable<Meeting> meetings, string name)
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

    public static void Print(this IEnumerable<Meeting> meetings)
    {
        int count = 1;
        foreach (Meeting meeting in meetings)
        {
            Console.WriteLine(count.ToString() + ". " + meeting.Name);
            count++;
        }
    }
}
