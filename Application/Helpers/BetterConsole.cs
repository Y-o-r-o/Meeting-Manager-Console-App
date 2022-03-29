namespace Application.Helpers
{
    public static class BetterConsole
    {
        public static string ReadLine (){
            string? str = Console.ReadLine();
            if(str is null) return string.Empty;
            return str;
        }

        public static string ReadLineAndClear (){
            string str = ReadLine();
            Console.Clear();
            return str;
        }

        public static void WriteLineAndWaitForKeypress (string stringToWrite){
            Console.WriteLine(stringToWrite);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void WaitForKeypress (){
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}