public class AccountantAccountsLogic
{
    private List<AccountantAccountModel> _accountantAccounts;
    static public AccountantAccountModel? CurrentAccountantAccount { get; private set; }

    public AccountantAccountsLogic()
    {
        _accountantAccounts = AdminAccountsAccess.LoadAll();
    }

    public void UpdateList(AccountantAccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accountantAccounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accountantAccounts[index] = acc;
        }
        else
        {
            //add new model
            _accountantAccounts.Add(acc);
        }
        AccountantAccountsAccess.WriteAll(_accountantAccounts);

    }

    // Get an account by id
    public AccountantAccountModel GetById(int id)
    {
        return _accountantAccounts.Find(i => i.Id == id);
    }

    // Check if the login is valid
    public AccountantAccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccountantAccount = _accountantAccounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccountantAccount;
    }
}
