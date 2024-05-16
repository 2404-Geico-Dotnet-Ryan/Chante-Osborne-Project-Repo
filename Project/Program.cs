using System;

class Program
{
    static PlantService ps = new();
    static UserService us = new();
    static User? currentUser = null;
    static void Main(string[] args)
    {

        welcome();
        askUserForLoginCredentials();
        mainMenu();

    }

    public static void welcome()
    {
        System.Console.WriteLine("Welcome to Chante's Plant Shop!");
        int milliseconds = 750;
        Thread.Sleep(milliseconds);
        System.Console.WriteLine("Let's get you logged in, so you can take a look around our shop!");
        Thread.Sleep(milliseconds);
    }

    public static void askUserForLoginCredentials()
    {
        System.Console.WriteLine("Please enter your User ID");
        string userId = (Console.ReadLine() ?? "0");
        System.Console.WriteLine("Please enter your Password");
        string password = (Console.ReadLine() ?? "0");
        validateUserLogin(userId, password);
        System.Console.WriteLine();
    }
    public static User validateUserLogin(string userId, string password)
    {
        // LoginUser checks input against users in storage, if found user is returned, if not, null is returned
        User? user = us.LoginUser(userId, password);
        // if null, below will loop until correct combo returns
        if (user == null)
        {
            askUserForLoginCredentials();
            return null;
        }
        currentUser = user;
        return user;
    }

    public static void mainMenu()
    {
        System.Console.WriteLine($"Welcome back {currentUser?.FirstName}! Good to see you again!");
        bool keepGoing = true;
        while (keepGoing)
        {
            System.Console.WriteLine("Please Select an Option:");
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("[1] View Our Inventory");
            System.Console.WriteLine("[2] Purchase a Plant");
            System.Console.WriteLine("[3] View Previous Purchases");
            System.Console.WriteLine("[0] Logout");
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            int input = int.Parse(Console.ReadLine() ?? "0");
            ValidateMenuSelection(input, 3);
            keepGoing = NextSelection(input);
        }
    }

    public static bool NextSelection(int input)
    {
        switch (input)
        {
            case 1:
                {
                    ViewInventory();
                    break;
                }
            case 2:
                {
                    PurchasePlant();
                    break;
                }
            case 3:
                {
                    ViewPurchaseHistory();
                    break;
                }
            case 0:
            default:
                {
                    //If option 0 or any other, set keepGoing to false.
                    return false;
                }
        }

        return true;
    }
    public static int ValidateMenuSelection(int selection, int maxSelection)
    {
        // Returns prompt for user to select a valid menu option, if selection is out of range.
        while (selection < 0 || selection > maxSelection)
        {
            System.Console.WriteLine($"Invalid Selection - Please Select an Option 1-{maxSelection}; or 0 to Logout");
            selection = int.Parse(Console.ReadLine() ?? "0");
        }
        // Returns customer selection if valid.
        return selection;
    }

    public static void ViewInventory()
    {
        // using Plant Service - GetAvailablePlants
        List<Plant> plants = ps.GetAvailablePlants();
        System.Console.WriteLine("Here is a list of plants available for purchase:");
        System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        foreach (Plant p in plants)
        {
            System.Console.WriteLine($"ID: {p.Id}  Name: {p.PlantName}  Price: ${p.Price}");
            System.Console.WriteLine();
        }
        System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        System.Console.WriteLine();

    }

    public static void PurchasePlant()
    {
        Plant? plant = PromptUserForPlant();
        bool promptUser = true;
        while (promptUser)
        {
            if (plant == null)
            {
                promptUser = false;
                return; // completes path from PromptUserForPlant back to menu
            }
            if (plant != null)
            {
                System.Console.WriteLine($"Please confirm you want to purchase the {plant.PlantName}.");
                System.Console.WriteLine($"Enter 'Y' to purchase or 'N' to enter a different plant.");
                string confirmation = (Console.ReadLine() ?? "N");
                if (confirmation == "Y")
                {
                    plant = ps.BuyPlant(plant);
                    if (plant != null)
                    {
                        plant.Buyer = currentUser; // This is not updating anything :( Might be an update in SQL that we haven't discussed yet.
                        promptUser = false;
                        System.Console.WriteLine($"Congrats! Your purchase for {plant.PlantName} is complete!");
                        System.Console.WriteLine();
                        System.Console.WriteLine();
                    }
                }
                else if (confirmation == "N")
                {
                    plant = null;
                    PurchasePlant();
                }
                else
                {
                    return;
                }
            }
        }
    }

    public static Plant? PromptUserForPlant()
    {
        // creating a plant variable
        Plant? retrievedPlant = null;
        // creating a loop until plant is retrieved or customer selects '0' to exit process
        while (retrievedPlant == null)
        {
            System.Console.WriteLine("Please enter a Plant ID or enter 0 to return to the main menu: ");
            int input = int.Parse(Console.ReadLine() ?? "0");

            if (input == 0) return null;

            retrievedPlant = ps.GetPlant(input);
        }
        return retrievedPlant;
    }

    public static void ViewPurchaseHistory()
    {
        List<Plant> purchases = ps.PurchaseHistory(currentUser);
        System.Console.WriteLine(purchases);
        System.Console.WriteLine();

    }

}