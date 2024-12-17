using System.Linq;
using System.Runtime.CompilerServices;
public static class ManageMovies
{
    private static MoviesLogic _moviesLogic = new MoviesLogic();
    static private MoviesArchiveLogic _moviesArchiveLogic = new MoviesArchiveLogic();

    public static void AddMovieMenu()
    {
        System.Console.WriteLine("You have chosen to add a movie.");
        System.Console.WriteLine("If you want to quit type q or quit.");
        System.Console.WriteLine("Please give the info needed to add a movie.");
        System.Console.WriteLine("");

        // Name input
        string MovieName;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the name of the movie:");
            MovieName = PresentationHelper.StringInput(AdminLogin.AdminMenu);
            if (MovieName != "")
            {
                break;
            }
        }

        // Genre input
        string MovieGenre;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the genre of the movie:");
            MovieGenre = PresentationHelper.StringInput(AdminLogin.AdminMenu);
            if (MovieGenre != "")
            {
                break;
            }
        }

        // Duration input
        int MovieDuration = 0;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the duration of the movie:");
            MovieDuration = PresentationHelper.IntInput(AdminLogin.AdminMenu);
            if (MovieDuration > 0)
            {
                break;
            }
        }

        // Summary input
        string MovieSummary;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the Summary of the movie:");
            MovieSummary = PresentationHelper.StringInput(AdminLogin.AdminMenu);
            Console.Clear();
            if (MovieSummary == "")
            {
                MovieSummary = "No summary available";
                break;
            }
        }

        // Cost input
        int Cost = 0;
        while (true)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please give the cost:");
            Cost = PresentationHelper.IntInput(AdminLogin.AdminMenu);
            if (Cost > 0)
            {
                break;
            }
        }

        // Check if movie is in archive, act accordingly
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
                    MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration, MovieSummary, Cost);
                    System.Console.WriteLine("The movie was successfully added.");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Give any input to go back to admin menu.");
                    PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
                }
                MoviesArchiveLogic.RemoveMovie(Movie);
                MoviesLogic.AddMovie(Movie);
                System.Console.WriteLine("The movie was removed from the archive and added to current movies.");
                System.Console.WriteLine("");
                System.Console.WriteLine("Give any input to go back to admin menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
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
            PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
        }
        MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration, MovieSummary, Cost);
        System.Console.WriteLine("The movie was successfully added.");
        System.Console.WriteLine("");
        System.Console.WriteLine("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
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
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
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
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
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
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }

    public static void SeeArchivedMoviesMenu()
    {
        PresentationHelper.ClearConsole();
        while (true)
        {
            System.Console.WriteLine("You have chosen to see all archived movies.");
            if (PresentationHelper.Continue(AdminLogin.AdminMenu))
            {
                System.Console.WriteLine("---------------------------------------");
                foreach (MovieModel movie in MoviesArchiveLogic._movies)
                {
                    System.Console.WriteLine(movie.ToStringComplete());
                    System.Console.WriteLine("---------------------------------------");
                }
                System.Console.WriteLine("Archived list shown above.");
                System.Console.WriteLine("");
                System.Console.WriteLine("Give any input to go back to admin menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
            else
            {
                continue;
            }
        }
    }

    public static void SeeCurrentMoviesMenu()
    {
        PresentationHelper.ClearConsole();
        while (true)
        {
            System.Console.WriteLine("You have chosen to see all current movies.");
            if (PresentationHelper.Continue(AdminLogin.AdminMenu))
            {
                System.Console.WriteLine("---------------------------------------");
                foreach (MovieModel movie in MoviesLogic._movies)
                {
                    System.Console.WriteLine(movie.ToStringComplete());
                    System.Console.WriteLine("---------------------------------------");
                }
                System.Console.WriteLine("Current movie list shown above.");
                System.Console.WriteLine("");
                System.Console.WriteLine("Give any input to go back to admin menu.");
                PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
            }
            else
            {
                continue;
            }
        }
    }
}