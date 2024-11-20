using System.Linq;
public static class ManageMovies
{
    private static MoviesLogic _moviesLogic = new MoviesLogic();
    static private MoviesArchiveLogic _moviesArchiveLogic = new MoviesArchiveLogic();
    public static void AddMovieMenu()
    {
        _moviesLogic.ToString();
        _moviesArchiveLogic.ToString();
        System.Console.WriteLine("You have chosen to add a movie.");
        System.Console.WriteLine("If you want to quit type Q or quit, this does not work when entering duration.");
        System.Console.WriteLine("Please give the info needed to add a movie.");
        System.Console.WriteLine("");
        System.Console.WriteLine("Please enter the name of the movie:");
        string MovieName = System.Console.ReadLine();
        if (MovieName == "q" || MovieName == "quit")
        {
            AdminLogin.AdminMenu();
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("Please enter the genre of the movie:");
        string MovieGenre = System.Console.ReadLine();
        if (MovieGenre == "q" || MovieGenre == "quit")
        {
            AdminLogin.AdminMenu();
        }
        int MovieDuration = 0;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the duration of the movie:");
            string input = System.Console.ReadLine();
            if(input.All(char.IsDigit))
            {
                MovieDuration = Convert.ToInt32(input);
                break;
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input.");
            }
        }
        if (MoviesArchiveLogic.CheckIfMovieInArchive(MovieName))
        {
            MovieModel Movie = MoviesArchiveLogic.GetMovieByName(MovieName);
            if(Movie.Duration != MovieDuration || Movie.Genre != MovieGenre)
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
                    MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration);
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
            else if(Movie.Duration == MovieDuration && Movie.Genre == MovieGenre)
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
        _moviesLogic.ToString();
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
        _moviesLogic.ToString();
        _moviesArchiveLogic.ToString();
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
        _moviesLogic.ToString();
        _moviesArchiveLogic.ToString();
        System.Console.WriteLine("You have chosen to promote a movie.");
        System.Console.WriteLine("Please give the name of the movie.");
        System.Console.WriteLine("If you want to quit type Q or quit");
        string input = System.Console.ReadLine().ToLower();
        if (input == "q" || input == "quit")
        {
            AdminLogin.AdminMenu();
        }
        MovieModel movie = MoviesLogic.GetMovieByName(input);
        if (movie == null && MoviesLogic.CheckIfMovieInMovies(input))
        {
            Console.Clear();
            System.Console.WriteLine("This movie does not exist.");
            RemoveMovieMenu();
        }
        bool prom = MoviesLogic.PromoteMovie(movie);
        if(prom == false)
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

    public static void SeeArchivedMoviesMenu()
    {
        _moviesLogic.ToString();
        _moviesArchiveLogic.ToString();
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
        _moviesLogic.ToString();
        _moviesArchiveLogic.ToString();
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
}