using System.Diagnostics;

class HotelSystem {
    Accounts accounts;
    const int roomsAmount = 48;
    private readonly Room[] Rooms;
    Menu? mainMenu;

    public HotelSystem() {
        accounts = new Accounts();
        Rooms = new Room[roomsAmount];
        PopulateRooms();
        CreateMenu();
        
        
        mainMenu?.Start();
    }

    private void CreateMenu() {
        List<MenuCreator?> tmpMenus = new List<MenuCreator?>();
        MenuCreator? submenu;
        string[] options;
        Action[] actions;
        int index;
        string header, internalName, parentName;

        options = new string[] {"Pokaż dostępne miesjca", "Rezerwacja", "Konto", "Wyjdź"};
        actions = new Action[] {DisplayAllRooms, EnterSubmenu, EnterSubmenu, Exit};
        header = "System rezerwacji pokojów w hotelu";
        index = -1;
        internalName = "MainMenu";
        submenu = MenuCreator.CreateInstance(options, actions, header, index, internalName);
        tmpMenus.Add(submenu);

        options = new string[] {"Zarezerwuj pokój", "Anuluj rezerwację", "Wyświetl twoje rezerwacje", "Wstecz"};
        actions = new Action[] {ReserveRoom, CancelReservation, DisplayYourReservations, GoBack};
        header = "Rezerwacja";
        index = 1;
        internalName = "RezerwacjaMenu";
        parentName = "MainMenu";
        submenu = MenuCreator.CreateInstance(options, actions, header, index, internalName, parentName);
        tmpMenus.Add(submenu);
        

        options = new string[] {"Zaloguj się", "Załóż konto", "Płatności", "Wyloguj się", "Wstecz"};
        actions = new Action[] {Login, CreateUser, EnterSubmenu, Logout, GoBack};
        header = "Konto";
        index = 2;
        internalName = "KontoMenu";
        parentName = "MainMenu";
        submenu = MenuCreator.CreateInstance(options, actions, header, index, internalName, parentName);
        tmpMenus.Add(submenu);
        
        options = new string[] {"Wszystkie płatności", "Nieopłacone płatności", "Opłacone płatności", "Wstecz"};
        actions = new Action[] {ShowAllOrders, ShowUnpaidOrders, ShowPaidOrders, GoBack};
        header = "Płatności";
        index = 2;
        internalName = "PłatnościMenu";
        parentName = "KontoMenu";
        submenu = MenuCreator.CreateInstance(options, actions, header, index, internalName, parentName);
        tmpMenus.Add(submenu);
        
        foreach (MenuCreator? menu in tmpMenus)
        {
            if (menu == null) {
                PrinterHelper.PrintMessage("Ilość opcji nie zgadza się z liczbą akcji", 2, true);
                return;
            }
            AddMenu(menu);
        }
    }
    private void AddMenu(MenuCreator submenu) {
        Menu newMenu = new Menu(submenu.Options, submenu.Actions, submenu.Header, accounts, submenu.InternalName);
        if (mainMenu == null) {
            mainMenu = newMenu;
        }

        else {
            if (mainMenu.MenuName == submenu.ParentName) {
                mainMenu.AddSubmenu(submenu.Index, newMenu);
            }
            else {
                foreach (var menu in mainMenu.GetSubmenus())
                {
                    if (menu.Value.MenuName == submenu.ParentName) {
                        menu.Value.AddSubmenu(submenu.Index, newMenu);
                        return;
                    }
                }
            }
        }
    }
    private void CreateUser() {
        accounts.CreateUser();
        GoBack();
    }
    private void Login() {
        accounts.Login();
        GoBack();
    }
    

    private void Logout() {
        accounts.Logout();
        GoBack();
    }

