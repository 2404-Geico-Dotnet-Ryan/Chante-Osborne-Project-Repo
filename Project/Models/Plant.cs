using System.Data.Common;

class Plant
{
    public int Id { get; set; }
    public string PlantName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public User? Buyer { get; set; }

    // No Arg Constructor
    public Plant()
    {
        PlantName = "";
    }

    // Full Arg Constructor
    public Plant(int id, string plantName, double price, int quantity, User buyer)
    {
        Id = id;
        PlantName = plantName;
        Price = price;
        Quantity = quantity;
        Buyer = buyer;
    }

    public override string ToString()
    {
        return $"{{Id: {Id},Plant Name: '{PlantName}',Price: {Price},Quantity: {Quantity},Buyer: '{Buyer}'}}";
    }

}