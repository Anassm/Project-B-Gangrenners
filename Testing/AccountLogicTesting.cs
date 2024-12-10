using System.ComponentModel.DataAnnotations;

namespace Testing;

[TestClass]
public class TestAccountLogic
{
    [TestMethod]
    [DataRow(null, "password")]
    [DataRow("email", null)]
    [DataRow(null, null)]
    public void CheckLogin_NullValues_ReturnNull(string email, string password)
    {
        AccountsLogic ac = new AccountsLogic();
        AccountModel actual = ac.CheckLogin(email, password);
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void CheckPassword_CorrectPassword_ReturnTrue()
    {
        string password = "Abcdef1!";
        bool actual = AccountsLogic.CheckPassword(password);
        Assert.IsTrue(actual);
    }

    [TestMethod]
    [DataRow("abcdefg")]
    [DataRow("abcdefghi")]
    [DataRow("abcdefgh")]
    [DataRow("abcd1Fgh")]
    public void CheckPassword_IncorrectPassword_ReturnFalse(string password)
    {
        bool actual = AccountsLogic.CheckPassword(password);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void CheckEmail_CorrectEmail_ReturnTrue()
    {
        string email = "tester@test.nl";
        bool actual = AccountsLogic.CheckEmail(email);
        Assert.IsTrue(actual);
    }

    [TestMethod]
    [DataRow("testertestnl")]
    [DataRow("tester@testnl")]
    [DataRow("testertest.nl")]
    public void CheckEmail_IncorrectEmail_ReturnFalse(string email)
    {
        bool actual = AccountsLogic.CheckEmail(email);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void CheckEmail_ExistingEmail_ReturnFalse()
    {
        DateTime bod = new DateTime(2004, 11, 22);
        AccountsLogic.AddAccount("n@b.c", "password", "John", "Doe", bod);
        bool actual = AccountsLogic.CheckEmail("n@b.c");
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void CheckEmail_NewEmail_ReturnTrue()
    {
        bool actual = AccountsLogic.CheckEmail("iets@ietsanders.nl");
        Assert.IsTrue(actual);
    }

    [TestMethod]
    [DataRow("27-11-1904")]
    [DataRow("27-11-1903")]
    public void CheckDateOfBirth_TooOldDateOfBirth_ReturnFalse(string bodString)
    {
        bool actual = AccountsLogic.CheckDateOfBirth(bodString);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    [DataRow("28-11-1905")]
    [DataRow("27-11-1905")]
    [DataRow("27-11-2023")]
    [DataRow("27-11-2024")]
    public void CheckDateOfBirth_CorrectDateOfBirth_ReturnTrue(string bodString)
    {
        bool actual = AccountsLogic.CheckDateOfBirth(bodString);
        Assert.IsTrue(actual);
    }

    [TestMethod]
    [DataRow("28-11-2030")]
    [DataRow("27-11-2025")]
    public void CheckDateOfBirth_FutureDateOfBirth_ReturnFalse(string bodString)
    {
        bool actual = AccountsLogic.CheckDateOfBirth(bodString);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("B")]
    public void CheckName_TooShortName_ReturnFalse(string name)
    {
        bool actual = AccountsLogic.CheckName(name);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    [DataRow("Jo")]
    [DataRow("Jim")]
    public void CheckName_CorrectName_ReturnTrue(string name)
    {
        bool actual = AccountsLogic.CheckName(name);
        Assert.IsTrue(actual);
    }

    
}