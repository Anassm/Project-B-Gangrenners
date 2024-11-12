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
        MoviesLogic ml = new MoviesLogic();

        MovieModel movie = new MovieModel(4, "The Dark Knight", "Action", 152, false);

        ml.AddMovie(movie);

        Assert.IsTrue(ml.CheckIfMovieInMovies("The Dark Knight"));
    }

    public void RemoveMovie_ExistingMovie_RemoveMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        MovieModel movie = MoviesLogic.GetMovieByName("Inception");

        ml.RemoveMovie(movie);

        Assert.IsFalse(ml.CheckIfMovieInMovies("Inception"));
    }

    public void RemoveMovie_ExistingMovieName_RemoveMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        ml.RemoveMovie("Inception");

        Assert.IsFalse(ml.CheckIfMovieInMovies("Inception"));
    }

    public void PromoteMovie_IsPromoted_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        MovieModel movie = MoviesLogic.GetMovieByName("Inception");

        ml.PromoteMovie(movie);

        Assert.IsTrue(movie.Promoted);
    }

    public void PromoteMovieName_IsPromoted_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        ml.PromoteMovie("Inception");

        Assert.IsTrue(MoviesLogic.GetMovieByName("Inception").Promoted);
    }


}