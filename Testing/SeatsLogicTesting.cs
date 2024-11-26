namespace Testing;

[TestClass]
public class TestSeatsLogic
{
    [TestMethod]
    public void GetPriceBySeat_ExistingSeat_ReturnPrice()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        Assert.AreEqual(sl.GetPriceBySeat(seat), 50);
    }

    [TestMethod]
    public void GetPriceBySeat_NonExistingSeat_ReturnZero()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(sl.GetPriceBySeat(1000), 0);
    }

    [TestMethod]
    public void GetSeatById_ExistingSeat_ReturnSeat()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat = new SeatModel(4, 1, 1, 1, 1, 50);
        sl.UpdateList(seat);
        Assert.AreEqual(sl.GetSeatById(4), seat);
    }

    [TestMethod]
    public void GetSeatById_NonExisting_ReturnNull()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(sl.GetSeatById(100000), null);
    }

    [TestMethod]
    public void GetSeatsByHall_ExistingHall_ReturnSeats()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat1 = new SeatModel(1, 4, 1, 1, 1, 50);
        SeatModel seat2 = new SeatModel(2, 4, 1, 1, 1, 50);
        SeatModel seat3 = new SeatModel(3, 4, 1, 1, 1, 50);
        sl.UpdateList(seat1);
        sl.UpdateList(seat2);
        sl.UpdateList(seat3);
        List<SeatModel> seats = [seat1, seat2, seat3];
        CollectionAssert.AreEqual(sl.GetSeatsByHall(4), seats);
    }

    [TestMethod]
    public void GetSeatsByHall_NonExistingHall_ReturnEmptyList()
    {
        SeatsLogic sl = new SeatsLogic();
        List<SeatModel> seats = new List<SeatModel>();
        CollectionAssert.AreEqual(sl.GetSeatsByHall(1000), seats);
    }

    [TestMethod]
    public void GetSeatByRowAndSeat_ExistingSeat_ReturnSeat()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        sl.UpdateList(seat);
        Assert.AreEqual(SeatsLogic.GetSeatByRowAndSeat(1, 1, 1), seat);
    }

    [TestMethod]
    public void GetSeatByRowAndSeat_NonExistingSeat_ReturnNull()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(SeatsLogic.GetSeatByRowAndSeat(10, 10, 10), null);
    }

    [TestMethod]
    public void GetSeatByCoordinates_ExistingSeat_ReturnSeat()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        sl.UpdateList(seat);
        Assert.AreEqual(SeatsLogic.GetSeatByCoordinates(1, 13, 0), seat);
    }

    [TestMethod]
    public void GetSeatByCoordinates_NonExistingSeat_ReturnNull()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(SeatsLogic.GetSeatByCoordinates(1, 100, 100), null);
    }

    [TestMethod]
    public void GetCoordinatesBySeat_ExistingSeat_ReturnCoordinates()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        sl.UpdateList(seat);
        CollectionAssert.AreEqual(SeatsLogic.GetCoordinatesBySeat(1, 1), new int[] { 13, 0 });
    }

    [TestMethod]
    public void GetCoordinatesBySeat_NonExistingSeat_ReturnNull()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(SeatsLogic.GetCoordinatesBySeat(1, 1000), null);
    }

    [TestMethod]
    //HallID, Type, NewPrice
    [DataRow(1, 1, 10)]
    [DataRow(1, 2, 15)]
    [DataRow(1, 3, 20)]
    public void UpdatePrice_NewPrice_ReturnPrice(int hallid, int type, double price)
    {
        SeatsLogic.UpdatePrice(hallid, type, price);
        List<SeatModel> seats = new List<SeatModel>();
        foreach (SeatModel seat in seats)
        {
            if (seat.HallId == hallid && seat.Type == type)
            {
                Assert.AreEqual(seat.Price, price);
            }
        }
    }
}
