public class MoviesArchiveLogic
{
    public static List<MovieModel> _movies = [];

    public MoviesArchiveLogic()
    {
        _movies = MovieArchiveAccess.LoadAll();
    }

    public static MovieModel GetMovieById(int id)
    {
        MovieModel movie = _movies.Find(movie => movie.Id == id);

        return movie;
    }

    public static MovieModel GetMovieByName(string name)
    {
        MovieModel movie = _movies.Find(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return movie;
    }

    public static bool CheckIfMovieInArchive(string name)
    {
        MovieModel movie = GetMovieByName(name);

        return movie != null;
    }

    public static bool CheckIfMovieInArchive(int id)
    {
        MovieModel movie = GetMovieById(id);

        return movie != null;
    }

    public static void AddMovie(MovieModel movie)
    {
        // Conversion from MovieModel to MovieModel
        _movies.Add(movie);
        MovieArchiveAccess.WriteAll(_movies);
    }

    public static void RemoveMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return;
        }
        _movies.Remove(movie);
        MovieArchiveAccess.WriteAll(_movies);
    }

    public void RemoveMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        RemoveMovie(movie);
    }

    public static int GetCount()
    {
        return _movies.Count();
    }
}