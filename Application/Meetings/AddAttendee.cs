using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings;

public class AddAttendee
{
    public class Command : IRequest<Result> { }
    public class Handler : IRequestHandler<Command, Result>
    {

        private DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = _dataContext.Meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Task.FromResult(Result.Failure(result.Error));

            var meeting = result.Value;

            Console.WriteLine("Enter name of attendee you want to add: ");
            Name name;
            try
            {
                name = new Name(BetterConsole.readLine());
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.ToString()));
            }

            if (meeting.ResponsiblePerson is not null && meeting.ResponsiblePerson.Username.Equals(name))
                return Task.FromResult(Result.Failure("You can't add responsible person as attendee."));

            if (meeting.Attendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is not null)
                return Task.FromResult(Result.Failure("Person is already in this meeting."));

            warnIfMeetingsIntersects(name, meeting);

            meeting.Attendees.Add(new Person(name));
            _dataContext.saveChanges();

            return Task.FromResult(Result.Success());
        }

        private void warnIfMeetingsIntersects(Name name, Meeting meeting)
        {
            var sameNameMeetings = _dataContext.Meetings.FindAll(meeting => meeting.Attendees.Any(attendee => attendee.Username.Equals(name)));
            Meeting? intersectingMeeting;
            try
            {
                intersectingMeeting = sameNameMeetings.FirstOrDefault(m => m.FromToDateTime.StartDate < meeting.FromToDateTime.EndDate &&
                    meeting.FromToDateTime.StartDate < m.FromToDateTime.EndDate);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Meeting arrangment datetime is null.");
            }

            if (intersectingMeeting is null)
                return;
            Console.WriteLine($"Warning: Meeting {meeting.Name} intersects with {intersectingMeeting.Name}");
        }
    }
}
