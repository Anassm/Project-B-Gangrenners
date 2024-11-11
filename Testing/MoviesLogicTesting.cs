namespace Testing;

[TestClass]
public class TestMoviesLogic
{
    [TestMethod]
    public void GetMovieById_ExistingMovie_ReturnMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.AreEqual(ml.GetMovieById(1).Name, "Inception");
    }

    [TestMethod]
    public void GetMovieByName_ExistingMovie_ReturnMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.AreEqual(MoviesLogic.GetMovieByName("Inception").Id, 1);
    }

    [TestMethod]
    public void CheckIfMovieInMoviesName_ExistingMovie_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.IsTrue(ml.CheckIfMovieInMovies("Inception"));
    }

    [TestMethod]
    public void CheckIfMovieInMoviesId_ExistingMovie_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.IsTrue(ml.CheckIfMovieInMovies(1));
    }

    [TestMethod]
    public void AddMovie_NewMovie_AddMovie()
    {
        MoviesLogic ml = new();

        MovieModel movie = new(6, "Test Movie", "Test Genre", 120, false);

        ml.AddMovie(movie);

        Assert.IsNotNull(ml.GetMovieById(6));
    }

    [TestMethod]
    public void RemoveMovie_ExistingMovie_RemoveMovieById()
    {
        MoviesLogic ml = new();

        MovieModel movie = ml.GetMovieById(1);

        ml.RemoveMovie(movie);

        Assert.IsFalse(ml.CheckIfMovieInMovies(1));
    }

    [TestMethod]
    public void RemoveMovie_ExistingMovie_RemoveMovieByName()
    {
        MoviesLogic ml = new();

        ml.RemoveMovie("Inception");

        Assert.IsFalse(ml.CheckIfMovieInMovies("Inception"));
    }

    [TestMethod]
    public void PromoteMovie_PromoteByMovie_ReturnTrue()
    {
        MoviesLogic ml = new();

        MovieModel movie = ml.GetMovieById(1);

        ml.PromoteMovie(movie);

        Assert.IsTrue(movie.Promoted);
    }

    [TestMethod]
    public void PromoteMovie_PromoteByName_ReturnTrue()
    {
        MoviesLogic ml = new();

        MovieModel movie = MoviesLogic.GetMovieByName("Inception");

        Assert.IsTrue(movie.Promoted);
    }
}