using Application.Core;
using Application.Helpers;
using Managers;

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
        userManager = new UserManager();
        meetingManager = new MeetingManager(services);
    }

    public void run()
    {
        appIsRuning = true;

        BetterConsole.writeLineAndWaitForKeypress(WELCOME_MESSAGE);

        while (userManager.CurrentUser is null && appIsRuning)
            login();

        while (appIsRuning)
        {
            Console.Clear();
            runMenu();
        }

    }

    public void runMenu()
    {
        Result result;

        foreach (string menuItem in MENU_ITEMS)
            Console.WriteLine(menuItem);
        Console.Write("Type number to select or 'q' to exit app: ");
        switch (BetterConsole.readLineAndClear())
        {
            case "1":
                result = meetingManager.createMeeting(userManager.CurrentUser);
                if (!result.IsSuccess)
                    BetterConsole.writeLineAndWaitForKeypress(result.Error);
                break;
            case "2":
                result = meetingManager.deleteMeeting(userManager.CurrentUser);
                if (!result.IsSuccess)
                    BetterConsole.writeLineAndWaitForKeypress(result.Error);
                break;
            case "3":
                result = meetingManager.addAPersonToMeeting();
                if (!result.IsSuccess)
                    BetterConsole.writeLineAndWaitForKeypress(result.Error);
                break;
            case "4":
                result = meetingManager.removeAPersonFromMeeting();
                if (!result.IsSuccess)
                    BetterConsole.writeLineAndWaitForKeypress(result.Error);
                break;
            case "5":
                result = meetingManager.listAllMeetings();
                if (!result.IsSuccess)
                    BetterConsole.writeLineAndWaitForKeypress(result.Error);
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
        Console.WriteLine("Enter your username:");
        Result result = userManager.login(BetterConsole.readLine());
        if (!result.IsSuccess)
            askToTryAgain(result);
    }

    private void askToTryAgain(Result problem)
    {
        Console.WriteLine(problem.Error);
        Console.WriteLine("Press any key to try again, or 'q' to exit application.");
        if (Console.ReadKey().Equals("q"))
            appIsRuning = false;
    }

    
}