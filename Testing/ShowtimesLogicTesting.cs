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

        Assert.AreEqual(ShowtimesLogic.GetShowtimeById(1).Id, 1);
    }

    [TestMethod]
    public void GetShowtimeById_NonExistingShowtime_ReturnNull()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        Assert.IsNull(ShowtimesLogic.GetShowtimeById(0));
    }

    [TestMethod]
    public void AddShowTime_ValidShowtime_AddShowtime()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();
        ShowtimeModel showtime = new ShowtimeModel(4, 1, DateTime.Now, 1, new int[10, 10]);

        showtimesLogic.AddShowtime(showtime);

        Assert.IsNotNull(ShowtimesLogic.GetShowtimeById(4));
    }

    [TestMethod]
    public void RemoveShowtime_ExistingShowtime_RemoveShowtime()
    {
        ShowtimesLogic showtimesLogic = new ShowtimesLogic();

        showtimesLogic.RemoveShowtime(1);

        Assert.AreEqual(null, ShowtimesLogic.GetShowtimeById(1));
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

    // The amount of tickets should be checked against the availability 
    [TestMethod]
    [DataRow(3, 151)]
    [DataRow(3, 152)]
    public void CheckIfEnoughAvailableSeats_InsufficientSeats_ReturnFalse(int showTimeId, int tickets)
    {
        Assert.IsFalse(ShowtimesLogic.CheckIfEnoughAvailableSeats(showTimeId, tickets));
    }

    [TestMethod]
    [DataRow(3, 1)]
    [DataRow(3, 2)]
    [DataRow(3, 149)]
    [DataRow(3, 150)]
    public void CheckIfEnoughAvailableSeats_SufficientSeats_ReturnTrue(int showTimeId, int tickets)
    {
        Assert.IsTrue(ShowtimesLogic.CheckIfEnoughAvailableSeats(showTimeId, tickets));
    }

    [TestMethod]
    [DataRow(3, -1)]
    [DataRow(3, 0)]
    public void CheckIfEnoughAvailableSeats_InvalidTickets_ReturnFalse(int showTimeId, int tickets)
    {
        Assert.IsFalse(ShowtimesLogic.CheckIfEnoughAvailableSeats(showTimeId, tickets));
    }
}