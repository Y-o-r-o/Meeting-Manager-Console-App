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

    public Result CreateMeeting(Person creator)
    {
        return Handle(new Create.Command() { Creator = creator });
    }

    public Result DeleteMeeting(Person creator)
    {
        return Handle(new Delete.Command() { Creator = creator });
    }

    public Result AddAPersonToMeeting()
    {
        return Handle(new AddAttendee.Command());
    }

    public Result RemoveAPersonFromMeeting()
    {
        return Handle(new Create.Command());
    }

    public Result ListAllMeetings()
    {
        return Handle(new Create.Command());
    }

}