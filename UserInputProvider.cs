public interface IUserInputProvider {
    string ReadLine(string text, int margin);
    void PrintMessage(string text, int margin, bool pause, string? text2 = null);
    void ClearScreen();
}

public class ConsoleUserInputProvider : IUserInputProvider {
    public string ReadLine(string text, int margin) {
        return PrinterHelper.AskForInput(text, margin);
    }
    public void PrintMessage(string text, int margin, bool pause, string? text2 = null) {
        PrinterHelper.PrintMessage(text, margin, pause, text2);
    }
    public void ClearScreen() {
        PrinterHelper.ClearScreen();
    }
}

public class TestUserInputProvider : IUserInputProvider {
    private readonly string[] testStrings = {"asdfg", "12345", "!@#%^"};
    private readonly string[] testRooms = {"3", "-5", "5000", "asdfg"};
    private int index = 0;

    public string ReadLine(string text, int margin) {
        string testString;
        //testing Reservations
        if (text.Equals("Podaj numer pokoju")) {
            testString = testRooms[index];
            index++;
            if (index >= testRooms.Length) index = 0;
        }
        //testing accounts
        else {
            testString = testStrings[index];
            index++;
            if (index >= testStrings.Length) index = 0;
        }
        return testString;
    }

    public void PrintMessage(string text, int margin, bool pause, string? text2 = null) {
        return;
    }
    public  void ClearScreen() {
        return;
    }
}