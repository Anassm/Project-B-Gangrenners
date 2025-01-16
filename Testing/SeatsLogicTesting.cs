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
        SeatsLogic.UpdateList(seat);
        Assert.AreEqual(SeatsLogic.GetSeatById(4), seat);
    }

    [TestMethod]
    public void GetSeatById_NonExisting_ReturnNull()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(SeatsLogic.GetSeatById(100000), null);
    }

    [TestMethod]
    public void GetSeatsByHall_ExistingHall_ReturnSeats()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat1 = new SeatModel(1, 4, 1, 1, 1, 50);
        SeatModel seat2 = new SeatModel(2, 4, 1, 1, 1, 50);
        SeatModel seat3 = new SeatModel(3, 4, 1, 1, 1, 50);
        SeatsLogic.UpdateList(seat1);
        SeatsLogic.UpdateList(seat2);
        SeatsLogic.UpdateList(seat3);
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
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        SeatsLogic.UpdateList(seat);
        Assert.AreEqual(SeatsLogic.GetSeatByRowAndSeat(1, 1, 1), seat);
    }

    [TestMethod]
    public void GetSeatByRowAndSeat_NonExistingSeat_ReturnNull()
    {
        Assert.AreEqual(SeatsLogic.GetSeatByRowAndSeat(10, 10, 10), null);
    }

    [TestMethod]
    public void GetSeatByCoordinates_ExistingSeat_ReturnSeat()
    {
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        SeatsLogic.UpdateList(seat);
        Assert.AreEqual(SeatsLogic.GetSeatByCoordinates(1, 13, 0), seat);
    }

    [TestMethod]
    public void GetSeatByCoordinates_NonExistingSeat_ReturnNull()
    {
        Assert.AreEqual(SeatsLogic.GetSeatByCoordinates(1, 100, 100), null);
    }

    [TestMethod]
    public void GetCoordinatesBySeat_ExistingSeat_ReturnCoordinates()
    {
        SeatModel seat = new SeatModel(1, 1, 1, 1, 1, 50);
        SeatsLogic.UpdateList(seat);
        CollectionAssert.AreEqual(SeatsLogic.GetCoordinatesBySeat(1, 1), new int[] { 13, 0 });
    }

    [TestMethod]
    public void GetCoordinatesBySeat_NonExistingSeat_ReturnNull()
    {
        Assert.AreEqual(SeatsLogic.GetCoordinatesBySeat(1, 1000), null);
    }

    [TestMethod]
    public void GetHallById_HallShouldExist_ShouldPass()
    {
        Assert.IsNull(HallsLogic.GetHallById(-1));
        Assert.IsNull(HallsLogic.GetHallById(0));
        Assert.IsNotNull(HallsLogic.GetHallById(1));
    }

    [TestMethod]
    public void CheckSeatsByType_SeatTypeShouldExist_ShouldPass()
    {
        Assert.IsFalse(SeatsLogic.CheckSeatsByType(1, -1));
        Assert.IsFalse(SeatsLogic.CheckSeatsByType(1, 0));
        Assert.IsTrue(SeatsLogic.CheckSeatsByType(1, 2));
        Assert.IsTrue(SeatsLogic.CheckSeatsByType(1, 3));
        Assert.IsFalse(SeatsLogic.CheckSeatsByType(1, 4));
    }
}
