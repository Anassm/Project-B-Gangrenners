public class AccountsAccess : DataAccessLayer<AccountModel>
{
    private static readonly string fileName = "accounts";

    public static List<AccountModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<AccountModel> accounts)
    {
        WriteAll(fileName, accounts);
    }
}