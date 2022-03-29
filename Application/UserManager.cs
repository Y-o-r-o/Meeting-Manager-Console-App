using Application.Core;
using Application.Helpers;
using Application.Models;

public class UserManager
{
    public Person User { get; private set; } = new Person();

    public Result login()
    {
        Console.Clear();
        Console.WriteLine("Enter your username:");

        string enteredUsername = BetterConsole.ReadLine();
        
        Name username = new Name();
        var result = username.setName(enteredUsername);

        if (!result.IsSuccess)
            BetterConsole.WriteLineAndWaitForKeypress(result.Error);

        User = new Person(){ Username = username };

        return result;
    }
}