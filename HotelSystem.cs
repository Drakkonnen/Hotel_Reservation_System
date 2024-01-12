class HotelSystem {
    Accounts accounts;
    const int roomsAmount = 36;
    private readonly Room[] Rooms;
    Menu? menu;

    public HotelSystem() {
        accounts = new Accounts();
        Rooms = new Room[roomsAmount];
        PopulateRooms();

        AddMenu(new string[] {"Pokaż dostępne miesjca", "Rezerwacja", "Konto", "Wyjdź"},
        new Action[] {DisplayAllRooms, Submenu, Submenu, Exit},
        "System rezerwacji pokojów w hotelu", -1);

        AddMenu(new string[] {"Zarezerwuj pokój", "Anuluj rezerwację", "Wyświetl twoje rezerwacje", "Wstecz"},
        new Action[] {ReserveRoom, CancelReservation, DisplayYourReservations, GoBack},
        "Rezerwacja", 1);

        AddMenu(new string[] {"Zaloguj się", "Załóż konto", "Wyloguj się", "Wstecz"},
        new Action[] {Login, CreateUser, Logout, GoBack},
        "Konto", 2);
        
        menu?.Start();
    }

    private void AddMenu(string[] options, Action[] actions, string header, int index) {
        Menu newMenu = new Menu(options, actions, header, accounts);
        if (menu == null) {
            menu = newMenu;
        }
        else {
            menu.AddSubmenu(index, newMenu);
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

    private void Submenu() {
        return;
    }
    private void Exit() {
        Environment.Exit(0);
    }
    private void GoBack() {
        menu?.Start();
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
            PrinterHelper.PrintFormat(i);
            if (Rooms[i].IsReserved()) {
                Console.Write('X');
            }
            else {
                Console.Write('O');
            }
        }
        Console.ReadKey();
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