using Application.Core;
using Application.Helpers;
using Application.Models;
using MediatR;
using Type = Application.Models.Type;

namespace Application.Meetings;

public class Create
{
    public class Command : IRequest<Result>
    {
        public Person? Creator { get; set; }
        public string Description{get; set;}
        public string Name{get; set;}
        public string Category{get; set;}
        public string Type{get; set;}
        public string StartDate{get; set;}
        public string EndDate{get; set;}
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
            Meeting meeting = new Meeting();

            meeting.ResponsiblePerson = request.Creator;

            try
            {
                meeting.Name = new Name(request.Name);
                meeting.Description = new Description(request.Description);
                meeting.Category = new Category(request.Category);
                meeting.Type = new Type(request.Type);
                meeting.FromToDateTime = new FromToDateTime(request.StartDate, request.EndDate);
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult(Result.Failure(ex.Message));
            }

            _dataContext.Meetings.Add(meeting);
            _dataContext.SaveChanges();

            BetterConsole.WaitForKeypress("Meeting successfully added.");
            return Task.FromResult(Result.Success());
        }

    }
}