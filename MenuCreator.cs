class MenuCreator {
    public string[] Options {get; private set;}
    public Action[] Actions {get; private set;}
    public string Header {get; private set;}
    public int Index {get; private set;}
    public string InternalName {get; private set;}
    public string? ParentName {get; private set;}

    private MenuCreator(string[] options, Action[] actions, string header, int index, string internalName, string? parentName = null) {
        Options = options;
        Actions = actions;
        Header = header;
        Index = index;
        InternalName = internalName;
        ParentName = parentName;
    }
    public static MenuCreator? CreateInstance(string[] options, Action[] actions, string header, int index, string internalName, string? parentName = null) {
        if (options.Length == actions.Length) {
            return new MenuCreator(options, actions, header, index, internalName, parentName);
        }
        return null;
    }
}