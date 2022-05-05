namespace Application.Helpers
{
    public static class BetterConsole
    {
        public static string readLine()
        {
            string? str = Console.ReadLine();
            if (str is null) return string.Empty;
            return str;
        }

        public static string readLineAndClear()
        {
            string str = readLine();
            Console.Clear();
            return str;
        }

        public static char ReadKey()
        {
            var key = Console.ReadKey();
            return key.KeyChar;
        }
        public static void writeLineAndWaitForKeypress(string stringToWrite)
        {
            Console.WriteLine(stringToWrite);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void waitForKeypress()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}