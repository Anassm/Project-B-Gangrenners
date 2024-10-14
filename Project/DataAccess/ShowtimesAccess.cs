using System.Text.Json;

static class ShowtimesAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/showtimes.json"));


    public static List<ShowtimeModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ShowtimeModel>>(json);
    }


    public static void WriteAll(List<ShowtimeModel> showtimes)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(showtimes, options);
        File.WriteAllText(path, json);
    }



}