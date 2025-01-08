using System.Linq;
using System.Runtime.CompilerServices;
public static class ManageMovies
{
    private static MoviesLogic _moviesLogic = new MoviesLogic();
    static private MoviesArchiveLogic _moviesArchiveLogic = new MoviesArchiveLogic();

    public static void AddMovieMenu()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("You have chosen to add a movie.");
        PresentationHelper.PrintYellow("Please give the info needed to add a movie.");
        System.Console.WriteLine("");

        // Name input
        string MovieName;
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please enter the name of the movie:");
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
            PresentationHelper.PrintYellow("Please enter the genre of the movie:");
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
            PresentationHelper.PrintYellow("Please enter the duration of the movie:");
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
            PresentationHelper.PrintYellow("Please enter the Summary of the movie:");
            MovieSummary = PresentationHelper.StringInput(AdminLogin.AdminMenu);
            Console.Clear();
            if (MovieSummary == "")
            {
                MovieSummary = "No summary available";
                break;
            }
            break;
        }

        // Cost input
        int Cost = 0;
        while (true)
        {
            System.Console.WriteLine("");
            PresentationHelper.PrintYellow("Please give the cost:");
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
                PresentationHelper.ClearConsole();
                string StartMessage = "the movie you are trying to add is already in the archive but with different genre and/or duration.\nDo you want to remove the movie from the archive and add it to current movies?";
                bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
                if (YesNo)
                {
                    MoviesArchiveLogic.RemoveMovie(Movie);
                    MoviesLogic.AddMovie(Movie);
                    PresentationHelper.PrintGreen("The movie was removed from the archive and added to current movies.");
                    System.Console.WriteLine("");
                    PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
                    PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
                }
                else
                {
                    string StartMessage2 = "Do you want to add the movie with the same name but different genre or duration?";
                    bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
                    if (YesNo2)
                    {
                        MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration, MovieSummary, Cost);
                        PresentationHelper.PrintGreen("The movie was successfully added.");
                        System.Console.WriteLine("");
                        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
                        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
                    }
                    else
                    {
                        PresentationHelper.ClearConsole();
                        AdminLogin.AdminMenu();
                    }
                }
            }
            else if (Movie.Duration == MovieDuration && Movie.Genre == MovieGenre)
            {
                Console.Clear();
                System.Console.WriteLine("The movie you are trying to add is already in the archive.");
                string StartMessage = "Do you want to remove the movie from the archive and add it to current movies?";
                bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
                if (YesNo)
                {
                    MoviesArchiveLogic.RemoveMovie(Movie);
                    MoviesLogic.AddMovie(Movie);
                    PresentationHelper.PrintGreen("The movie was removed from the archive and added to current movies.");
                    System.Console.WriteLine("");
                    PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
                    PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
                }
                else
                {
                    Console.Clear();
                    AdminLogin.AdminMenu();
                }
            }
        }
        MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration, MovieSummary, Cost);
        PresentationHelper.PrintGreen("The movie was successfully added.");
        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }

    public static void RemoveMovieMenu()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("You have chosen to remove a movie.");
        PresentationHelper.PrintYellow("Please give the name of the movie.");
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
            PresentationHelper.PrintRed("This movie does not exist.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
            PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
        }
        MoviesLogic.RemoveMovie(movie);
        PresentationHelper.PrintGreen("Movie successfully removed and added to the archive.");
        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }


    public static void PromoteMovieMenu()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("You have chosen to promote a movie.");
        PresentationHelper.PrintYellow("Please give the name of the movie.");
        string input = System.Console.ReadLine().ToLower();
        if (input == "q" || input == "quit")
        {
            AdminLogin.AdminMenu();
        }
        MovieModel movie = MoviesLogic.GetMovieByName(input);
        if (movie == null || MoviesLogic.CheckIfMovieInMovies(input) == false)
        {
            Console.Clear();
            PresentationHelper.PrintRed("This movie does not exist.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
            PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
        }
        bool prom = MoviesLogic.PromoteMovie(movie);
        if (prom == false)
        {
            Console.Clear();
            PresentationHelper.PrintRed("This movie cannot be promoted.");
        }
        else
        {
            Console.Clear();
            PresentationHelper.PrintGreen("Movie was successfully promoted.");

        }

        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }

    public static void DemoteMovieMenu()
    {
        PresentationHelper.ClearConsole();
        PresentationHelper.PrintGreen("You have chosen to demote a movie.");
        PresentationHelper.PrintYellow("Please give the name of the movie.");
        string input = System.Console.ReadLine().ToLower();
        if (input == "q" || input == "quit")
        {
            AdminLogin.AdminMenu();
        }
        MovieModel movie = MoviesLogic.GetMovieByName(input);
        if (movie == null || MoviesLogic.CheckIfMovieInMovies(input) == false)
        {
            Console.Clear();
            PresentationHelper.PrintRed("This movie does not exist.");
            DemoteMovieMenu();
        }
        bool prom = MoviesLogic.unPromoteMovie(movie);
        if (prom == false)
        {
            Console.Clear();
            PresentationHelper.PrintRed("This movie cannot be demoted.");
        }
        else
        {
            Console.Clear();
            PresentationHelper.PrintGreen("Movie was successfully demoted.");

        }
        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }

    public static void SeeArchivedMoviesMenu()
    {
        PresentationHelper.ClearConsole();

        System.Console.WriteLine("---------------------------------------");
        foreach (MovieModel movie in MoviesArchiveLogic._movies)
        {
            System.Console.WriteLine(movie.ToStringComplete());
            System.Console.WriteLine("---------------------------------------");
        }
        PresentationHelper.PrintGreen("Archived list shown above.");
        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }

    public static void SeeCurrentMoviesMenu()
    {
        PresentationHelper.ClearConsole();
        System.Console.WriteLine("---------------------------------------");
        foreach (MovieModel movie in MoviesLogic._movies)
        {
            System.Console.WriteLine(movie.ToStringComplete());
            System.Console.WriteLine("---------------------------------------");
        }
        PresentationHelper.PrintGreen("Current movie list shown above.");
        System.Console.WriteLine("");
        PresentationHelper.PrintYellow("Give any input to go back to admin menu.");
        PresentationHelper.PressAnyToContinue(AdminLogin.AdminMenu);
    }
}