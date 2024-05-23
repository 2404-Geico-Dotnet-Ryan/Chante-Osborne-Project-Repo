using System.Data.Common;

class PlantService
{
    NPlantRepo pr;

    public PlantService(NPlantRepo pr)
    {
        this.pr = pr;
    }

    public List<Plant> GetAvailablePlants() // working!! 
    {
        // Create list of all plants by calling GetAllPlants method
        List<Plant> allPlants = pr.GetAllPlants();
        // Create empty List for available plants
        List<Plant> availablePlants = new();
        // Loop through allPlants list to find available plants and add to new availablePlants list.
        foreach (Plant p in allPlants)
        {
            if (p.Available)
            {
                availablePlants.Add(p);
            }
            if (allPlants.Count == 0)
            {
                System.Console.WriteLine("ALL ITEMS SOLD OUT");
            }
        }

        //Return that list of available plants.
        return availablePlants;

    }

    // trivial service calling GetPlant method - already set up to get one plant, no need to edit.
    public Plant? GetPlant(int id)
    {
        return pr.GetPlant(id);
    }

    public Plant BuyPlant(Plant p, User u) //working!! 
    {
        if (!p.Available)
        {
            Console.WriteLine("Selected Plant is Not Available. Please try again.");
            return null;
        }
        p.Available = false;
        p.UserId = u.UserId;
        return p;
    }

    public List<Plant> PurchaseHistory(User u) // working!! 
    {
        // Create list of all plants by calling GetAllPlants method 
        List<Plant> allPlants = pr.GetAllPlants();
        // Create empty List for buyer's plants
        List<Plant> buyersPlants = new();
        foreach (Plant p in allPlants) // check through all plants for any with the current users userId and add to buyer's list.
        {
            if (p.UserId == u.UserId)
            {
                buyersPlants.Add(p);
            }
        }
        if (buyersPlants.Count == 0) // If there are no results, let them know.
        {
            System.Console.WriteLine("You haven't purchased anything yet.");
        }
        return buyersPlants;

    }

    public Plant? UpdatePlant(Plant p) // working!! 
    {
        return pr.UpdatePlant(p);
    }

}