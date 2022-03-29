using Application;
using Application.Core;
using Application.Extensions;
using Application.Helpers;
using Application.Models;

public class App
{
    const string WELCOME_MESSAGE = "Wectome to internal meetings manager.";

    readonly string[] MENU_ITEMS = new string[]
    {
        "1) Create a new meeting",
        "2) Delete a meeting",
        "3) Add a person to the meeting",
        "4) Remove a person from the meeting",
        "5) List all the meetings"};

    private bool appIsRuning = false;

    private UserManager userManager;
    private Serializer<Meeting> meetingsSerializer;
    private List<Meeting> meetings;

    public App()
    {
        userManager = new UserManager();
        meetings = new List<Meeting>();
        meetingsSerializer = new Serializer<Meeting>();
    }

    public void run()
    {
        loadMeetings();

        appIsRuning = true;

        BetterConsole.WriteLineAndWaitForKeypress(WELCOME_MESSAGE);

        login();

        while (appIsRuning)
        {
            Console.Clear();
            runMenu();
        }

    }

    public void loadMeetings()
    {
        Result<List<Meeting>> result = meetingsSerializer.deserialize();

        if (result.IsSuccess)
        {
            meetings = result.Value;
            return;
        }
        Console.WriteLine(result.Error);
    }

    public void runMenu()
    {
        Result result;

        foreach (string menuItem in MENU_ITEMS)
            Console.WriteLine(menuItem);
        Console.Write("Type number to select or 'q' to exit app: ");
        switch (BetterConsole.ReadLineAndClear())
        {
            case "1":
                result = createMeeting();
                if (!result.IsSuccess)
                    BetterConsole.WriteLineAndWaitForKeypress(result.Error);
                break;
            case "2":
                result = deleteMeeting();
                if (!result.IsSuccess)
                    BetterConsole.WriteLineAndWaitForKeypress(result.Error);
                break;
            case "3":
                result = addAPersonToMeeting();
                if (!result.IsSuccess)
                    BetterConsole.WriteLineAndWaitForKeypress(result.Error);
                break;
            case "4":
                result = removeAPersonFromMeeting();
                if (!result.IsSuccess)
                    BetterConsole.WriteLineAndWaitForKeypress(result.Error);
                break;
            case "5":
                result = listAllMeetings();
                if (!result.IsSuccess)
                    BetterConsole.WriteLineAndWaitForKeypress(result.Error);
                break;
            case "q":
                appIsRuning = false;
                break;
            default:
                Console.WriteLine("Selection isn't valid.");
                break;
        }

    }

    private void login()
    {
        while (!userManager.login().IsSuccess && appIsRuning)
            askToTryAgain();
        Console.Clear();
    }

    private void askToTryAgain()
    {
        Console.WriteLine("Press any key to try again, or 'q' to exit application.");
        if (Console.ReadKey().Equals("q"))
            appIsRuning = false;
    }

    private Result createMeeting()
    {
        Meeting meeting = new Meeting();
        Result result;

        Console.WriteLine("Enter meeting name: ");
        result = meeting.setName(BetterConsole.ReadLine());
        if (!result.IsSuccess) return result;

        meeting.ResponsiblePerson = userManager.User;

        Console.WriteLine("Enter meeting description: ");
        result = meeting.setDescription(BetterConsole.ReadLine());
        if (!result.IsSuccess) return result;

        Console.WriteLine("Enter category: ");
        result = meeting.setCategory(BetterConsole.ReadLine());
        if (!result.IsSuccess) return result;

        Console.WriteLine("Enter type: ");
        result = meeting.setType(BetterConsole.ReadLine());
        if (!result.IsSuccess) return result;

        Console.WriteLine("Enter start date: ");
        result = meeting.setStartDate(BetterConsole.ReadLine());
        if (!result.IsSuccess) return result;

        Console.WriteLine("Enter end date: ");
        result = meeting.setEndDate(BetterConsole.ReadLine());
        if (!result.IsSuccess) return result;

        storeMeeting(meeting);

        BetterConsole.WriteLineAndWaitForKeypress("Meeting successfully added.");
        return Result.Success();
    }

    public void storeMeeting(Meeting meeting)
    {
        meetings.Add(meeting);
        meetingsSerializer.serialize(meetings);
    }



