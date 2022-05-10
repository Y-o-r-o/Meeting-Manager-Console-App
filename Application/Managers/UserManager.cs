using Application.Core;
using Application.Models;

namespace Managers;

public class UserManager

{
    public Person? CurrentUser { get; private set; }

    public Result login(string loginStr)
    {
        try
        {
            Name username = new Name(loginStr);
            CurrentUser = new Person(username);
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(ex.ToString());
        }

        return Result.Success();
    }
}