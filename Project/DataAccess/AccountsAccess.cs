using System.Text.Json;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));


    // Load all accounts from the json file
    public static List<AccountModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<AccountModel>>(json) ?? new List<AccountModel>();
        }
        catch (Exception)
        {
            return new List<AccountModel>();
        }
        
    }


    // Write all accounts to the json file
    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        try
        {
            string json = JsonSerializer.Serialize(accounts, options);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }



}