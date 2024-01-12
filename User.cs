public class User {
    public string Username {get; private set;}
    private string Password {get; set;}
    private List<int> ReservedRooms = new List<int>();
    public User(string username, string password) {
        Username = username;
        Password = password;
    }

    public bool Login(string password) {
        if (Password.Equals(password)) {
            return true;
        }
        return false;
    }
    public void AddReservedRoom(int roomNumber) {
        ReservedRooms.Add(roomNumber);
    }
    public void RemoveReservedRoom(int roomNumber) {
        ReservedRooms.Remove(roomNumber);
    }
    public bool IsRoomReserved(int roomNumber) {
        return ReservedRooms.Contains(roomNumber);
    }
    public List<int> GetReservedRooms() {
        return ReservedRooms;
    }
}