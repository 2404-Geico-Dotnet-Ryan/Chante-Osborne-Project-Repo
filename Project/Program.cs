using System;

class Program
{
    static void Main(string[] args)
    {
        Grocery grocery = new();
        System.Console.WriteLine(grocery);
        
        WishList wishList = new();
        System.Console.WriteLine(wishList);

        User user = new();
        System.Console.WriteLine(user);

    }
}