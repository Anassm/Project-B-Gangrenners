using Newtonsoft.Json;

static class HallsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/halls.json"));

    public static List<HallModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<HallModel>>(json) ?? new List<HallModel>();
        }
        catch (Exception)
        {
            return new List<HallModel>();
        }
    }

    public static void WriteAll(List<HallModel> halls)
    {
        var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        try 
        {
            string json = JsonConvert.SerializeObject(halls, settings);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }

}