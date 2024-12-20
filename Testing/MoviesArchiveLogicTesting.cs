namespace Testing;

[TestClass]
public class TestMoviesArchiveLogic
{
    [TestMethod]
    public void GetMovieById_ExistingMovie_ReturnMovie()
    {
        MovieModel movie = new(5, "The Matrix", "Sci-Fi", 136, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        MovieModel result = MoviesArchiveLogic.GetMovieById(5);

        Assert.AreEqual(movie, result);
    }

    [TestMethod]
    public void GetMovieByName_ExistingMovie_ReturnMovie()
    {
        MovieModel movie = new(5, "The Matrix", "Sci-Fi", 136, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        MovieModel result = MoviesArchiveLogic.GetMovieByName("The Matrix");

        Assert.AreEqual(movie.Id, result.Id);
    }

    [TestMethod]
    public void CheckIfMovieInArchiveName_ExistingMovie_ReturnTrue()
    {
        MovieModel movie = new(1, "The Matrix", "Sci-Fi", 136, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        bool result = MoviesArchiveLogic.CheckIfMovieInArchive("The Matrix");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CheckIfMovieInArchiveId_ExistingMovie_ReturnTrue()
    {
        MovieModel movie = new(7, "The Matrix", "Sci-Fi", 136, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        bool result = MoviesArchiveLogic.CheckIfMovieInArchive(7);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AddMovie_NewMovie_AddMovie()
    {
        MovieModel movie = new(4, "The Dark Knight", "Action", 152, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        bool result = MoviesArchiveLogic.CheckIfMovieInArchive("The Dark Knight");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void RemoveMovie_ExistingMovie_RemoveMovie()
    {
        MovieModel movie = new(6, "The Matrixx", "Sci-Fi", 136, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        MoviesArchiveLogic.RemoveMovie(movie);

        bool result = MoviesArchiveLogic.CheckIfMovieInArchive("The Matrixx");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RemoveMovie_ExistingMovieName_RemoveMovie()
    {
        MovieModel movie = new(1, "The Matrixxx", "Sci-Fi", 136, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie);

        MoviesArchiveLogic mal = new();
        mal.RemoveMovie("The Matrixxx");

        bool result = MoviesArchiveLogic.CheckIfMovieInArchive("The Matrixxx");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetCount_ExistingMovies_ReturnCount()
    {
        MovieModel movie1 = new(1, "The Matrix", "Sci-Fi", 136, false, "No summary available", 0, 0);
        MovieModel movie2 = new(2, "Inception", "Sci-Fi", 148, false, "No summary available", 0, 0);

        MoviesArchiveLogic.AddMovie(movie1);
        MoviesArchiveLogic.AddMovie(movie2);

        int result = MoviesArchiveLogic.GetCount();

        Assert.AreEqual(8, result);
    }
}