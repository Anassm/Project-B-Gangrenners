public class MoviesLogic
{
    private static List<MovieModel> _movies;

    public MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll();
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
            throw new Exception("Movie already added");
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
        if (CheckIfMovieInMovies(name))
        {
            throw new Exception("Movie already added");
        }
        if (MoviesArchiveLogic.CheckIfMovieInArchive(name))
        {
            MoviesArchiveLogic.RemoveMovie(GetMovieByName(name));
        }
        _movies.Add(new MovieModel(_movies.Count()+MoviesArchiveLogic.GetCount()+1, name, genre, duration, false));
        MoviesAccess.WriteAll(_movies);
    }

    public static void RemoveMovie(MovieModel movie)
    {
        if (movie == null)
        {
            throw new Exception("invalid movie.");
        }
        MoviesArchiveLogic.AddMovie(movie);
        _movies.Remove(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public static void RemoveMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        RemoveMovie(movie);
    }

    public static void PromoteMovie(MovieModel movie)
    {
        if (movie == null)
        {
            throw new Exception("Movie does not exist");
        }
        movie.Promoted = true;
        MoviesAccess.WriteAll(_movies);
    }

    public static void PromoteMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        PromoteMovie(movie);
    }

    public static int GetNextId()
    {
        return _movies.Count() + MoviesArchiveLogic.GetCount() + 1;
    }
}