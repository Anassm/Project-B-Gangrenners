using System.Text.Json;

static class MovieArchiveAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/moviearchive.json"));


    public static List<MovieModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<MovieModel>>(json) ?? new List<MovieModel>();
        }
        catch (Exception)
        {
            return new List<MovieModel>();
        }
    }


    public static void WriteAll(List<MovieModel> movies)
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