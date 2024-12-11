public class AdminAccountsAccess : DataAccessLayer<AdminAccountModel>
{
    private static readonly string fileName = "adminaccounts";

    public static List<AdminAccountModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<AdminAccountModel> accounts)
    {
        WriteAll(fileName, accounts);
    }
}