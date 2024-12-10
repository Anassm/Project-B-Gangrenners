using System.Data.Common;

namespace Testing;

[TestClass]
public class TestMoviesLogic
{
    [TestMethod]
    public void GetMovieById_ExistingMovie_ReturnMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.AreEqual(MoviesLogic.GetMovieById(1).Name, "Inception");
    }

    [TestMethod]
    public void GetMovieByName_ExistingMovie_ReturnMovie()
    {   
        Assert.AreEqual(MoviesLogic.GetMovieByName("Inception").Id, 1);
    }

    public void GetMovieByName_ExistingMovie_ReturnNotNull()
    {   
        Assert.IsTrue(MoviesLogic.GetMovieByName("Inception") != null);
    }

    public void GetMovieByName_NonExistingMovie_ReturnNull()
    {   
        Assert.AreEqual(MoviesLogic.GetMovieByName("Alladin"), null);
    }

    [TestMethod]
    public void CheckIfMovieInMoviesName_ExistingMovie_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.IsTrue(MoviesLogic.CheckIfMovieInMovies("Inception"));
    }

    [TestMethod]
    public void CheckIfMovieInMoviesId_ExistingMovie_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        Assert.IsTrue(MoviesLogic.CheckIfMovieInMovies(1));
    }

    [TestMethod]
    public void AddMovie_NewMovie_AddMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        MovieModel movie = new MovieModel(4, "The Dark Knight", "Action", 152, false, "No summary available", 0, 0);

        MoviesLogic.AddMovie(movie);

        Assert.IsTrue(MoviesLogic.CheckIfMovieInMovies("The Dark Knight"));
    }

    public void RemoveMovie_ExistingMovie_RemoveMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        MovieModel movie = MoviesLogic.GetMovieByName("Inception");

        MoviesLogic.RemoveMovie(movie);

        Assert.IsFalse(MoviesLogic.CheckIfMovieInMovies("Inception"));
    }

    public void RemoveMovie_ExistingMovieName_RemoveMovie()
    {
        MoviesLogic ml = new MoviesLogic();

        MoviesLogic.RemoveMovie("Inception");

        Assert.IsFalse(MoviesLogic.CheckIfMovieInMovies("Inception"));
    }

    public void PromoteMovie_IsPromoted_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        MovieModel movie = MoviesLogic.GetMovieByName("Inception");

        MoviesLogic.PromoteMovie(movie);

        Assert.IsTrue(movie.Promoted);
    }

    public void PromoteMovieName_IsPromoted_ReturnTrue()
    {
        MoviesLogic ml = new MoviesLogic();

        MoviesLogic.PromoteMovie("Inception");

        Assert.IsTrue(MoviesLogic.GetMovieByName("Inception").Promoted);
    }


}