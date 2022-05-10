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
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = _dataContext.Meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Task.FromResult(Result.Failure(result.Error));

            var meeting = result.Value;

            if (meeting.ResponsiblePerson == request.Creator)
            {
                _dataContext.Meetings.Remove(meeting);
                _dataContext.saveChanges();

                Console.WriteLine("Meeting is deleted.");
                return Task.FromResult(Result.Success());
            }

            return Task.FromResult(Result.Failure("You can't delete this meeting, because you are not responsible for it."));
        }

    }
}