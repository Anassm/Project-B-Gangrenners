using System.Text.Json;

class AdminAccountsAccess : Access<AdminAccountModel>
{
    public static string Path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/adminaccounts.json"));
}




 