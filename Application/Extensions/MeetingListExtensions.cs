using Application.Core;
using Application.Helpers;
using Application.Models;

namespace Application.Extensions
{
    public static class MeetingListExtensions
    {
        public static Result<Meeting> GetMeetingByName(this List<Meeting> meetings, string name)
        {
            Name validName;
            
            try
            {
                validName = new Name(name);
            }
            catch (ArgumentException ex)
            {
                return Result<Meeting>.Failure(ex.ToString());
            }

            Meeting? meeting = meetings.FirstOrDefault(meeting => meeting.Name.Equals(name));

            if (meeting is null)
                return Result<Meeting>.Failure("Meeting not found.");

            return Result<Meeting>.Success(meeting);
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
        public static Result<List<Meeting>> GetMeetingByStartDate(this List<Meeting> meetings, string startDate)
        {
            var validStartDate = startDate.TryParseToDateOnly();
            if (!validStartDate.IsSuccess)
                return Result<List<Meeting>>.Failure("Entered wrong date format.");
            return Result<List<Meeting>>.Success(
                meetings.FindAll(meeting => DateOnly.FromDateTime(meeting.FromToDateTime.StartDate) >= validStartDate.Value));
        }
        public static Result<List<Meeting>> GetMeetingByEndDate(this List<Meeting> meetings, string endDate)
        {
            var validEndDate = endDate.TryParseToDateOnly();
            if (!validEndDate.IsSuccess)
                return Result<List<Meeting>>.Failure("Entered wrong date format.");
            return Result<List<Meeting>>.Success(
                meetings.FindAll(meeting => DateOnly.FromDateTime(meeting.FromToDateTime.EndDate) <= validEndDate.Value));
        }

        public static Result<List<Meeting>> GetMeetingByAttendeesCount(this List<Meeting> meetings, string countStr)
        {
            int count;
            if (Int32.TryParse(countStr, out count))
                return Result<List<Meeting>>.Failure("You must enter numbers only.");
            return Result<List<Meeting>>.Success(
            meetings.FindAll(meeting => meeting.Attendees.Count == count));
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
}