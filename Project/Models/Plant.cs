using System.Data.Common;

class Plant
{
    public int Id { get; set; }
    public string PlantName { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; }
    public User? Buyer { get; set; }

    // No Arg Constructor
    public Plant()
    {
        PlantName = "";
    }

    // Full Arg Constructor
    public Plant(int id, string plantName, double price, bool available, User? buyer)
    {
        Id = id;
        PlantName = plantName;
        Price = price;
        Available = available;
        Buyer = buyer;
        
    }

    public override string ToString()
    {
        return $"{{Id: {Id},Plant Name: '{PlantName}',Price: {Price}, Available: {Available},Buyer: '{Buyer}'}}";
    }

}