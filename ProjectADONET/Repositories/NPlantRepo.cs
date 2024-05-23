
using Microsoft.Data.SqlClient;

class NPlantRepo
{
    private readonly string _connectionString;

    public NPlantRepo(string _connString)
    {
        _connectionString = _connString;
    }

    public Plant AddPlant(Plant p) // working!!
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        string sql = "INSERT INTO dbo.Plant OUTPUT inserted.* VALUES (@PlantName, @Price, @Available, @UserId)";
        using SqlCommand cmd = new(sql, connection);
        cmd.Parameters.AddWithValue("@PlantName", p.PlantName);
        cmd.Parameters.AddWithValue("@Price", p.Price);
        cmd.Parameters.AddWithValue("Available", p.Available);
        cmd.Parameters.AddWithValue("@UserId", p.UserId);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Plant newPlant = BuildPlant(reader);
            return newPlant;
        }
        else
        {
            return null;
        }
    }

    public Plant? DeletePlant(int id) // Working!! 
    {
        try
        {
            // Setting up connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            // Creating SQL query
            string sql = "DELETE FROM dbo.Plant OUTPUT deleted.* WHERE Id = @Id";
            // Set SQL cmd object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            // Run query
            using var reader = cmd.ExecuteReader();
            // Extract results
            if (reader.Read())
            {
                Plant newPlant = BuildPlant(reader);
                return newPlant;
            }
            System.Console.WriteLine("Invalid Plant Id. Please try again.");
            return null;


        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public List<Plant> GetAllPlants() // Needs testing
    {
        List<Plant> plants = [];
        try
        {
            // Set up SQL connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            // Create the SQL query
            string sql = "SELECT * FROM dbo.Plant";
            // Set SQL cmd object
            using SqlCommand cmd = new(sql, connection);
            //Execute query
            using var reader = cmd.ExecuteReader();
            // Extract results
            while (reader.Read())
            {
                Plant newPlant = BuildPlant(reader);
                plants.Add(newPlant);
            }
            return plants;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public Plant? GetPlant(int id) // Needs testing
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.Plant WHERE Id = @Id";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Plant newPlant = BuildPlant(reader);
                return newPlant;
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

    public Plant? UpdatePlant(Plant updatedPlant) // Needs testing
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string sql = "UPDATE dbo.Plant SET PlantName = @PlantName, Price = @Price, Available = @Available, UserId = @UserId OUTPUT inserted.* WHERE Id = @Id";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Id", updatedPlant.Id);
            cmd.Parameters.AddWithValue("@PlantName", updatedPlant.PlantName);
            cmd.Parameters.AddWithValue("@Price", updatedPlant.Price);
            cmd.Parameters.AddWithValue("@Available", updatedPlant.Available);
            cmd.Parameters.AddWithValue("@UserId", updatedPlant.UserId);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Plant newPlant = BuildPlant(reader);
                return newPlant;
            }

            return null; // If no plant found

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    private static Plant BuildPlant(SqlDataReader reader)
    {
        Plant newPlant = new();
        newPlant.Id = (int)reader["Id"];
        newPlant.PlantName = (string)reader["PlantName"];
        newPlant.Price = (double)(decimal)reader["Price"];
        newPlant.Available = (bool)reader["Available"];
        newPlant.UserId = (int)reader["UserId"];

        return newPlant;
    }
}