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

        Assert.AreEqual(ml.GetMovieByName("Inception").Id, 1);
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
}