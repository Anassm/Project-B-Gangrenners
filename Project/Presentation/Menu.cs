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
        string[] MenuNames = { "Login", "Register", "Search movie", "About page", "Exit" };
        Action[] Actions = { LoginSubMenu, Register.Start, MainMenu, AboutPage.Start, Exit, };
        SelectingMenu.MenusSelectMainMenu(MenuNames, Actions);
    }

    static public void LoginSubMenu()
    {
        string[] MenuNames = { "User", "Admin", "Accountant", "Go back" };
        Action[] Actions = { UserLogin.Start, AdminLogin.Start, AccountantLogin.Start, Start };
        SelectingMenu.MenusSelect(MenuNames, Actions, "Log in as:");
    }

    public static void DynamicSubscriptionOption()
    {
        if (SubscriptionLogic.IsSubscribed(AccountsLogic.CurrentAccount.Id) || SubscriptionLogic.IsSubscriptionCancelledButValid(AccountsLogic.CurrentAccount.Id))
        {
            Subscription.ManageMenu();
        }
        else
        {
            if (Subscription.TermsAndServicesAgreement() == 0)
            {
                MainMenu();
            }
            else
            {
                SubscriptionLogic.AddSubscription(AccountsLogic.CurrentAccount.Id);
                MainMenu();
            }
        }
    }

    public static void Exit()
    {
        System.Console.WriteLine("bye bye");
        Environment.Exit(0);
    }

    static public void MainMenu()
    {


        if (AccountsLogic.CurrentAccount != null)
        {
            string subscriptionText = SubscriptionLogic.IsSubscribed(AccountsLogic.CurrentAccount.Id) || SubscriptionLogic.IsSubscriptionCancelledButValid(AccountsLogic.CurrentAccount.Id) ? "manage your" : "opt in to a";
            string StartMessage = "Welcome back " + AccountsLogic.CurrentAccount.FirstName + " " + AccountsLogic.CurrentAccount.LastName + ", what would you like to do?";
            string[] MenuNames = { "Search movies by date and buy ticket", "Search all movies and buy ticket", "See reservations", $"{subscriptionText} subscription", "Logout", };
            Action[] Actions = { MovieSearch.SearchByDate, MovieSearch.SearchAll, SeeReservations.SeeReservationSubMenu, DynamicSubscriptionOption, Start };
            SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
        }
        else
        {
            string StartMessage = "";
            string[] MenuNames = { "Search movies by date and buy ticket", "Search all movies and buy ticket", "Go back", };
            Action[] Actions = { MovieSearch.SearchByDate, MovieSearch.SearchAll, Start };
            SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
        }
    }
}
