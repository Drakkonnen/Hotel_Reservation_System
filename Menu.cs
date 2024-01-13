class Menu {
    bool isWorking = true;
    public string MenuName {get; private set; }
    string Header {get; set; }
    int CursorPos {get; set; }
    private IUserProvider userProvider;
    Dictionary<int, Menu> submenus = new Dictionary<int, Menu>();
    MenuOption[] arrayOfOptions;


    public Menu(string[] options, Action[] actions, string header, IUserProvider userProvider, string menuName) {
        arrayOfOptions = new MenuOption[options.Length];
        for (int i = 0; i < options.Length; i++) {
            MenuOption option = new MenuOption(options[i], i, actions[i]);
            arrayOfOptions[i] = option;
        }
        this.userProvider = userProvider;
        Header = header;
        CursorPos = 0;
        MenuName = menuName;
    }

    public void Start() {
        PrinterHelper.CheckForBoundaries();
        isWorking = true;
        Console.Clear();
        Console.CursorVisible = false;
        while(isWorking) {
            PrintMenu();
            PrintUser(userProvider.GetCurrentUser());
            ReadInput();
        }
    }
    private void  StopMenu() {
        Console.Clear();
        isWorking = false;
    }
    public void AddSubmenu(int optionIndex, Menu submenu) {
        submenus.Add(optionIndex, submenu);
    }

    public Dictionary<int, Menu> GetSubmenus() {
        return submenus;
    }
    private void PrintMenu() {
        PrinterHelper.PrintMessage(Header, 0, false);
        Console.CursorTop++;
        for (int i = 0; i < arrayOfOptions.Length; i++)
        {
            string text = arrayOfOptions[i].OptionName;
            if (CursorPos == arrayOfOptions[i].Index) {
                PrinterHelper.SelectColor();
            }
            PrinterHelper.PrintMessage(text, 1, false);
            PrinterHelper.UnselectColor();
        }
    }

    private void PrintUser(User? loggedUser) {
        string message;
        if (loggedUser == null) {
            message = "Nie zalogowano";
        }
        else {
            message = "Zalogowano: " + loggedUser.Username;
        }
        Console.SetCursorPosition(Console.WindowWidth - message.Length, 0);
        Console.Write(message);
    }
    private void SelectOption() {
        foreach (var option in arrayOfOptions)
        {
            if (option.Index == CursorPos) {
                if (option.Action.Method.Name.ToString() == "EnterSubmenu") {
                    submenus[option.Index].Start();
                }
                else if (option.Action.Method.Name.ToString() == "GoBack") {
                    StopMenu();
                }
                else {
                    option.Action();
                    return;
                }
            }
        }
    }

    private void ReadInput() {
        ConsoleKeyInfo input = Console.ReadKey(true);
        switch (input.Key)
        {
            case ConsoleKey.UpArrow:
                CursorPos--;
                if (CursorPos < 0) {
                    CursorPos = arrayOfOptions.Length - 1;
                }
                break;
            case ConsoleKey.DownArrow:
                CursorPos++;
                if (CursorPos > arrayOfOptions.Length - 1) {
                    CursorPos = 0;
                }
                break;
            case ConsoleKey.Enter:
                SelectOption();
                break;
            default:
                return;
        }
    }

}