namespace Application.Helpers;

public static class BetterConsole
{
    public static string ReadLine()
    {
        string? str = Console.ReadLine();
        if (str is null) return string.Empty;
        return str;
    }

    public static string ReadLineAndClear()
    {
        string str = ReadLine();
        Console.Clear();
        return str;
    }

    public static char ReadKey()
    {
        var key = Console.ReadKey();
        return key.KeyChar;
    }
    public static char ResponseWithKeyPress(string? message = null)
    {
        if (message is not null) Console.WriteLine(message);
        var key = Console.ReadKey().KeyChar;
        Console.Clear();
        return key;
    }

    public static void WaitForKeypress(string? message = null)
    {
        if (message is not null) Console.WriteLine(message);
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }
}