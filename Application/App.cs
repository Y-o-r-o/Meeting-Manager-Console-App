using Application.Core;
using Application.Helpers;
using Managers;
using MediatR;

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

    private MeetingManager meetingManager;
    private UserManager userManager;

    public App(IServiceProvider services)
    {
        userManager = new UserManager(services);
        meetingManager = new MeetingManager(services);
    }

    public void Run()
    {
        appIsRuning = true;

        BetterConsole.WaitForKeypress(WELCOME_MESSAGE);

        while (userManager.CurrentUser is null)
        {
            var key = BetterConsole.ResponseWithKeyPress("Press any key to login, or 'q' to quit app.");
            if (key.Equals('q'))
                return;
            userManager.Login();
        }

        while (appIsRuning)
            RunMenu();

    }

    public void RunMenu()
    {
        Console.Clear();

        foreach (string menuItem in MENU_ITEMS)
            Console.WriteLine(menuItem);
        Console.Write("Type number to select or 'q' to exit app: ");

        switch (BetterConsole.ReadLineAndClear())
        {
            case "1":
                HandleResult(meetingManager.CreateMeeting(userManager.CurrentUser)); return;
            case "2":
                HandleResult(meetingManager.DeleteMeeting(userManager.CurrentUser)); return;
            case "3":
                HandleResult(meetingManager.AddAPersonToMeeting()); return;
            case "4":
                HandleResult(meetingManager.RemoveAPersonFromMeeting()); return;
            case "5":
                HandleResult(meetingManager.ListAllMeetings()); return;
            case "q":
                appIsRuning = false; return;
            default:
                Console.WriteLine("Selection isn't valid."); return;
        }
    }

    private void HandleResult(Result result)
    {
        result = meetingManager.CreateMeeting(userManager.CurrentUser);
        if (!result.IsSuccess)
            BetterConsole.WaitForKeypress(result.Error);
    }
}