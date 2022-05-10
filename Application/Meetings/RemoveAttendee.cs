using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings;
public class RemoveAttendee
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


            Console.WriteLine("Enter name of attendee you want to remove: ");
            Name name = new Name(BetterConsole.readLine());

            if (meeting.ResponsiblePerson is not null && meeting.ResponsiblePerson.Username.Equals(name))
                return Task.FromResult(Result.Failure("You can't remove responsible person from meeting."));

            List<Person> meetingAttendees = meeting.Attendees;

            if (meetingAttendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is null)
                return Task.FromResult(Result.Failure("This person doesn't exist in this meeting."));

            meetingAttendees.Remove(new Person(name));
            _dataContext.saveChanges();

            Console.WriteLine("Person successfully removed. ");
            return Task.FromResult(Result.Success());
        }
    }
}