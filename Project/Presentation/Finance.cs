static class Finance
{
    public static void Start()
    {
        string StartMessage = "Financial data";
        string[] MenuNames = { "Current movies", "Past movies", "All movies", "Go back to accountant menu" };
        Action[] Actions = { () => DisplayMoviesFinance(true, false), () => DisplayMoviesFinance(false, true), () => DisplayMoviesFinance(true, true), AccountantLogin.AccountantMenu };
        SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
    }

    public static void FollowUp()
    {
        string StartMessage = "";
        string[] MenuNames = { "Go back to finance menu", "Go back to accountant menu" };
        bool YesNo = SelectingMenu.YesNoSelect(StartMessage, MenuNames);
        if (YesNo)
        {
            Start();
        }
        else
        {
            AccountantLogin.AccountantMenu();
        }
    }

    public static void DisplayMoviesFinance(bool current, bool past)
    {
        bool hasDisplayedMovies = false; // Track if any movies are displayed
        List<MovieModel> MoviesList = [];
        List<MovieModel> CurrentMovies = MoviesLogic._movies;
        List<MovieModel> PastMovies = MoviesArchiveLogic._movies;
        List<MovieModel> AllMovies = [];
        AllMovies.AddRange(CurrentMovies);
        AllMovies.AddRange(PastMovies);

        if (current && !past)
        {
            MoviesList = CurrentMovies;
        }
        else if (!current && past)
        {
            MoviesList = PastMovies;
        }
        else
        {
            MoviesList = AllMovies;
        }
        //==============================================================================================
        int selectedIndex = 0;
        string[] menuOptions = { "Go back to finance menu", "Go back to accountant menu" };
        while (true)
        {
            PresentationHelper.ClearConsole();
            foreach (MovieModel movie in MoviesList)
            {

                hasDisplayedMovies = true;

                Dictionary<string, int> reservations = MoviesLogic.CalculateTotalReservationsPerMovie(movie);
                double profit = MoviesLogic.CalculateTotalRevenueForFilm(movie) - movie.Cost;

                Console.WriteLine(movie.Name);
                Console.WriteLine($"Regular: {reservations["Regular"]}");
                Console.WriteLine($"VIP: {reservations["VIP"]}");
                Console.WriteLine($"VIP+: {reservations["VIP+"]}");
                Console.WriteLine($"Total Revenue: {MoviesLogic.CalculateTotalRevenueForFilm(movie)}");
                Console.WriteLine($"Total Cost: {movie.Cost}");
                if (profit >= 0)
                {
                    PresentationHelper.PrintGreen($"Total Profit: {profit}");
                }
                else
                {
                    PresentationHelper.PrintRed($"Total Profit: {profit}");
                }

                Console.WriteLine("----------------------------------------------");
            }

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                    Console.ResetColor();

                }
                else
                {
                    Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? menuOptions.Length - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == menuOptions.Length - 1) ? 0 : selectedIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    // Handle selection
                    if (selectedIndex == 0)
                    {
                        Start();
                    }
                    else if (selectedIndex == 1)
                    {
                        AccountantLogin.AccountantMenu();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
