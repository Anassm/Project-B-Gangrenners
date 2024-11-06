static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        //Implement logic to show different menus depending on the user's role

        //This is the main menu
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to continue as guest");
        Console.WriteLine("Enter 3 to do something else in the future");
        Console.WriteLine("Enter 4 to exit");

        string input = Console.ReadLine();
        Console.Clear();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Console.WriteLine("guest");
            MainMenu();
        }
        else if (input == "3")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else if (input == "4")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }

    static public void MainMenu()
    {
        // TODO: Discuss whether to add interactivity with the user when logged in, or not.
        Console.Clear();
        Console.WriteLine("Enter 1 to buy a ticket");
        Console.WriteLine("Enter 2 to exit");

        string input = Console.ReadLine();
        Console.Clear();
        if (input == "1")
        {
            Console.WriteLine("Buying a ticket...");
            BuyTicket.Start(ChooseMovie.StartMovie());
        }
        else if (input == "2")
        {
            Console.WriteLine("Are you sure you want to exit? (y/n)");
            string exitInput = Console.ReadLine();
            if (exitInput.ToLower() == "y")
            {
                Environment.Exit(0);
            }
            else if (exitInput.ToLower() == "n")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid input");
                MainMenu();
            }
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            MainMenu();
        }
    }
}