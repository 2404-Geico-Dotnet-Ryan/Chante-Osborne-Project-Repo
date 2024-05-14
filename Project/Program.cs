using System;

class Program
{
    static void Main(string[] args)
    {
        // Grocery grocery = new();
        // System.Console.WriteLine(grocery);
        
        // WishList wishList = new();
        // System.Console.WriteLine(wishList);

        // User user = new();
        // System.Console.WriteLine(user);
        GroceryRepo groceryRepo = new();

        AddNewGroceryItem(groceryRepo);

    }


    public static void AddNewGroceryItem(GroceryRepo groceryRepo)
    {
        System.Console.WriteLine("Let's add a new grocery item!");
        System.Console.WriteLine("What do you want to add to the grocery list?");
        string NewGroceryItem = Console.ReadLine() ?? "0";
        System.Console.WriteLine("Great! How many do you need?");
        int NewItemQuantity = int.Parse(Console.ReadLine() ?? "0");
        
        Grocery grocery = new(NewGroceryItem, NewItemQuantity,0,false);
        grocery = groceryRepo.AddGroceryItem(grocery);

        System.Console.WriteLine($"Newly added item: {grocery}");

    }
}