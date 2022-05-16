using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Meetings.Core;
using Application.Models;
using MediatR;

namespace Application.Meetings;
public class List
{
    public class Command : IRequest<Result>
    {
        public IEnumerable<FilterCriteria> Criteries { get; set; }
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
            IEnumerable<Meeting> selectedMeetings = new List<Meeting>(_dataContext.Meetings);

            foreach (var crit in request.Criteries)
                selectedMeetings = crit.Filter(selectedMeetings);

            selectedMeetings.Print();
            BetterConsole.WaitForKeypress();

            return Task.FromResult(Result.Success());
        }
    }
}


