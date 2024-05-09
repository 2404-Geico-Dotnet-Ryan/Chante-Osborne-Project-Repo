class Grocery
{
    public string? Item { get; set; }
    public int Quantity { get; set; }
    public int UserId { get; set; }
    public bool Purchased { get; set; }

    // No Arg Constructor
    public Grocery()
    {
        Item = "";
    }

    // Full Arg Constructor
    public Grocery(string item, int quantity, int userId, bool purchsed)
    {
        Item = item;
        Quantity = quantity;
        UserId = userId;
        Purchased = purchsed;
    }

    // To String
    public override string ToString()
    {
        return $"{{Item: {Item},Quantity: {Quantity}, UserId: {UserId}, Purchased: {Purchased}}}";
    }


}