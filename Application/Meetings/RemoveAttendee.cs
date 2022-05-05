using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;

namespace Application.Meetings
{
    public class RemoveAttendee
    {
        private DataContext _dataContext;

        public RemoveAttendee(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Result Handle()
        {
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = _dataContext.Meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Result.Failure(result.Error);

            var meeting = result.Value;


            Console.WriteLine("Enter name of attendee you want to remove: ");
            Name name;
            try
            {
                name = new Name(BetterConsole.readLine());
            }
            catch (ArgumentException ex)
            {
                return Result.Failure(ex.ToString());
            }

            if (meeting.ResponsiblePerson is not null && meeting.ResponsiblePerson.Username.Equals(name))
                return Result.Failure("You can't remove responsible person from meeting.");

            List<Person> meetingAttendees = meeting.Attendees;

            if (meetingAttendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is null)
                return Result.Failure("This person doesn't exist in this meeting.");

            meetingAttendees.Remove(new Person(name));
            _dataContext.saveChanges();

            Console.WriteLine("Person successfully removed. ");
            return Result.Success();
        }
    }
}