using System.Runtime.InteropServices.Marshalling;
using Newtonsoft.Json;

public abstract class Access<T>
{
    protected static string Path { get; set; }

    public static List<T> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
        catch (Exception)
        {
            return [];
        }
    }

    public static void WriteAll(List<T> items)
    {
        var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        try
        {
            string json = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(Path, json);
        }
        catch (Exception)
        {
            return;
        }
    }
}