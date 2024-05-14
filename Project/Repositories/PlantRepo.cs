using System.Collections.Immutable;

class PlantRepo
{
    /*
    - CRUD Operations
        - C - Create
        - R - Read
        - U - Update
        - D - Delete
    */


    PlantStorage plantStorage = new();

    // Create/Add
    public Plant AddPlant(Plant p)
    {
        p.Id = plantStorage.idCounter++; // creates new id using id counter.

        plantStorage.plants.Add(p.Id, p); // adds plant to plant dictionary
        return p;
    }

    // Retrieve
    public Plant? GetPlant(int id)
    {
        if (plantStorage.plants.ContainsKey(id))
        {
            Plant selectedPlant = plantStorage.plants[id];
            return selectedPlant;
        }
        else
        {
            System.Console.WriteLine("Invalid Plant ID. Please Try Again.");
            return null;
        }
    }

    public List<Plant> GetAllPlants()
    {
        return plantStorage.plants.Values.ToList(); // returns list of all plants in plantStorage
    }

    // Update
    public Plant? UpdatePlant(Plant updatedPlant)
    {
        try
        {
            plantStorage.plants[updatedPlant.Id] = updatedPlant; // checks for plant id
            return updatedPlant;
        }
        catch (Exception)
        {
            System.Console.WriteLine("Invalid Plant ID. Please Try Again.");
            return null;
        }
    }

    // Delete
    public Plant? DeletePlant(Plant p)
    {
        bool didRemove = plantStorage.plants.Remove(p.Id);

        if (didRemove)
        {
            return p;
        }
        else
        {
            System.Console.WriteLine("Invalid Plant ID. Please Try Again.");
            return null;
        }

    }



}