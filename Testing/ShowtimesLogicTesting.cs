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
    public void GetShowTimesByMovieId_NonExistingMovies_ReturnEmptyList()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.AreEqual(showtimesLogic.GetShowtimesByMovieId(0).Count, 0);
    }

    [TestMethod]
    public void GetShowtimeById_ExistingShowtime_ReturnShowtime()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.AreEqual(showtimesLogic.GetShowtimeById(1).Id, 1);
    }

    [TestMethod]
    public void GetShowtimeById_NonExistingShowtime_ReturnNull()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.IsNull(showtimesLogic.GetShowtimeById(0));
    }

    [TestMethod]
    public void AddShowTime_ValidShowtime_AddShowtime()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();
        ShowtimeModel showtime = new ShowtimeModel(4, 1, DateTime.Now, 1, new int[10,10]);

        showtimesLogic.AddShowtime(showtime);

        Assert.IsNotNull(showtimesLogic.GetShowtimeById(4));
    }

    [TestMethod]
    public void RemoveShowtime_ExistingShowtime_RemoveShowtime()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        showtimesLogic.RemoveShowtime(1);

        Assert.AreEqual(null, showtimesLogic.GetShowtimeById(1));
    }

    [TestMethod]
    public void ReserveSeat_ExistingShowtimeAndSeat_ReserveSeat()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.IsTrue(showtimesLogic.ReserveSeat(2, 8, 8));
    }

    [TestMethod]
    public void ReserveSeat_NonExistingShowtimeAndSeat_DoNothing()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.IsFalse(showtimesLogic.ReserveSeat(0, 0, 0));
    }
}