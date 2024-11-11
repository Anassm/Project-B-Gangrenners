public class AdminAccountsLogic
{
    private List<AdminAccountModel> _adminAccounts;
    static public AdminAccountModel? CurrentAdminAccount { get; private set; }

    public AdminAccountsLogic()
    {
        _adminAccounts = AdminAccountsAccess.LoadAll();
    }

        public void UpdateList(AdminAccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _adminAccounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _adminAccounts[index] = acc;
        }
        else
        {
            //add new model
            _adminAccounts.Add(acc);
        }
        AdminAccountsAccess.WriteAll(_adminAccounts);

    }

    // Get an account by id
    public AdminAccountModel GetById(int id)
    {
        return _adminAccounts.Find(i => i.Id == id);
    }

    // Check if the login is valid
    public AdminAccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAdminAccount = _adminAccounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAdminAccount;
    }
}
