public class MoviesLogic
{
    private static List<MovieModel> _movies;

    public MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll();
    }

    public MovieModel GetMovieById(int id)
    {
        MovieModel movie = _movies.Find(movie => movie.Id == id);

        return movie;
    }

    public static MovieModel GetMovieByName(string name)
    {
        MovieModel movie = _movies.Find(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return movie;
    }

    public bool CheckIfMovieInMovies(string name)
    {
        MovieModel movie = GetMovieByName(name);

        return movie != null;
    }

    public bool CheckIfMovieInMovies(int id)
    {
        MovieModel movie = GetMovieById(id);

        return movie != null;
    }

    public void AddMovie(MovieModel movie)
    {
        if (CheckIfMovieInMovies(movie.Name))
        {
            throw new Exception("Movie already exists");
        }
        if (MoviesArchiveLogic.CheckIfMovieInArchive(movie.Name))
        {
            MoviesArchiveLogic.RemoveMovie(movie);
        }

        _movies.Add(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public void RemoveMovie(MovieModel movie)
    {
        if (movie == null)
        {
            throw new Exception("Movie does not exist");
        }

        MoviesArchiveLogic.AddMovie(movie);

        _movies.Remove(movie);
        MoviesAccess.WriteAll(_movies);
    }

    public void RemoveMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        RemoveMovie(movie);
    }

    public void PromoteMovie(MovieModel movie)
    {
        if (movie == null)
        {
            throw new Exception("Movie does not exist");
        }

        movie.Promoted = true;
        MoviesAccess.WriteAll(_movies);
    }

    public void PromoteMovie(string name)
    {
        MovieModel movie = GetMovieByName(name);
        PromoteMovie(movie);
    }

    public int GetNextId()
    {
        return _movies.Count() + MoviesArchiveLogic.GetCount() + 1;
    }
}