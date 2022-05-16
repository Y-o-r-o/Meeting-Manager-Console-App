using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings;
public class Delete
{
    public class Command : IRequest<Result>
    {
        public Person? Creator { get; set; }
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
                meeting = _dataContext.Meetings.GetByName(request.Name);
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.Message));
            }

            if (meeting is null) return Task.FromResult(Result.Failure("Meeting not found."));

            if (meeting.ResponsiblePerson == request.Creator)
            {
                _dataContext.Meetings.Remove(meeting);
                _dataContext.SaveChanges();

                Console.WriteLine("Meeting is deleted.");
                return Task.FromResult(Result.Success());
            }

            return Task.FromResult(Result.Failure("You can't delete this meeting, because you are not responsible for it."));
        }

    }
}