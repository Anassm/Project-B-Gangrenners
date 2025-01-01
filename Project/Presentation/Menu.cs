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
        AccountsLogic.CurrentAccount = null;
        string StartMessage = "Welcome to this Cinema";
        string[] MenuNames = {"Login", "Register", "Continue as quest", "Login as admin", "Login as accountant", "About page", "Exit"};
        Action[] Actions = {UserLogin.Start, Register.Start, MainMenu, AdminLogin.Start, AccountantLogin.Start, AboutPage.Start, Exit,};
        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
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
            string StartMessage = "";
            string[] MenuNames = {"Search movies by date and buy ticket", "Search all movies and buy ticket", "See reservations", "Logout",};
            Action[] Actions = {MovieSearch.SearchByDate, MovieSearch.SearchAll, SeeReservations.SeeReservationSubMenu, Start};
            SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
        }
        else
        {
            string StartMessage = "";
            string[] MenuNames = {"Search movies by date and buy ticket", "Search all movies and buy ticket", "Register", "Go back",};
            Action[] Actions = {MovieSearch.SearchByDate, MovieSearch.SearchAll, Register.Start, Start};
            SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
        }
    }
}
