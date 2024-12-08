using System.Linq;
public static class ManageMovies
{
    private static MoviesLogic _moviesLogic = new MoviesLogic();
    static private MoviesArchiveLogic _moviesArchiveLogic = new MoviesArchiveLogic();
    public static void AddMovieMenu()
    {
        System.Console.WriteLine("You have chosen to add a movie.");
        System.Console.WriteLine("If you want to quit type Q or quit, this does not work when entering duration.");
        System.Console.WriteLine("Please give the info needed to add a movie.");
        System.Console.WriteLine("");

        // Name input
        string MovieName;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the name of the movie:");
            MovieName = System.Console.ReadLine().ToLower();
            if (MovieName == "q" || MovieName == "quit")
            {
                AdminLogin.AdminMenu();
            }
            if (MovieName != "")
            {
                break;
            }
            Console.Clear();
            System.Console.WriteLine("Invalid input.");
            System.Console.WriteLine("");
        }

        // Genre input
        string MovieGenre;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the genre of the movie:");
            MovieGenre = System.Console.ReadLine();
            if (MovieGenre == "q" || MovieGenre == "quit")
            {
                AdminLogin.AdminMenu();
            }
            if (MovieGenre != "")
            {
                break;
            }
            Console.Clear();
            System.Console.WriteLine("Please give an input.");
        }

        // Duration input
        int MovieDuration = 0;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the duration of the movie:");
            string input = System.Console.ReadLine();
            if (input.All(char.IsDigit))
            {
                MovieDuration = Convert.ToInt32(input);
                if (MovieDuration < 0)
                {
                    System.Console.WriteLine("Can not be a negative number.");
                    continue;
                }
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

        // Summary input
        string MovieSummary;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the Summary of the movie:");
            MovieSummary = System.Console.ReadLine();
            if (MovieSummary == "q" || MovieSummary == "quit")
            {
                AdminLogin.AdminMenu();
            }
            if (MovieSummary == "")
            {
                MovieSummary = "No summary available";
                break;
            }
            Console.Clear();
            System.Console.WriteLine("Invalid input.");
        }

        int cost = 0;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please give the cost:");
            string costString = Console.ReadLine();
            System.Console.WriteLine("");
            if (costString.All(char.IsDigit))
            {
                cost = Convert.ToInt32(costString);
                if (cost < 0)
                {
                    System.Console.WriteLine("Can not be a negative number.");
                    continue;
                }
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

            // Check if movie is in archive
            if (MoviesArchiveLogic.CheckIfMovieInArchive(MovieName))
            {
                MovieModel Movie = MoviesArchiveLogic.GetMovieByName(MovieName);
                if (Movie.Duration != MovieDuration || Movie.Genre != MovieGenre)
                {
                    Console.Clear();
                    System.Console.WriteLine("The movie you are trying to add is already in the archive but with different genre or duration.");
                    System.Console.WriteLine("Do you want to remove the movie from the archive and add it to current movies? (Y/N)");
                    string input = Console.ReadLine().ToLower();
                    if (input == "n" || input == "no")
                    {
                        System.Console.WriteLine("Do you want to add the movie with the same name but different genre or duration? (Y/N)");
                        string input2 = Console.ReadLine().ToLower();
                        if (input2 == "n" || input2 == "no")
                        {
                            Console.Clear();
                            AdminLogin.AdminMenu();
                        }
                        MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration, MovieSummary);
                        System.Console.WriteLine("The movie was successfully added.");
                        System.Console.WriteLine("");
                        System.Console.WriteLine("Give any input to go back to admin menu.");
                        ConsoleKeyInfo key1 = Console.ReadKey(true);
                        if (key1.Key != null)
                        {
                            Console.Clear();
                            AdminLogin.AdminMenu();
                        }
                    }
                    MoviesArchiveLogic.RemoveMovie(Movie);
                    MoviesLogic.AddMovie(Movie);
                    System.Console.WriteLine("The movie was removed from the archive and added to current movies.");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Give any input to go back to admin menu.");
                    ConsoleKeyInfo key3 = Console.ReadKey(true);
                    if (key3.Key != null)
                    {
                        Console.Clear();
                        AdminLogin.AdminMenu();
                    }
                }
                else if (Movie.Duration == MovieDuration && Movie.Genre == MovieGenre)
                {
                    Console.Clear();
                    System.Console.WriteLine("The movie you are trying to add is already in the archive.");
                    System.Console.WriteLine("Do you want to remove the movie from the archive and add it to current movies? (Y/N)");
                    string input = Console.ReadLine().ToLower();
                    if (input == "n" || input == "no")
                    {
                        Console.Clear();
                        AdminLogin.AdminMenu();
                    }
                }
                MoviesArchiveLogic.RemoveMovie(Movie);
                MoviesLogic.AddMovie(Movie);
                System.Console.WriteLine("The movie was removed from the archive and added to current movies.");
                System.Console.WriteLine("");
                System.Console.WriteLine("Give any input to go back to admin menu.");
                ConsoleKeyInfo key2 = Console.ReadKey(true);
                if (key2.Key != null)
                {
                    Console.Clear();
                    AdminLogin.AdminMenu();
                }
            }
            MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration);
            System.Console.WriteLine("The movie was successfully added.");
            System.Console.WriteLine("");
            System.Console.WriteLine("Give any input to go back to admin menu.");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key != null)
            {
                Console.Clear();
                AdminLogin.AdminMenu();
            }
    }

    public static void RemoveMovieMenu()
    {
        System.Console.WriteLine("You have chosen to remove a movie.");
        System.Console.WriteLine("Please give the name of the movie.");
        System.Console.WriteLine("If you want to quit type Q or quit");
        string input = System.Console.ReadLine().ToLower();
        if (input == "q" || input == "quit")
        {
            AdminLogin.AdminMenu();
        }
        _moviesLogic.ToString();
        MovieModel movie = MoviesLogic.GetMovieByName(input);
        if (movie == null)
        {
            Console.Clear();
            System.Console.WriteLine("This movie does not exist.");
            RemoveMovieMenu();
        }
        MoviesLogic.RemoveMovie(movie);
        System.Console.WriteLine("Movie successfully removed and added to the archive.");
        System.Console.WriteLine("");
        System.Console.WriteLine("Give any input to go back to admin menu.");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            AdminLogin.AdminMenu();
        }
    }


    public static void PromoteMovieMenu()
    {
        System.Console.WriteLine("You have chosen to promote a movie.");
        System.Console.WriteLine("Please give the name of the movie.");
        System.Console.WriteLine("If you want to quit type Q or quit");
        string input = System.Console.ReadLine().ToLower();
        if (input == "q" || input == "quit")
        {
            AdminLogin.AdminMenu();
        }
        MovieModel movie = MoviesLogic.GetMovieByName(input);
        if (movie == null || !MoviesLogic.CheckIfMovieInMovies(input) == false)
        {
            Console.Clear();
            System.Console.WriteLine("This movie does not exist.");
            RemoveMovieMenu();
        }
        bool prom = MoviesLogic.PromoteMovie(movie);
        if (prom == false)
        {
            Console.Clear();
            System.Console.WriteLine("This movie cannot be promoted.");
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine("Movie was successfully promoted.");

        }

        System.Console.WriteLine("");
        System.Console.WriteLine("Give any input to go back to admin menu.");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            AdminLogin.AdminMenu();
        }
    }

    public static void DemoteMovieMenu()
    {
        System.Console.WriteLine("You have chosen to demote a movie.");
        System.Console.WriteLine("Please give the name of the movie.");
        System.Console.WriteLine("If you want to quit type Q or quit");
        string input = System.Console.ReadLine().ToLower();
        if (input == "q" || input == "quit")
        {
            AdminLogin.AdminMenu();
        }
        MovieModel movie = MoviesLogic.GetMovieByName(input);
        if (movie == null || MoviesLogic.CheckIfMovieInMovies(input) == false)
        {
            Console.Clear();
            System.Console.WriteLine("This movie does not exist.");
            DemoteMovieMenu();
        }
        bool prom = MoviesLogic.unPromoteMovie(movie);
        if (prom == false)
        {
            Console.Clear();
            System.Console.WriteLine("This movie cannot be demoted.");
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine("Movie was successfully demoted.");

        }

        System.Console.WriteLine("");
        System.Console.WriteLine("Give any input to go back to admin menu.");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            AdminLogin.AdminMenu();
        }
    }

    public static void SeeArchivedMoviesMenu()
    {
        System.Console.WriteLine("You have chosen to see all archived.");
        System.Console.WriteLine("Do you want to continue? (Y/N)");
        string input = Console.ReadLine().ToLower();
        if (input == "n" || input == "no")
        {
            AdminLogin.AdminMenu();
        }
        MoviesArchiveLogic Archive = new();
        System.Console.WriteLine("---------------------------------------");
        foreach (MovieModel movie in MoviesArchiveLogic._movies)
        {
            System.Console.WriteLine(movie.ToStringComplete());
            System.Console.WriteLine("---------------------------------------");
        }
        System.Console.WriteLine("Archive list shown above.");
        System.Console.WriteLine("");
        System.Console.WriteLine("Give any input to go back to admin menu.");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            AdminLogin.AdminMenu();
        }
    }

    public static void SeeCurrentMoviesMenu()
    {
        System.Console.WriteLine("You have chosen to see all current movies.");
        System.Console.WriteLine("Do you want to continue? (Y/N)");
        string input = Console.ReadLine().ToLower();
        if (input == "n" || input == "no")
        {
            AdminLogin.AdminMenu();
        }
        MoviesLogic Archive = new();
        System.Console.WriteLine("---------------------------------------");
        foreach (MovieModel movie in MoviesLogic._movies)
        {
            System.Console.WriteLine(movie.ToStringComplete());
            System.Console.WriteLine("---------------------------------------");
        }
        System.Console.WriteLine("current movie list shown above.");
        System.Console.WriteLine("");
        System.Console.WriteLine("Give any input to go back to admin menu.");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key != null)
        {
            Console.Clear();
            AdminLogin.AdminMenu();
        }
    }

    public static void AddShowTimesMenu()
    {
        System.Console.WriteLine("you have chosen to add ShowTimes.");
        System.Console.WriteLine("Do you want to continue? (Y/N)");
        string input = Console.ReadLine().ToLower();
        if (input == "n" || input == "no")
        {
            AdminLogin.AdminMenu();
        }
        Console.Clear();

        //moviename input
        System.Console.WriteLine("");
        System.Console.WriteLine("Please give the name of the movie:");
        string movieName = Console.ReadLine();
        if (MoviesLogic.GetMovieByName(movieName) == null)
        {
            while (true)
            {
                System.Console.WriteLine("This movie does not exist, would you like to add the movie? (Y/N)");
                string input1 = Console.ReadLine().ToLower();
                if (input1 == "y" || input1 == "yes")
                {
                    AddMovieMenu();
                }
                else if (input1 == "n" || input1 == "no")
                {
                    System.Console.WriteLine("Chosen not to add a new movie, press anything to go back to the menu.");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != null)
                    {
                        Console.Clear();
                        AdminLogin.AdminMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    System.Console.WriteLine("Invalid input.");
                }
            }
        }

        //start date input
        DateOnly date = DateOnly.Parse("0000-00-00");
        TimeOnly time = TimeOnly.Parse("00:00");
        while(true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please give the date of the first showing (YYYY-MM-DD):");
            string dateString = Console.ReadLine();
            if (MoviesLogic.IsValidDateFormat(dateString))
            {
                date = DateOnly.Parse(dateString);
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid format.");
                continue;
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("please give the time when the movie is played (HH:MM):");
            string playtime = Console.ReadLine();
            if (MoviesLogic.IsValidTimeOnlyFormat(playtime))
            {
                time = TimeOnly.Parse(playtime);
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid format.");
                continue;
            }
            DateTime datetime = date.ToDateTime(time);
            if(DateTime.Now > datetime)
            {
                Console.Clear();
                System.Console.WriteLine("Can not add screenings for in the past, please give a valid date and/or time");
                continue;
            }
            while (true)
            {
                System.Console.WriteLine($"Start date and time: {datetime}");
                System.Console.WriteLine("Is this correct? (Y/N)");
                string check1 = Console.ReadLine().ToLower();
                if (check1 == "y" || check1 == "yes")
                {
                    break;
                }
                else if (check1 == "n" || check1 == "no")
                {
                    AddShowTimesMenu();
                }
                else
                {
                    System.Console.WriteLine("Invalid input.");
                }
            }
        }

        //end date input
        System.Console.WriteLine("");
        System.Console.WriteLine("please give the date you would like the showings to end (YYYY-MM-DD):");
        string dateString2 = Console.ReadLine();
        DateOnly date2 = DateOnly.Parse(dateString2);

        //interval input
        System.Console.WriteLine("");
        System.Console.WriteLine("please give the amount of days between each showing:");
        int interval = Convert.ToInt32(Console.ReadLine());

        //showing time input


        int hallId = 0;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please give the hall ID:");
            string hallIdString = Console.ReadLine();
            System.Console.WriteLine("");
            if (hallIdString.All(char.IsDigit))
            {
                hallId = Convert.ToInt32(hallIdString);
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }

        while (true)
        {
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine($"Movie name: {movieName}");
            System.Console.WriteLine($"Start date: {date}");
            System.Console.WriteLine($"End date: {date2}");
            System.Console.WriteLine($"Days between showings: {interval}");
            System.Console.WriteLine($"Time of showing: {time}");
            System.Console.WriteLine($"Hall ID: {hallId}");
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("is the above correct? (Y/N)");
            string input2 = Console.ReadLine().ToLower();
            if (input2 == "y" || input2 == "yes")
            {
                List<DateTime> datetimes = ShowtimesLogic.GenerateDateTimesList(date, date2, time, interval);
                List<ShowtimeModel> showtimes = ShowtimesLogic.GenerateShowTimesList(movieName, hallId, datetimes);
                ShowtimesLogic.AddShowTimes(showtimes);
                break;
            }
            Console.Clear();
            System.Console.WriteLine("Invalid input, press anything to try again.");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key != null)
            {
                Console.Clear();
                AddShowTimesMenu();
            }
        }
    }
}