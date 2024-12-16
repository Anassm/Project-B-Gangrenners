static class Finance
{
    public static void Start()
    {
        Console.WriteLine("Financial data");
        Console.WriteLine("1. Current movies");
        Console.WriteLine("2. Past movies");
        Console.WriteLine("3. All movies");
        Console.WriteLine("4. Go back to admin menu");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                DisplayMoviesFinance(true, false);
                FollowUp();
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                DisplayMoviesFinance(false, true);
                FollowUp();
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                DisplayMoviesFinance(true, true);
                FollowUp();
                break;
            case "4":
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                AdminLogin.AdminMenu();
                break;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }
    }

    public static void FollowUp()
    {
        Console.WriteLine("1. Go back to finance menu");
        Console.WriteLine("2. Go back to accountant menu");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Start();
                break;
            case "2":
                Console.Clear();
                AccountantLogin.AccountantMenu();
                break;
            default:
                Console.WriteLine("Invalid input");
                FollowUp();
                break;
        }
    }

    public static void DisplayMoviesFinance(bool current, bool past)
    {
        bool hasDisplayedMovies = false; // Track if any movies are displayed

        foreach (MovieModel movie in MoviesLogic._movies)
        {
            if (current && !past)
            {
                if (ShowtimesLogic.GetShowtimesByMovieId(movie.Id).Count == 0)
                {
                    continue;
                }
            }
            else if (!current && past)
            {
                if (ShowtimesLogic.GetShowtimesByMovieId(movie.Id).Count != 0)
                {
                    continue;
                }
            }

            // If we reach here, at least one movie matches the criteria
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Total Profit: {profit}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Total Profit: {profit}");
            }
            Console.ResetColor();

            Console.WriteLine("----------------------------------------------");
        }

        // If no movies were displayed, show a message
        if (!hasDisplayedMovies)
        {
            Console.WriteLine("There are no movies to display.");
        }

        Console.WriteLine("");
    }

}