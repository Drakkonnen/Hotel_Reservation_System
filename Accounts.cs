class Accounts : IUserProvider { 
    User? loggedUser;
    List<User> listOfUsers = new List<User>();
    private IUserInputProvider userInputProvider;


    public Accounts(IUserInputProvider userInputProvider) {
        this.userInputProvider = userInputProvider;
    }
    public bool CreateUser() {
        userInputProvider.ClearScreen();
        userInputProvider.PrintMessage("Zakładanie konta", 0, false);
        if (IsLoggedIn()) {
            userInputProvider.PrintMessage("Już zalogowany", 2, true);
            return false;
        }

        string username = userInputProvider.ReadLine("Podaj nazwę użytkownika:", 2);
        string password = userInputProvider.ReadLine("Podaj hasło:", 2);

        foreach (User user in listOfUsers)
        {
            if (user.Username.Equals(username)) {
                userInputProvider.PrintMessage("Użytkownik o podanej nazwie już istnieje", 2, true);
                return false;
            }
        }
        User newUser = new User(username, password);
        listOfUsers.Add(newUser);
        loggedUser = newUser;
        userInputProvider.PrintMessage("Stworzono konto użytkownika {0}", 1, true, newUser.Username);
        return true;
    }
    public bool Login() {
        userInputProvider.ClearScreen();
        userInputProvider.PrintMessage("Logowanie", 0, false);

        if (IsLoggedIn()) {
            userInputProvider.PrintMessage("Już zalogowany", 2, true);
            return false;
        }
        
        string username = userInputProvider.ReadLine("Podaj nazwę użytkownika:", 2);
        string password = userInputProvider.ReadLine("Podaj hasło:", 2);
        
        foreach (User user in listOfUsers)
        {
            if (user.Username.Equals(username)) {
                if (user.Login(password)) {
                    loggedUser = user;
                    userInputProvider.PrintMessage("Zalogowano", 2, true);
                    return true;
                }
                else {
                    userInputProvider.PrintMessage("Złe hasło", 2, true);
                    return false;
                }
            }
        }
        userInputProvider.PrintMessage("Nie znaleziono użytkownika", 1, true);
        return false;
    }
    public void Logout() {
        userInputProvider.ClearScreen();
        if (IsLoggedIn()) {
            userInputProvider.PrintMessage("Wylogowano", 2, true);
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
    public int GetAmountOfUsers() {
        return listOfUsers.Count;
    }
}

