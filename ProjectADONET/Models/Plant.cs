using System.Data.Common;

class Plant
{
    public int Id { get; set; }
    public string PlantName { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; }
    public int UserId { get; set; }

    // No Arg Constructor
    public Plant()
    {
        PlantName = "";
    }

    // Full Arg Constructor
    public Plant(int id, string plantName, double price, bool available, int userId)
    {
        Id = id;
        PlantName = plantName;
        Price = price;
        Available = available;
        UserId = userId;
        
    }

    public Plant(int id, string plantName, double price, bool available)
    {
        Id = id;
        PlantName = plantName;
        Price = price;
        Available = available;
    }

    public override string ToString()
    {
        return $"{{Id: {Id},Plant Name: '{PlantName}',Price: {Price}, Available: {Available},Buyer: '{UserId}'}}";
    }

}