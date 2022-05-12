using Application.Core;
using Application.Helpers;
using Application.Models;
using Application.Users;

namespace Managers;

public class UserManager : ManagerBase

{
    public Person? CurrentUser { get; private set; }

    public UserManager(IServiceProvider services) : base(services) { }

    public Result Login()
    {
        Console.WriteLine("Enter your username:");
        var username = BetterConsole.ReadLineAndClear();

        var result = Handle(new Login.Command() { Name = username });

        if (!result.IsSuccess)
            return Result.Failure(result.Error);

        CurrentUser = result.Value;
        return Result.Success();

    }
}