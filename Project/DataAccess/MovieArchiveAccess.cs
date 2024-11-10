using System.Text.Json;

static class MovieArchiveAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/moviearchive.json"));


    public static List<MovieArchiveModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<MovieArchiveModel>>(json) ?? new List<MovieArchiveModel>();
        }
        catch (Exception)
        {
            return new List<MovieArchiveModel>();
        }
    }


    public static void WriteAll(List<MovieArchiveModel> movies)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        try 
        {
            
            string json = JsonSerializer.Serialize(movies, options);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }
}