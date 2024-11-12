namespace Testing;

[TestClass]
public class TestMoviesArchiveLogic
{
    [TestMethod]
    public void GetMovieById_ExistingMovie_ReturnMovie()
    {
        MovieModel movie = new MovieModel(1, "The Matrix", "Sci-Fi", 136, false);

        MoviesArchiveLogic.AddMovie(movie);

        MovieModel result = MoviesArchiveLogic.GetMovieById(1);

        Assert.AreEqual(movie, result);
    }

    [TestMethod]
    public void GetMovieByName_ExistingMovie_ReturnMovie()
    {
        MovieModel movie = new MovieModel(1, "The Matrix", "Sci-Fi", 136, false);

        MoviesArchiveLogic.AddMovie(movie);

        MovieModel result = MoviesArchiveLogic.GetMovieByName("The Matrix");

        Assert.AreEqual(movie, result);
    }

    
}