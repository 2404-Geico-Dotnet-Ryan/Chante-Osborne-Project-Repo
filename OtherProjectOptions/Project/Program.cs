using System;
using System.Runtime.CompilerServices;

class Program
{
    static PlantService ps;
    static UserService us;
    static User? currentUser = null;
    static void Main(string[] args)
    {
        string path = @"C:\Users\U88ABJ\Revature\ChantePlantShopSQLdb.txt";
        string connectionString = File.ReadAllText(path);

        NPlantRepo pr = new(connectionString);
        ps = new(pr);
        NUserRepo ur = new(connectionString);
        us = new(ur);

        
       
       Console.WriteLine(pr.DeletePlant(18));


        // welcome();
        // askUserForLoginCredentials();
        // mainMenu();

    }

    public static void welcome()
    {
        System.Console.WriteLine("Welcome to Chante's Plant Shop!");
        int milliseconds = 750;
        Thread.Sleep(milliseconds);
        System.Console.WriteLine("Let's get you logged in, so you can take a look around our shop!");
        Thread.Sleep(milliseconds);
        // System.Console.WriteLine("If you need to register, please enter 'R' to register for a new account. Otherwise, press any key to continue.");
        // string register = (Console.ReadLine()?? "0");
        // if (register == "R")
        // {
        //     RegisterNewUser();
        // }
        
        // COMMENTED OUT REGISTER-- Need to handle nulls in registration process, customer is still able to access main menu
        // after hitting enter over and over
    }

    public static void RegisterNewUser()
    {
        User u = CreateNewUser();
        
    }

    private static User CreateNewUser()
    {
        User u = new();
        u.Role = "user";
        System.Console.WriteLine("Let's get your account set up!");
        System.Console.WriteLine("Please enter your First Name: ");
        u.FirstName = Console.ReadLine();
        System.Console.WriteLine("Great! What's your Last Name?");
        u.LastName = Console.ReadLine();
        System.Console.WriteLine("Please enter a User ID: ");
        u.UserName = Console.ReadLine();
        u.Password = CreatePassword();
        us.RegisterUser(u);
        return u;
    }

    public static string? CreatePassword()
    {
        System.Console.WriteLine("Enter a password: ");
        string password = Console.ReadLine();

        if (password != null)
        {
            System.Console.WriteLine("Please Re-enter password: ");
            string confirmPassword = (Console.ReadLine()?? "0");
            if (confirmPassword == password)
            {
                return password;
            }
            else 
            {
                return null;
            }
        }
        else
        {
            return null;
        }

    }
    public static void askUserForLoginCredentials()
    {
        System.Console.WriteLine("Please enter your User ID");
        string userId = Console.ReadLine() ?? "0";
        System.Console.WriteLine("Please enter your Password");
        string password = Console.ReadLine() ?? "0";
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
            int milliseconds = 1000;
            Thread.Sleep(milliseconds);
            System.Console.WriteLine("Please Select an Option:");
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("[1] View Our Inventory");
            System.Console.WriteLine("[2] Purchase a Plant");
            System.Console.WriteLine("[3] View Previous Purchases");
            System.Console.WriteLine("[4] Logout");
            System.Console.WriteLine("[0] Close Application");
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            int input = int.Parse(Console.ReadLine() ?? "0");
            ValidateMenuSelection(input, 4);
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
                    ViewPurchaseHistory(currentUser);
                    break;
                }
            case 4:
                {
                    currentUser = null;
                    welcome();
                    askUserForLoginCredentials();
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
            System.Console.WriteLine($"Invalid Selection - Please Select an Option 1-{maxSelection}; or 0 to Close Application");
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
                System.Console.WriteLine($"Enter 'Y' to purchase, 'N' to enter a different plant, or any other key to return to the main menu.");
                string confirmation = Console.ReadLine() ?? "N";
                if (confirmation == "Y" || confirmation =="y")
                {
                    plant = ps.BuyPlant(plant, currentUser);
                    if (plant != null)
                    {
                        //plant.Buyer = currentUser; // This is not updating anything :( Might be an update in SQL that we haven't discussed yet.
                        promptUser = false;
                        System.Console.WriteLine($"Thank you! Your purchase for {plant.PlantName} is complete!");
                        System.Console.WriteLine();

                    }
                }
                else if (confirmation == "N" || confirmation == "n")
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
            string input = Console.ReadLine()?? "0"; 
            int plantId; // Creating int variable to use for GetPlant method
            if (String.IsNullOrEmpty(input)) // returns null if customer presses ENTER to prevent error
            {
                return null;
            }
            try
            {
            plantId = int.Parse(input);
            if (plantId == 0) return null;
            retrievedPlant = ps.GetPlant(plantId);
            }
            catch (FormatException) // handles exception for anything other than an int
            {
                System.Console.WriteLine("Invalid Plant Id."); 
            }
        }
        return retrievedPlant;
    }

    public static void ViewPurchaseHistory(User currentUser)
    {
        // System.Console.WriteLine("***THIS FEATURE IS UNDER CONSTRUCTION***");
        List<Plant> purchases = ps.PurchaseHistory(currentUser);
        for (int i = 0; i < purchases.Count; i ++)
        {
            System.Console.WriteLine(purchases[i]);
        }
        System.Console.WriteLine();

    }

}