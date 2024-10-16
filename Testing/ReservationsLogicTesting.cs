namespace Testing;

[TestClass]
public class TestReservationsLogic
{
    [TestMethod]
    public void GenerateCode_ExistingCode_ReturnNewCode()
    {
        ReservationsLogic rl = new ReservationsLogic();
        ReservationModel res = new ReservationModel(2, 1, 1, 1, 1, rl.GenerateCode());
        rl.UpdateList(res);
        Assert.AreNotEqual(res.Code, "jol8M8");
    }
}