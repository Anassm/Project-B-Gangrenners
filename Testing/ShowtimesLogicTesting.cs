namespace Testing;

[TestClass]
public class TestShowtimesLogic
{
    [TestMethod]
    public void GetShowTimesByMovieId_ExistingMovies_ReturnShowtimeList()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.IsInstanceOfType(showtimesLogic.GetShowtimesByMovieId(1), typeof(List<ShowtimeModel>));
    }

    [TestMethod]
    public void GetShowtimeByIIId_ExistingShowtime_ReturnShowtime()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.AreEqual(showtimesLogic.GetShowtimeById(1).Id, 1);
    }
}