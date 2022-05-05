using Application.Core;
using Application.Helpers;
using Application.Models;
using MediatR;

namespace Application.Meetings
{
    public class Create
    {
        public class Command : IRequest<Result>
        {
            public Person? Creator { get; set; }
        }
        public class Handler : Core.IRequestHandler<Command, Result>
        {

            private DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public Result Handle(Command command)
            {
                Meeting meeting = new Meeting();

                meeting.ResponsiblePerson = command.Creator;

                try
                {
                    Console.WriteLine("Enter meeting name: ");
                    meeting.Name = new Name(BetterConsole.readLine());

                    Console.WriteLine("Enter meeting description: ");
                    meeting.Description = new Description(BetterConsole.readLine());

                    Console.WriteLine("Enter category: ");
                    meeting.Category = new Category(BetterConsole.readLine());

                    Console.WriteLine("Enter type: ");
                    meeting.Type = new Application.Models.Type(BetterConsole.readLine());

                    Console.WriteLine("Enter start date: ");
                    string startDate = BetterConsole.readLine();
                    Console.WriteLine("Enter end date: ");
                    string endDate = BetterConsole.readLine();
                    meeting.FromToDateTime = new FromToDateTime(startDate, endDate);
                }
                catch (ArgumentException ex)
                {
                    return Result.Failure(ex.ToString());
                }

                _dataContext.Meetings.Add(meeting);
                _dataContext.saveChanges();

                BetterConsole.writeLineAndWaitForKeypress("Meeting successfully added.");
                return Result.Success();
            }


        }
    }
}