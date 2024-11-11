public class MoviesArchiveLogic
{
    private static List<MovieArchiveModel> _movies = [];

    public MoviesArchiveLogic()
    {
        _movies = MovieArchiveAccess.LoadAll();
    }

    public static MovieArchiveModel GetMovieById(int id)
    {
        MovieArchiveModel movie = _movies.Find(movie => movie.Id == id);

        return movie;
    }

    public static MovieArchiveModel GetMovieByName(string name)
    {
        MovieArchiveModel movie = _movies.Find(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return movie;
    }

    public static bool CheckIfMovieInArchive(string name)
    {
        MovieArchiveModel movie = GetMovieByName(name);

        return movie != null;
    }

    public static bool CheckIfMovieInArchive(int id)
    {
        MovieArchiveModel movie = GetMovieById(id);

        return movie != null;
    }

    public static void AddMovie(MovieModel movie)
    {
        // Conversion from MovieModel to MovieArchiveModel
        _movies.Add(new MovieArchiveModel(movie.Id, movie.Name, movie.Genre, movie.Duration));
        MovieArchiveAccess.WriteAll(_movies);
    }
}