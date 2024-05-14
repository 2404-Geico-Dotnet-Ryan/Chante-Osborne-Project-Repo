class UserStorage
{
    // Creating dictionary to store user profiles
    public Dictionary<int, User> users;
    
    // Setting counter to auto-assign UserIds
    public int idCounter = 1;

    public UserStorage()
    {
        User user1 = new(idCounter, "Admin", "pass1", "Admin", "Admin", "Admin"); idCounter++;
        User user2 = new(idCounter, "Chante1981", "pass2", "Chante", "Osborne", "user"); idCounter++;
        User user3 = new(idCounter, "Christian123", "pass3", "Christian", "Osborne", "user"); idCounter++;
        User user4 = new(idCounter, "AJTekoa", "pass4", "Asher", "Long", "user"); idCounter++;


        users = [];

        users.Add(user1.UserId, user1);
        users.Add(user2.UserId, user2);
        users.Add(user3.UserId, user3);
        users.Add(user4.UserId, user4);



    }


}