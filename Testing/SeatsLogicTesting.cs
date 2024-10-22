namespace Testing;

[TestClass]
public class TestSeatsLogic
{
    [TestMethod]
    public void GetPriceBySeat_ExistingSeat_ReturnPrice()
    {
        SeatsLogic sl = new SeatsLogic();
        SeatModel seat = new SeatModel(1, 1, 1, 1, 50, 1);
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
        SeatModel seat = new SeatModel(4, 1, 1, 1, 50, 1);
        sl.UpdateList(seat);
        Assert.AreEqual(sl.GetSeatById(4), seat);
    }

    [TestMethod]
    public void GetSeatById_NonExisting_ReturnNull()
    {
        SeatsLogic sl = new SeatsLogic();
        Assert.AreEqual(sl.GetSeatById(1000), null);
    }
}
