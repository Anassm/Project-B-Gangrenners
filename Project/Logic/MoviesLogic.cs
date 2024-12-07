public class MoviesLogic
{
    public static List<MovieModel> _movies { get; set; } = MoviesAccess.LoadAll();

    public MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll() ?? new List<MovieModel>();
    }

    public static MovieModel GetMovieById(int id)
    {
        MovieModel movie = _movies.Find(movie => movie.Id == id);
        return movie;
    }

    public static MovieModel GetMovieByName(string name)
    {
        MovieModel movie = _movies.Find(movie => movie.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false);

        return movie;
    }

    public static bool CheckIfMovieInMovies(string name)
    {
        MovieModel movie = GetMovieByName(name);

        return movie != null;
    }

    public static bool CheckIfMovieInMovies(int id)
    {
        MovieModel movie = GetMovieById(id);

        return movie != null;
    }

    public static void AddMovie(MovieModel movie)
    {
        if (CheckIfMovieInMovies(movie.Name))
        {
            return;
        }
        if (MoviesArchiveLogic.CheckIfMovieInArchive(movie.Name))
        {
            MoviesArchiveLogic.RemoveMovie(movie);
        }
        _movies.Add(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public static void AddMovie(string name, string genre, int duration)
    {
        AddMovie(name, genre, duration, "No summary available");
    }

    public static void AddMovie(string name, string genre, int duration, string summary)
    {
        MovieModel movie = new MovieModel(GetNextId(), name, genre, duration, false, summary);
        AddMovie(movie);
    }

    public static void RemoveMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return;
        }
        unPromoteMovie(movie);
        MoviesArchiveLogic.AddMovie(movie);
        _movies.Remove(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public static void RemoveMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        RemoveMovie(movie);
    }

    public static bool PromoteMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return false;
        }
        movie.Promoted = true;
        MoviesAccess.WriteAll(_movies);
        return true;
    }

    public static bool PromoteMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        return(PromoteMovie(movie));
    }

    public static bool unPromoteMovie(MovieModel movie)
    {
        if (movie == null)
        {
            return false;
        }
        movie.Promoted = false;
        MoviesAccess.WriteAll(_movies);
        return true;
    }

    public static bool unPromoteMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        return(PromoteMovie(movie));
    }

    public static int GetNextId()
    {
        return _movies.Count() + MoviesArchiveLogic.GetCount() + 1;
    }
}