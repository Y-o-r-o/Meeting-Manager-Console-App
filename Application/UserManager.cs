using Application.Core;
using Application.Helpers;
using Application.Models;

public class UserManager
{
    public Person? CurrentUser { get; set; }

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