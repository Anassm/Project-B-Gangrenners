public class MoviesLogic
{
    private List<MovieModel> _movies;

    public MoviesLogic()
    {
        _movies = MoviesAccess.LoadAll();
    }

    public MovieModel GetMovieById(int id)
    {
        MovieModel movie = _movies.Find(movie => movie.Id == id);

        return movie;
    }

    public MovieModel GetMovieByName(string name)
    {
        MovieModel movie = _movies.Find(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return movie;
    }

    public bool CheckIfMovieExists(string name)
    {
        MovieModel movie = GetMovieByName(name);

        return movie != null;
    }

    public bool CheckIfMovieExists(int id)
    {
        MovieModel movie = GetMovieById(id);

        return movie != null;
    }
}