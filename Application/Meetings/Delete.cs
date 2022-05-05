using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;

namespace Application.Meetings
{
    public class Delete
    {
        private DataContext _dataContext;

        public Delete(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Result Handle(Person creator)
        {
            Console.WriteLine("Enter name of meeting you want to select: ");
            Result<Meeting> result = _dataContext.Meetings.GetMeetingByName(BetterConsole.readLine());

            if (!result.IsSuccess)
                return Result.Failure(result.Error);

            var meeting = result.Value;

            if (meeting.ResponsiblePerson == creator)
            {
                _dataContext.Meetings.Remove(meeting);
                _dataContext.saveChanges();

                Console.WriteLine("Meeting is deleted.");
                return Result.Success();
            }

            return Result.Failure("You can't delete this meeting, because you are not responsible for it.");
        }
    }
}