    private void EnterSubmenu() {
        return;
    }
    private void Exit() {
        Environment.Exit(0);
    }
    private void GoBack() {
        Console.Clear();
    }
    private void ShowPaidOrders() {
        return;
    }
    private void ShowUnpaidOrders() {
        return;
    }
    private void ShowAllOrders() {
        return;
    }
    private void ReserveRoom()
    {
        Console.Clear();
        PrinterHelper.PrintMessage("Reservation", 0, false);
        User? currentUser = accounts.GetCurrentUser();
        if (currentUser != null) {
            if (int.TryParse(PrinterHelper.AskForInput("Podaj numer pokoju", 2), out int roomNumber) 
            && roomNumber <= roomsAmount && roomNumber > 0)
            {
                if (Rooms[roomNumber - 1].IsReserved()) {
                    PrinterHelper.PrintMessage("Pokój o numerze {0} jest już zarezerwowany", roomNumber.ToString(), 2, true);
                    GoBack();
                }
                Rooms[roomNumber - 1].Reserve(currentUser);
                currentUser.AddReservedRoom(roomNumber);
                PrinterHelper.PrintMessage("Zarezerwowano pokój {0}", roomNumber.ToString(), 2, true);
            }
            else {
                PrinterHelper.PrintMessage("Wprowadź poprawny numer pokoju", 2, true);
            }
        }
        else {
            PrinterHelper.PrintMessage("Proszę się zalogować", 0, true);
        }
        GoBack();
    }

    private void CancelReservation()
    {
        Console.Clear();
        PrinterHelper.PrintMessage("Anulowanie rezerwacji", 0, false);
        
        User? currentUser = accounts.GetCurrentUser();
        if (currentUser != null) {
            if (int.TryParse(PrinterHelper.AskForInput("Podaj numer pokoju", 2), out int roomNumber) 
            && roomNumber <= roomsAmount && roomNumber > 0)
            {
                if (!Rooms[roomNumber - 1].IsReserved()) {
                    PrinterHelper.PrintMessage("Pokój o numerze {0} nie jest zarezerwowany", roomNumber.ToString(), 2, true);
                    GoBack();
                }
                if (!currentUser.IsRoomReserved(roomNumber)) {
                    PrinterHelper.PrintMessage("Pokój o numerze {0} nie jest zarezerwowany przez ciebie", roomNumber.ToString(), 2, true);
                    GoBack();
                }
                Rooms[roomNumber - 1].FreeRoom();
                currentUser.RemoveReservedRoom(roomNumber);
                PrinterHelper.PrintMessage("Anulowano rezerwację pokoju {0}", roomNumber.ToString(), 2, true);
            }
            else {
                PrinterHelper.PrintMessage("Wprowadź poprawny numer pokoju", 2, true);
            }
        }
        else {
            PrinterHelper.PrintMessage("Proszę się zalogować", 2, true);
        }
        GoBack();
    }

    private void DisplayAllRooms()
    {
        Console.Clear();
        PrinterHelper.PrintMessage("Stan pokojów\n", 0, false);
        for (int i = 0; i < Rooms.Length; i++)
        {   
            PrinterHelper.PrintRoomsFormat(i);
            if (Rooms[i].IsReserved()) {
                Console.Write('X');
            }
            else {
                Console.Write('O');
            }
        }
        Console.ReadKey(true);
        GoBack();
    }
    private void DisplayYourReservations() {
        Console.Clear();
        PrinterHelper.PrintMessage("Pokoje zarezerwowane przez ciebie\n", 0, false);
        User? currentUser = accounts.GetCurrentUser();
        List<int> reservedRooms;
        if (currentUser != null) {
            reservedRooms = currentUser.GetReservedRooms();
            if (reservedRooms.Count > 0) {
                PrinterHelper.PrintMessage("Pokoje: ", 2, false);
                foreach (int room in reservedRooms)
                {
                    Console.Write(room + " ");
                }
                Console.ReadKey();
            }
            else {
                PrinterHelper.PrintMessage("Brak zarezerwowanych pokoi", 2, true);
            }
        }
        else {
            PrinterHelper.PrintMessage("Proszę się zalogować", 2, true);
        }
        GoBack();
    }
    private void PopulateRooms() {
        for (int i = 0; i < Rooms.Length; i++)
        {
            Rooms[i] = new Room();
        }
    }
}