    private Result deleteMeeting()
    {
        Console.WriteLine("Enter name of meeting you want to select: ");
        Result<Meeting> result = meetings.GetMeetingByName(BetterConsole.ReadLine());

        if (!result.IsSuccess)
            return Result.Failure(result.Error);

        var meeting = result.Value;

        if (meeting.ResponsiblePerson == userManager.User)
        {
            meetings.Remove(meeting);
            meetingsSerializer.serialize(meetings);

            Console.WriteLine("Meeting is deleted.");
            return Result.Success();
        }

        return Result.Failure("You can't delete this meetin, because you are not responsible for it.");
    }

    private Result addAPersonToMeeting()
    {
        Console.WriteLine("Enter name of meeting you want to select: ");
        Result<Meeting> result = meetings.GetMeetingByName(BetterConsole.ReadLine());

        if (!result.IsSuccess)
            return Result.Failure(result.Error);

        var meeting = result.Value;

        Name name = new Name();

        Console.WriteLine("Enter name of attendee you want to add: ");
        Result typeNameResult = name.setName(BetterConsole.ReadLine());

        if (!typeNameResult.IsSuccess)
            return Result.Failure(typeNameResult.Error);

        if (meeting.ResponsiblePerson.getName().Equals(name.Value))
            return Result.Failure("You can't add responsible person as attendee.");

        List<Person> meetingAttendees = meeting.Attendees;

        if (meetingAttendees.FirstOrDefault(attendee => attendee.getName().Equals(name.Value)) is not null)
            return Result.Failure("Person is already in this meeting.");

        warnIfMeetingsIntersects(name, meeting);

        meetingAttendees.Add(new Person() { Username = name });
        meetingsSerializer.serialize(meetings);

        return Result.Success();
    }

    private void warnIfMeetingsIntersects(Name name, Meeting meeting)
    {
        foreach (Meeting meetingContainingAttendee in meetings.FindAll(meeting =>
         meeting.Attendees.Any(attendee => attendee.getName().Equals(name.Value))))
        {
            if (meetingContainingAttendee.StartDate < meeting.EndDate &&
            meeting.StartDate < meetingContainingAttendee.EndDate)
                Console.WriteLine(
                    $"Warning: Meeting {meeting.getName()} intersects with {meetingContainingAttendee.getName()}"
                    );
        }
    }
    private Result removeAPersonFromMeeting()
    {
        Console.WriteLine("Enter name of meeting you want to select: ");
        Result<Meeting> result = meetings.GetMeetingByName(BetterConsole.ReadLine());

        if (!result.IsSuccess)
            return Result.Failure(result.Error);

        var meeting = result.Value;

        Name name = new Name();

        Console.WriteLine("Enter name of attendee you want to remove: ");
        Result typeNameResult = name.setName(BetterConsole.ReadLine());

        if (!typeNameResult.IsSuccess)
            return Result.Failure(typeNameResult.Error);

        if (meeting.ResponsiblePerson.getName().Equals(name.Value))
            return Result.Failure("You can't remove responsible personfrom meeting.");

        List<Person> meetingAttendees = meeting.Attendees;

        if (meetingAttendees.FirstOrDefault(attendee => attendee.getName().Equals(name.Value)) is null)
            return Result.Failure("This person doesn't exist in this meeting.");

        meetingAttendees.Remove(new Person() { Username = name });
        meetingsSerializer.serialize(meetings);

        Console.WriteLine("Person successfully removed. ");
        return Result.Success();
    }

    private Result listAllMeetings()
    {
        List<Meeting> selectedMeetings = meetings;
        string input;

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
        {
            Result<List<Meeting>> result = selectedMeetings.GetMeetingByStartDate(input);
            if(result.IsSuccess)
                selectedMeetings = result.Value;
            else return Result.Failure(result.Error);
        }

        Console.WriteLine("Type end date (yyyy/mm/dd) to filter data. Or type * to select all.");
        input = BetterConsole.ReadLine();
        if (!input.Equals("*"))
        {
            Result<List<Meeting>> result = selectedMeetings.GetMeetingByEndDate(input);
            if(result.IsSuccess)
                selectedMeetings = result.Value;
            else return Result.Failure(result.Error);
        }

        Console.WriteLine("Type attendees count to filter data. Or type * to select all.");
        input = BetterConsole.ReadLine();
        if (!input.Equals("*"))
        {
           Result<List<Meeting>> result = selectedMeetings.GetMeetingByAttendeesCount(input);
            if(result.IsSuccess)
                selectedMeetings = result.Value;
            else return Result.Failure(result.Error);
        }

        selectedMeetings.Print();
        BetterConsole.WaitForKeypress();

        return Result.Success();
    }

}