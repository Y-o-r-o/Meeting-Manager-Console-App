using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;

namespace Application.Meetings
{
    public class List
    {
        private DataContext _dataContext;

        public List(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Result Handle()
        {
            List<Meeting> selectedMeetings = new(_dataContext.Meetings);
            string input;

            Console.WriteLine("Type fragments from description to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingsByDescription(input);

            Console.WriteLine("Type responsible person name to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingsByResponsiblePerson(input);

            Console.WriteLine("Type category to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingByCategory(input);

            Console.WriteLine("Type type to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
                selectedMeetings = selectedMeetings.GetMeetingByType(input);

            Console.WriteLine("Type start date (yyyy/mm/dd) to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
            {
                Result<List<Meeting>> result = selectedMeetings.GetMeetingByStartDate(input);
                if (result.IsSuccess)
                    selectedMeetings = result.Value;
                else return Result.Failure(result.Error);
            }

            Console.WriteLine("Type end date (yyyy/mm/dd) to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
            {
                Result<List<Meeting>> result = selectedMeetings.GetMeetingByEndDate(input);
                if (result.IsSuccess)
                    selectedMeetings = result.Value;
                else return Result.Failure(result.Error);
            }

            Console.WriteLine("Type attendees count to filter data. Or type * to select all.");
            input = BetterConsole.readLine();
            if (!input.Equals("*"))
            {
                Result<List<Meeting>> result = selectedMeetings.GetMeetingByAttendeesCount(input);
                if (result.IsSuccess)
                    selectedMeetings = result.Value;
                else return Result.Failure(result.Error);
            }

            selectedMeetings.Print();
            BetterConsole.waitForKeypress();

            return Result.Success();
        }
    }
}