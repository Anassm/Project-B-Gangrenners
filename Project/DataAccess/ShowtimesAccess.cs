using Newtonsoft.Json;

static class ShowtimesAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/showtimes.json"));

    public static List<ShowtimeModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ShowtimeModel>>(json) ?? new List<ShowtimeModel>();
        }
        catch (Exception)
        {
            return new List<ShowtimeModel>();
        }
    }

    public static void WriteAll(List<ShowtimeModel> showtimes)
    {
        var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        try 
        {
            string json = JsonConvert.SerializeObject(showtimes, settings);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }

}