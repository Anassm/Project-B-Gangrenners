public class MoviesArchiveLogic
{
    private List<MovieArchiveModel> _movies;

    public MoviesArchiveLogic()
    {
        _movies = MovieArchiveAccess.LoadAll();
    }

    public MovieArchiveModel GetMovieById(int id)
    {
        MovieArchiveModel movie = _movies.Find(movie => movie.Id == id);

        return movie;
    }

    public MovieArchiveModel GetMovieByName(string name)
    {
        MovieArchiveModel movie = _movies.Find(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return movie;
    }

    public bool CheckIfMovieInArchive(string name)
    {
        MovieArchiveModel movie = GetMovieByName(name);

        return movie != null;
    }

    public bool CheckIfMovieInArchive(int id)
    {
        MovieArchiveModel movie = GetMovieById(id);

        return movie != null;
    }

    public void AddMovie(MovieModel movie)
    {
        // Conversion from MovieModel to MovieArchiveModel
        _movies.Add(new MovieArchiveModel(movie.Id, movie.Name, movie.Genre, movie.Duration));
        MovieArchiveAccess.WriteAll(_movies);
    }
}