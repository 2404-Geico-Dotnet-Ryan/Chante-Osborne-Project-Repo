class UserService
{
    UserRepo ur = new();

    /*
        Services Needed for User:

        - Register New User
        - Login
    */

    // Register
    public User? RegisterUser(User u)
    {
        if (u.Role != "user") // do not allow anyone to add a role other than "user"
        {
            System.Console.WriteLine("Invalid Role - Please try again!");
            return null;
        }
        // Let's not let them register if username is already taken! 
        // Get all users
        // Check if our new username matches any of the usernames on all of those users
        List<User> allUsers = ur.GetAllUser();
        // creates list of all users to check
        foreach (User user in allUsers)
        {
            if (user.UserName == u.UserName)
            {
                System.Console.WriteLine("UserName already taken. Please try again.");
                return null; // reject them
            }
        }

        // If we make it this far, then we are saying that the role is good to go and username is good to go
        return ur.AddUser(u);

    }


    // Login
    public User? LoginUser(string userName, string password)
    {
        List<User> allUsers = ur.GetAllUser();
        foreach (User user in allUsers)
        {
            // if we get a match, they login by returning that user
            if (user.UserName == userName && user.Password == password)
            {
                // Yay! login! 
                return user; // us returning the user will indicate success
            }
        }
        // if we make it this far, we did not find a match. this is where we reject them
        System.Console.WriteLine("Invalid UserName / Password combo. Please try again.");
        return null;
    }


}