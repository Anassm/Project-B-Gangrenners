using System.Text.Json;

class AccountsAccess : Access<AccountModel>
{
    static AccountsAccess()
    {
        Path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
    }
}


