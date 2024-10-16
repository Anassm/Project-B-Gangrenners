using System.Text.Json;

static class ShowtimesAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/showtimes.json"));


    public static List<ShowtimeModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<ShowtimeModel>>(json) ?? new List<ShowtimeModel>();
        }
        catch (Exception)
        {
            return new List<ShowtimeModel>();
        }
    }


    public static void WriteAll(List<ShowtimeModel> showtimes)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        try
        {
            string json = JsonSerializer.Serialize(showtimes, options);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }



}