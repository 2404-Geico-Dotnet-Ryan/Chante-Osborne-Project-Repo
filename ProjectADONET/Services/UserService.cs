class UserService
{
    NUserRepo ur;

    public UserService(NUserRepo ur)
    {
        this.ur = ur;
    }

    public User? RegisterUser(User u)
    {
        if (u.Role != "user") // do not allow anyone to add a role other than "user"
        {
            System.Console.WriteLine("Invalid Role - Please try again!");
            return null;
        }

        List<User> allUsers = ur.GetAllUsers();

        foreach (User user in allUsers)
        {
            if (user.UserName == u.UserName)
            {
                System.Console.WriteLine("UserName already taken. Please try again.");
                return null; 
            }
        }
        if (u.Password == null)
        {
            System.Console.WriteLine("Invalid password. Please try again.");
            return null;
        }

        return ur.AddUser(u);

    }

    public User? LoginUser(string userName, string password)
    {
        List<User> allUsers = ur.GetAllUsers();
        foreach (User user in allUsers)
        {
            // if we get a match, they login by returning that user
            if (user.UserName == userName && user.Password == password)
            {
                // Yay! login! 
                return user; // us returning the user will indicate success
            }
        }

        System.Console.WriteLine("Invalid UserName / Password combo. Please try again.");
        return null;
    }

}