using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings;
public class List
{
    public class Command : IRequest<Result>
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AttendeeCount { get; set; }
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
            List<Meeting> selectedMeetings = new(_dataContext.Meetings);
            string input;

            try
            {
                input = request.Description;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterDescription(input);

                input = request.Name;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterResponsiblePerson(input);

                input = request.Category;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterCategory(input);

                input = request.Type;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterType(input);

                input = request.StartDate;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterStartDate(input);

                input = request.EndDate;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterEndDate(input);

                input = request.AttendeeCount;
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.FilterAttendeesCount(input);
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.Message));
            }

            selectedMeetings.Print();
            BetterConsole.WaitForKeypress();

            return Task.FromResult(Result.Success());
        }
    }
}

