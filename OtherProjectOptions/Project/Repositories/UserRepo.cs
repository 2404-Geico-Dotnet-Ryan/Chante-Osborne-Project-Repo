class UserRepo 
{
    UserStorage userStorage = new();

    // Create
    public User AddUser(User u)
    {
        u.UserId = userStorage.idCounter++; // increments value for next profile added

        userStorage.users.Add(u.UserId, u);
        return u;
    }

    // Retrieve
    public User? GetUser(int userId)
    {
        if (userStorage.users.ContainsKey(userId))
        {
            User selectedUser = userStorage.users[userId];
            return selectedUser;
        }
        else
        {
            System.Console.WriteLine("Invalid User Id - Please Try Again.");
            return null;
        }
    }

    public List<User> GetAllUsers()
    {
        return userStorage.users.Values.ToList();
    }

    // Update
    public User? UpdateUser(User updatedUser)
    {
        try
        {
            userStorage.users[updatedUser.UserId] = updatedUser;
            return updatedUser;
        }
        catch (Exception)
        {
            System.Console.WriteLine("Invalid User ID - Please Try Again");
            return null;
        }
    }

    // Delete
    public User? DeleteUser(User u)
    {
        bool didRemove = userStorage.users.Remove(u.UserId);

        if (didRemove)
        {
            return u;
        }
        else
        {
            System.Console.WriteLine("Invalid User ID - Please Try Again");
            return null;
        }

    }

}