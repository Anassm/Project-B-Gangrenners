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
        System.Console.WriteLine("");
        System.Console.WriteLine("Please enter the duration of the movie:");
        string MovieDuration = System.Console.ReadLine();

        if (MoviesArchiveLogic.CheckIfMovieInArchive(MovieName))
        {
            MovieModel MovieToAdd = MoviesArchiveLogic.GetMovieByName(MovieName);
            MovieModel movie = MoviesLogic.GetMovieByName(MovieName);
        }
    }

    public static void RemoveMovieMenu()
    {

    }

    public static void PromoteMovieMenu()
    {

    }

    public static void SeeArchivedMoviesMenu()
    {

    }
}