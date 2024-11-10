using System.Text.Json;

static class AdminAccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/adminaccounts.json"));

    // Load all accounts from the json file
    public static List<AdminAccountModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<AdminAccountModel>>(json) ?? new List<AdminAccountModel>();
        }
        catch (Exception)
        {
            return new List<AdminAccountModel>();
        }
    }

    // Write all accounts to the json file
    public static void WriteAll(List<AdminAccountModel> accounts)
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