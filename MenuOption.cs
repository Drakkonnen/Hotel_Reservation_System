class MenuOption {
    public string OptionName {get; private set;}
    public int Index {get; private set;} 
    public Action Action {get; private set;}
    public MenuOption(string name, int index, Action action) {
        OptionName = name;
        Index = index;
        Action = action;
    }
}