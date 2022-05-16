using Application.Core;
using Application.Helpers;
using Application.Meetings;
using Application.Meetings.Core;
using Application.Models;

namespace Managers;
public class MeetingManager : ManagerBase
{

    public MeetingManager(IServiceProvider services) : base(services) { }

    public Result CreateMeeting(Person creator)
    {


        Console.WriteLine("Enter meeting name: ");
        var name = BetterConsole.ReadLine();
        Console.WriteLine("Enter meeting description: ");
        var description = BetterConsole.ReadLine();
        Console.WriteLine("Enter category: ");
        var category = BetterConsole.ReadLine();
        Console.WriteLine("Enter type: ");
        var type = BetterConsole.ReadLine();
        Console.WriteLine("Enter start date: ");
        var startDate = BetterConsole.ReadLine();
        Console.WriteLine("Enter end date: ");
        var endDate = BetterConsole.ReadLine();

        return Handle(new Create.Command()
        {
            Creator = creator,
            Description = description,
            Name = name,
            Category = category,
            Type = type,
            StartDate = startDate,
            EndDate = endDate,
        });
    }

    public Result DeleteMeeting(Person creator)
    {
        Console.WriteLine("Enter name of meeting you want to delete: ");
        var name = BetterConsole.ReadLine();

        return Handle(new Delete.Command() { Creator = creator, Name = name });
    }

    public Result AddAPersonToMeeting()
    {
        Console.WriteLine("Enter name of meeting from witch you want add attendee: ");
        var meeting = BetterConsole.ReadLine();
        Console.WriteLine("Enter name of attendee you want to add: ");
        var name = BetterConsole.ReadLine();

        return Handle(new AddAttendee.Command() { Meeting = meeting, Name = name });
    }

    public Result RemoveAPersonFromMeeting()
    {
        Console.WriteLine("Enter name of meeting from witch you want remove attendee: ");
        var meeting = BetterConsole.ReadLine();
        Console.WriteLine("Enter name of attendee you want to remove: ");
        var name = BetterConsole.ReadLine();

        return Handle(new RemoveAttendee.Command() { Meeting = meeting, Name = name });
    }

    public Result ListAllMeetings()
    {
        List<FilterCriteria> criterias = new();

        Console.WriteLine("Type fragments from description to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });
        Console.WriteLine("Type responsible person name to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });
        Console.WriteLine("Type category to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });
        Console.WriteLine("Type type to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });
        Console.WriteLine("Type start date (yyyy/mm/dd) to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });
        Console.WriteLine("Type end date (yyyy/mm/dd) to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });
        Console.WriteLine("Type attendees count to filter data. //Or type * to select all.");
        criterias.Add(new DescriptionFilterCriteria() { Input = BetterConsole.ReadLine() });

        return Handle(new List.Command()
        {
            Criteries = criterias
        });
    }

}