
using Microsoft.Data.SqlClient;

class NUserRepo 
{
    private readonly string _connectionString;

    // Primary constructor
    public NUserRepo(string connString)
    {
        _connectionString = connString;
    }

    public User? AddUser(User u) // working! 
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        string sql = "INSERT INTO dbo.[User] OUTPUT inserted.* VALUES (@Username, @Password, @FirstName, @LastName, @Role)";
        using SqlCommand cmd = new(sql, connection);
        cmd.Parameters.AddWithValue("@Username", u.UserName);
        cmd.Parameters.AddWithValue("@Password", u.Password);
        cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
        cmd.Parameters.AddWithValue("@LastName", u.LastName);
        cmd.Parameters.AddWithValue("@Role", u.Role);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            User newUser = BuildUser(reader);
            return newUser;
        }
        else
        {
            return null;
        }
    }
    public User? DeleteUser(int id) // working!
    {
        try
        {
            // Setting up connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            // Creating SQL query
            string sql = "DELETE FROM dbo.[User] OUTPUT deleted.* WHERE UserId = @UserId";
            // Set SQL cmd object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@UserId", id);
            // Run query
            using var reader = cmd.ExecuteReader();
            // Extract results
            if (reader.Read())
            {
                User newUser = BuildUser(reader);
                return newUser;
            }
            System.Console.WriteLine("User does not exist. Please try again.");
            return null;


        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public List<User>? GetAllUsers()
    {
        List<User> users = [];
        try
        {
            // Set up SQL connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            // Create the SQL query
            string sql = "SELECT * FROM dbo.[User]";
            // Set SQL cmd object
            using SqlCommand cmd = new(sql, connection);
            //Execute query
            using var reader = cmd.ExecuteReader();
            // Extract results
            while (reader.Read())
            {
                User newUser = BuildUser(reader);
                users.Add(newUser);
            }
            return users;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
        
    }

    public User? GetUser(int id)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM dbo.[User] WHERE UserId = @Id";

            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@UserId", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                User newUser = BuildUser(reader);
                return newUser;
            }

            return null; 

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public User? UpdateUser(User u)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string sql = "UPDATE dbo.[User] SET UserName = @UserName, Password = @Password, FirstName = @FirstName, LastName = @LastName, Role = @Role OUTPUT inserted.* WHERE Id = @Id";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@UserId", u.UserId);
            cmd.Parameters.AddWithValue("@UserName", u.UserName);
            cmd.Parameters.AddWithValue("@Password", u.Password);
            cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
            cmd.Parameters.AddWithValue("@LastName", u.LastName);
            cmd.Parameters.AddWithValue("@Role", u.Role);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                User newUser = BuildUser(reader);
                return newUser;
            }

            return null; // If no user found

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }
    // Helper Method
    private static User BuildUser(SqlDataReader reader)
    {
        User newUser = new();
        newUser.UserId = (int)reader["UserId"];
        newUser.UserName = (string)reader["UserName"];
        newUser.Password = (string)reader["Password"];
        newUser.FirstName = (string)reader["FirstName"];
        newUser.LastName = (string)reader["LastName"];
        newUser.Role = (string)reader["Role"];
        return newUser;
    }
}