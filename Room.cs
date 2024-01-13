class Room {
    private bool Reserved {get; set;}
    private User? ReservedBy;
    public void Reserve(User user) {
        Reserved = true;
        ReservedBy = user;
    }
    public void FreeRoom() {
        Reserved = false;
        ReservedBy = null;
    }
    public bool IsReserved() {
        return Reserved;
    }
}