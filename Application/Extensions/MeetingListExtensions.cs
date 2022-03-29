using Application.Core;
using Application.Helpers;
using Application.Models;

namespace Application.Extensions
{
    public static class MeetingListExtensions
    {
        public static Result<Meeting> GetMeetingByName(this List<Meeting> meetings, string name)
        {
            Name validName = new Name();

            Result result = validName.setName(name);
            if (!result.IsSuccess)
                return Result<Meeting>.Failure(result.Error);

            Meeting? meeting = meetings.FirstOrDefault(meeting => meeting.Name == validName);

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
        public static List<Meeting> GetMeetingByStartDate(this List<Meeting> meetings, Result<DateOnly> startDate)
        {
            return meetings.FindAll(meeting => DateOnly.FromDateTime(meeting.StartDate) >= startDate.Value);
        }
        public static List<Meeting> GetMeetingByEndDate(this List<Meeting> meetings, Result<DateOnly> endDate)
        {
            return meetings.FindAll(meeting => DateOnly.FromDateTime(meeting.EndDate) <= endDate.Value);
        }

        public static List<Meeting> GetMeetingByAttendeesCount(this List<Meeting> meetings, int count)
        {
            return meetings.FindAll(meeting => meeting.Attendees.Count == count);
        }
    }
}