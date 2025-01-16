using System.Reflection.Metadata;

namespace Testing;

[TestClass]
public class TestSubscriptionLogic
{

    [TestMethod]
    public void useView_InvalidAmount_ReturnMinusOne()
    {
        int userId = 6;

        Assert.AreEqual(-1, SubscriptionLogic.useView(userId));
    }

    [TestMethod]
    public void useView_ValidAmount_ReturnZero()
    {
        int userId = 1;

        Assert.AreEqual(0, SubscriptionLogic.useView(userId));
    }
}