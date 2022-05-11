using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings;
public class List
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
            List<Meeting> selectedMeetings = new(_dataContext.Meetings);
            string input;

            try
            {
                Console.WriteLine("Type fragments from description to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingsByDescription(input);

                Console.WriteLine("Type responsible person name to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingsByResponsiblePerson(input);

                Console.WriteLine("Type category to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingByCategory(input);

                Console.WriteLine("Type type to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingByType(input);

                Console.WriteLine("Type start date (yyyy/mm/dd) to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingByStartDate(input);

                Console.WriteLine("Type end date (yyyy/mm/dd) to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingByEndDate(input);

                Console.WriteLine("Type attendees count to filter data. Or type * to select all.");
                input = BetterConsole.ReadLine();
                if (!input.Equals("*"))
                    selectedMeetings = selectedMeetings.GetMeetingByAttendeesCount(input);
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