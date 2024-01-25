public class Test {
    private static Accounts accounts;
    public static void TestAccounts() {
        accounts = new Accounts(new TestUserInputProvider());
        PrinterHelper.PrintMessage("Running Accounts Tests... ", 0, false);

        PrinterHelper.PrintMessage("Testing creating accounts... ", 2, false);

        PrinterHelper.PrintTestResults(TestCreatingAccounts());
        
        PrinterHelper.PrintMessage("Testing logging in... ", 0, false);

        PrinterHelper.PrintTestResults(TestLoggingIn());
        

    }
    
    public static void TestReservations() {
        HotelSystem hotelSystem = new HotelSystem(new TestUserInputProvider(), "test");
    }
    public static void Testing(bool result) {
        if (result) {
            PrinterHelper.PrintSameLine("Success", true);
        }
        else {
            PrinterHelper.PrintSameLine("Test failed", true);
        }
    }
    private static bool TestCreatingAccounts() {
        for (int i = 0; i < 3; i++) {
            if (!accounts.CreateUser()) {
                return false;
            }
            accounts.Logout();
        }
        return true;
    }
    private static bool TestLoggingIn() {
        for (int i = 0; i < 3; i++)
        {
            if (!accounts.Login()) {
                return false;
            }
            accounts.Logout();
        }
        return true;
    }
}