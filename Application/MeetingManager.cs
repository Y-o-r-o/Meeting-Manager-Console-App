using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Meetings;
using Application.Models;

namespace Application
{
    public class MeetingManager: ManagerBase
    {
        private List<Meeting> meetings;
        //private JsonSerializer<List<Meeting>> meetingsSerializer;

        public MeetingManager(IServiceProvider services) : base(services)
        {
           // meetingsSerializer = new JsonSerializer<List<Meeting>>() { FileName = "meetings" };
           // meetings = meetingsSerializer.deserialize();

            if (meetings is null) this.meetings = new();
        }

        public Result createMeeting(Person creator)
        {
            return Mediator.SendToHandler(new Create.Command(){ Creator = creator });
        }


        public Result deleteMeeting(Person creator)
        {
            return Result.Success();
        }

        public Result addAPersonToMeeting()
        {
            return Result.Success();
        }

        public Result removeAPersonFromMeeting()
        {
            return Result.Success();
        }

        public Result listAllMeetings()
        {
            return Result.Success();
        }

    }
}