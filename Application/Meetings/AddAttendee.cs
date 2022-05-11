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
            Meeting? meeting;
            try
            {
                meeting = _dataContext.Meetings.GetMeetingByName(BetterConsole.ReadLine());
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.Message));
            }

            if(meeting is null) return Task.FromResult(Result.Failure("Meeting not found."));

            Console.WriteLine("Enter name of attendee you want to add: ");
            Name name;
            try
            {
                name = new Name(BetterConsole.ReadLine());
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.Message));
            }

            if (meeting.ResponsiblePerson is not null && meeting.ResponsiblePerson.Username.Equals(name))
                return Task.FromResult(Result.Failure("You can't add responsible person as attendee."));

            if (meeting.Attendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is not null)
                return Task.FromResult(Result.Failure("Person is already in this meeting."));

            WarnIfMeetingsIntersects(name, meeting);

            meeting.Attendees.Add(new Person(name));
            _dataContext.SaveChanges();

            return Task.FromResult(Result.Success());
        }

        private void WarnIfMeetingsIntersects(Name name, Meeting meeting)
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
