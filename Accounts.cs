class Accounts : IUserProvider { 
    User? loggedUser;
    List<User> listOfUsers = new List<User>();

    public void CreateUser() {
        Console.Clear();
        PrinterHelper.PrintMessage("Zakładanie konta", 0, false);
        if (IsLoggedIn()) {
            PrinterHelper.PrintMessage("Już zalogowany", 2, true);
            return;
        }

        string username = PrinterHelper.AskForInput("Podaj nazwę użytkownika:", 2);
        string password = PrinterHelper.AskForInput("Podaj hasło:", 2);

        foreach (User user in listOfUsers)
        {
            if (user.Username.Equals(username)) {
                PrinterHelper.PrintMessage("Użytkownik o podanej nazwie już istnieje", 2, true);
                return;
            }
        }
        User newUser = new User(username, password);
        listOfUsers.Add(newUser);
        loggedUser = newUser;
        PrinterHelper.PrintMessage("Stworzono konto użytkownika {0}", newUser.Username, 1, true);
    }
    public void Login() {
        Console.Clear();
        PrinterHelper.PrintMessage("Logowanie", 0, false);

        if (IsLoggedIn()) {
            PrinterHelper.PrintMessage("Już zalogowany", 2, true);
            return;
        }
        
        string username = PrinterHelper.AskForInput("Podaj nazwę użytkownika:", 2);
        string password = PrinterHelper.AskForInput("Podaj hasło:", 2);
        
        foreach (User user in listOfUsers)
        {
            PrinterHelper.PrintMessage(user.Username, 1, false);
            if (user.Username.Equals(username)) {
                if (user.Login(password)) {
                    loggedUser = user;
                    PrinterHelper.PrintMessage("Zalogowano", 2, true);
                    return;
                }
                else {
                    PrinterHelper.PrintMessage("Złe hasło", 2, true);
                }
            }
        }
        PrinterHelper.PrintMessage("Nie znaleziono użytkownika", 1, true);
    }
    public void Logout() {
        Console.Clear();
        if (IsLoggedIn()) {
            PrinterHelper.PrintMessage("Wylogowano", 2, true);
            loggedUser = null;
        }
    }
    private bool IsLoggedIn() {
        
        if (loggedUser != null) {
            return true;
        }
        return false;
    }
    public User? GetCurrentUser() {
        if (IsLoggedIn()) {
            return loggedUser;
        }
        else return null;
    }
}

