namespace Testing;

[TestClass]
public class AdminAccountLogicTesting
{
    [TestMethod]
    [DataRow(null, "password")]
    [DataRow("email", null)]
    [DataRow(null, null)]
    public void CheckLogin_NullValues_ReturnNull(string email, string password)
    {
        AdminAccountsLogic ac = new AdminAccountsLogic();
        AdminAccountModel actual = ac.CheckLogin(email, password);
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void CheckLogin_ValidLogin_ReturnAccount()
    {
        AdminAccountsLogic ac = new AdminAccountsLogic();
        AdminAccountModel account = new AdminAccountModel(1000, "admin@abc.nl", "admin", "Piet Jansen");
        ac.UpdateList(account);
        AdminAccountModel actual = ac.CheckLogin("admin@abc.nl", "admin");
        Assert.AreEqual(account, actual);
    }

    [TestMethod]
    public void CheckLogin_InvalidLogin_ReturnNull()
    {
        AdminAccountsLogic ac = new AdminAccountsLogic();
        AdminAccountModel account = new AdminAccountModel(10001, "test@test.nl", "test", "test");
        ac.UpdateList(account);
        AdminAccountModel actual = ac.CheckLogin("dafdsf", "dsafdsf");
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void GetById_ValidId_ReturnAccount()
    {
        AdminAccountsLogic ac = new AdminAccountsLogic();
        AdminAccountModel account = new AdminAccountModel(10002, "tsd", "test", "test");
        ac.UpdateList(account);
        AdminAccountModel actual = ac.GetById(10002);
        Assert.AreEqual(account, actual);
    }

    [TestMethod]
    public void GetById_InvalidId_ReturnNull()
    {
        AdminAccountsLogic ac = new AdminAccountsLogic();
        AdminAccountModel actual = ac.GetById(10003);
        Assert.IsNull(actual);
    }

}