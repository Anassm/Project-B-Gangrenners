using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public abstract class DataAccessLayer<T> where T : class
{
    protected static string GetFilePath(string fileName)
    {
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, $"DataSources/{fileName}.json"));
    }

    public static List<T> LoadAll(string fileName)
    {
        try
        {
            string json = File.ReadAllText(GetFilePath(fileName));
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
        catch (Exception)
        {
            return new List<T>();
        }
    
    }

    public static void WriteAll(string fileName, List<T> items)
    {
        var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        try
        {
            string json = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(GetFilePath(fileName), json);
        }
        catch (Exception)
        {
            return;
        }
    }
}