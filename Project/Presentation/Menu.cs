static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static MoviesLogic moviesLogic = new MoviesLogic();
    public static ShowtimesLogic showtimesLogic = new ShowtimesLogic();
    public static SeatsLogic seatsLogic = new SeatsLogic();
    public static ReservationsLogic reservationsLogic = new ReservationsLogic();
    public static AccountsLogic accountsLogic = new AccountsLogic();

    static public void Start()
    {
        //Implement logic to show different menus depending on the user's role

        //This is the main menu
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to continue as guest");
        Console.WriteLine("Enter 3 to login as admin");
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
            AdminLogin.Main();
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
        if (AccountsLogic.CurrentAccount != null)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to buy a ticket");
            Console.WriteLine("Enter 2 to see reservation");
            Console.WriteLine("Enter 3 to logout");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to buy a ticket");
            Console.WriteLine("Enter 2 to register");
            Console.WriteLine("Enter 3 to logout");
        }
        string input = Console.ReadLine();
        Console.Clear();
        switch (input)
        {
            case "1":
                Console.WriteLine("Buying a ticket...");
                BuyTicket.Start(ChooseMovie.StartMovie());
                break;
            case "3":
                Start();
                break;
            case "2":
                if (AccountsLogic.CurrentAccount != null)
                {
                    SeeReservations.SeeReservationSubMenu();
                    break;
                }
                else
                {
                    //REGISTER
                    System.Console.WriteLine("");
                    break;
                }
                
            default:
                Console.WriteLine("Invalid input");
                MainMenu();
                break;
        }


        if (input == "1")
        {
            Console.WriteLine("Buying a ticket...");
            BuyTicket.Start(ChooseMovie.StartMovie());
        }
        else if (input == "2")
        {
            Start();
        }
        else
        {
            Console.WriteLine("Invalid input");
            MainMenu();
        }
    }
}