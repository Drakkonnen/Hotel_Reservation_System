class PrinterHelper {
    public static void PrintMessage(string message, int topMargin, bool pause) {
        Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop += topMargin);
        Console.Write(message);
        if (pause) {
            Console.ReadKey();
        }
    }
    public static void PrintMessage(string message, string var, int topMargin, bool pause) {
        Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop += topMargin);
        message = message.Replace("{0}", var);
        Console.Write(message);
        if (pause) {
            Console.ReadKey();
        }
    }
    public static string AskForInput(string message, int topMargin) {
        PrintMessage(message, topMargin, false);
        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.CursorTop += 1);
        string? input = Console.ReadLine();
        while (string.IsNullOrEmpty(input)) {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.CursorTop -= 1);
            input = Console.ReadLine();
        }
        return input;
    }
    public static void PrintRoomsFormat(int i) {
        if (i % 16 == 0) {
            Console.CursorLeft = Console.WindowWidth / 2 - 9;
            Console.CursorTop += 2;
            }
        else if (i % 4 == 0) {
            Console.CursorLeft++;
        }
    }
    public static void SelectColor() {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    public static void UnselectColor() {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void CheckForBoundaries() {
        if (Console.WindowHeight < 10 || Console.WindowWidth < 50) {
            PrintMessage("Za małe okno konsoli", 2, true);
        }
    }
}