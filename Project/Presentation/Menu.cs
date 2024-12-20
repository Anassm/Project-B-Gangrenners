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
        Console.WriteLine("Enter 2 to register");
        Console.WriteLine("Enter 3 to continue as guest");
        Console.WriteLine("Enter 4 to login as admin");
        Console.WriteLine("Enter 5 to login as accountant");
        Console.WriteLine("Enter 6 to go to our about page");
        Console.WriteLine("Enter 7 to exit");

        string input = Console.ReadLine();
        Console.Clear();
        switch (input)
        {
            case "1":
                UserLogin.Start();
                break;
            case "2":
                Register.Start();
                break;
            case "3":
                MainMenu();
                break;
            case "4":
                AdminLogin.Start();
                break;
            case "5":
                AccountantLogin.Start();
                break;
            case "6":
                AboutPage.Start();
                break;
            case "7":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }
    }

    static public void MainMenu()
    {
        if (AccountsLogic.CurrentAccount != null)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to search movies by date and buy ticket");
            Console.WriteLine("Enter 2 to search all movies and buy ticket");
            Console.WriteLine("Enter 3 to see reservation");
            Console.WriteLine("Enter 4 to logout");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to search movies by date and buy ticket");
            Console.WriteLine("Enter 2 to search all movies and buy ticket");
            Console.WriteLine("Enter 3 to register");
            Console.WriteLine("Enter 4 to go back to main menu");
        }
        string input = Console.ReadLine();
        Console.Clear();
        switch (input)
        {
            case "1":
                MovieSearch.SearchByDate();
                break;
            case "2":
                MovieSearch.SearchAll();
                break;
            case "3":
                if (AccountsLogic.CurrentAccount != null)
                {
                    SeeReservations.SeeReservationSubMenu();
                }
                else
                {
                    Register.Start();
                }
                break;
            case "4":
                AccountsLogic.CurrentAccount = null;
                Start();
                break;
            default:
                Console.WriteLine("Invalid input");
                MainMenu();
                break;
        }
    }
}
