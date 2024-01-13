class Submenu {
    string[] options;
    Action[] actions;
    string header;
    int index;
    string internalName;
    string parentName;

    private Submenu(string[] options, Action[] actions, string header, int index, string internalName, string parentName) {
        this.options = options;
        this.actions = actions;
        this.header = header;
        this.index = index;
        this.internalName = internalName;
        this.parentName = parentName;
    }
    public static Submenu CreateInstance(string[] options, Action[] actions, string header, int index, string internalName, string parentName) {
        if (options.Length == actions.Length) {
            return new Submenu(options, actions, header, index, internalName, parentName);
        }
        return null;
    }
}