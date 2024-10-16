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
        SeatModel seat = new SeatModel(1, 1, 1, 1, 50, 1);
        Assert.AreEqual(sl.GetPriceBySeat(2), 0);
    }
}
