class Menu {
    bool isWorking = true;
    string Header {get; set; }
    int CursorPos {get; set;}
    private IUserProvider userProvider;
    Dictionary<int, Menu> submenus = new Dictionary<int, Menu>();
    List<Option> optionsList = new List<Option>();
    struct Option
    {
        public string Name {get; private set;}
        public int Index {get; private set;} 
        public Action Action {get; private set;}
        public Option(string name, int index, Action action) {
            Name = name;
            Index = index;
            Action = action;
        }
    }



    public Menu(string[] options, Action[] actions, string header, IUserProvider userProvider) {
        for (int i = 0; i < options.Length; i++) {
            Option option = new Option(options[i], i, actions[i]);
            optionsList.Add(option);
        }
        this.userProvider = userProvider; 
        Header = header;
        CursorPos = 0;
    }

    public void Start() {
        if (Console.WindowHeight < 10 || Console.WindowWidth < 50) {
            Console.WriteLine("Za małe okno konsoli");
            return;
        }
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

    private void PrintMenu() {
        PrinterHelper.PrintMessage(Header, 0, false);
        Console.CursorTop++;
        for (int i = 0; i < optionsList.Count; i++)
        {
            string text = optionsList.ElementAt(i).Name;
            if (CursorPos == optionsList.ElementAt(i).Index) {
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
        foreach (var option in optionsList)
        {
            if (option.Index == CursorPos) {
                if (option.Action.Method.Name.ToString() == "Submenu") {
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
                    CursorPos = optionsList.Count - 1;
                }
                break;
            case ConsoleKey.DownArrow:
                CursorPos++;
                if (CursorPos > optionsList.Count - 1) {
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