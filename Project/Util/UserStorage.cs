class UserStorage
{
    // Creating dictionary to store user profiles
    public Dictionary<int, User> profiles;
    
    // Setting counter to auto-assign UserIds
    public int idCounter = 100;

    public UserStorage()
    {
        profiles = [];
    }


}