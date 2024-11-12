using System.Linq;
static class ManageMovies
{
    public static void AddMovieMenu()
    {
        System.Console.WriteLine("You have chosen to add a movie.");
        System.Console.WriteLine("Please give the info needed to add a movie.");
        System.Console.WriteLine("");
        System.Console.WriteLine("Please enter the name of the movie:");
        string MovieName = System.Console.ReadLine();
        System.Console.WriteLine("");
        System.Console.WriteLine("Please enter the genre of the movie:");
        string MovieGenre = System.Console.ReadLine();
        bool incorrect = false;
        int MovieDuration = 0;
        while (incorrect == false)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Please enter the duration of the movie:");
            string input = System.Console.ReadLine();
            if(input.All(char.IsDigit))
            {
                MovieDuration = Convert.ToInt32(input);
            }
        }
        if (MoviesArchiveLogic.CheckIfMovieInArchive(MovieName))
        {
            MovieModel Movie = MoviesArchiveLogic.GetMovieByName(MovieName);
            MoviesArchiveLogic.RemoveMovie(Movie);
            MoviesLogic.AddMovie(Movie);
            System.Console.WriteLine("The movie was removed from the archive and added to current movies.");
            return;
        }
        MoviesLogic.AddMovie(MovieName, MovieGenre, MovieDuration);
        System.Console.WriteLine("The movie has been added to current movies.");
        Console.Clear();
    }

    public static void RemoveMovieMenu()
    {
        System.Console.WriteLine("You have chosen to remove a movie.");
        System.Console.WriteLine("Please give the");
    }

    public static void PromoteMovieMenu()
    {

    }

    public static void SeeArchivedMoviesMenu()
    {

    }
}