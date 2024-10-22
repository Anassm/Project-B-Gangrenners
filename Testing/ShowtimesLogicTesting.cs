namespace Testing;

[TestClass]
public class TestShowtimesLogic
{
    [TestMethod]
    public void GetShowTimesByMovieId_ExistingMovies_ReturnShowtimeList()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();
        List<ShowtimeModel> showtimes = ShowtimesAccess.LoadAll();  // add showtimes here but its private? discuss with team
    }
}