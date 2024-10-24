using System.Text.Json;

static class SeatsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/seats.json"));


    public static List<SeatModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<SeatModel>>(json) ?? new List<SeatModel>();
        }
        catch (Exception)
        {
            return new List<SeatModel>();
        }
    }


    public static void WriteAll(List<SeatModel> seats)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        try
        {
            string json = JsonSerializer.Serialize(seats, options);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }



}