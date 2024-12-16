public class AccountantAccountsAccess : DataAccessLayer<AccountantAccountModel>
{
    private static readonly string fileName = "accountantaccounts";

    public static List<AccountantAccountModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<AccountantAccountModel> accounts)
    {
        WriteAll(fileName, accounts);
    }
}