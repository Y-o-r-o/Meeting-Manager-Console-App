using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings;
public class RemoveAttendee
{
    public class Command : IRequest<Result>
    {
        public string Meeting { get; set; }
        public string Name { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result>
    {

        private DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            Meeting? meeting;
            try
            {
                meeting = _dataContext.Meetings.GetByName(request.Meeting);
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.Message));
            }

            if (meeting is null) return Task.FromResult(Result.Failure("Meeting not found."));

            Name name = new Name(request.Name);
            if (meeting.ResponsiblePerson is not null && meeting.ResponsiblePerson.Username.Equals(name))
                return Task.FromResult(Result.Failure("You can't remove responsible person from meeting."));

            List<Person> meetingAttendees = meeting.Attendees;

            if (meetingAttendees.FirstOrDefault(attendee => attendee.Username.Equals(name)) is null)
                return Task.FromResult(Result.Failure("This person doesn't exist in this meeting."));

            meetingAttendees.Remove(new Person(name));
            _dataContext.SaveChanges();

            Console.WriteLine("Person successfully removed. ");
            return Task.FromResult(Result.Success());
        }
    }
}