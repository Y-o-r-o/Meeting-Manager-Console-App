using Application.Core;
using Application.Extensions;
using Application.Meetings;
using Application.Models;

namespace Managers;
public class MeetingManager : ManagerBase
{
    private List<Meeting> meetings;

    public MeetingManager(IServiceProvider services) : base(services)
    {
        if (meetings is null) this.meetings = new();
    }

    public Result createMeeting(Person creator)
    {
        return Handle(new Create.Command() { Creator = creator });
    }


    public Result deleteMeeting(Person creator)
    {
        return Handle(new Delete.Command() { Creator = creator });
    }

    public Result addAPersonToMeeting()
    {
        return Handle(new AddAttendee.Command());
    }

    public Result removeAPersonFromMeeting()
    {
        return Handle(new Create.Command());
    }

    public Result listAllMeetings()
    {
        return Handle(new Create.Command());
    }

}