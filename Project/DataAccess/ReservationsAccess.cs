using System.Text.Json;

static class ReservationsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));

    public static List<ReservationModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<ReservationModel>>(json) ?? new List<ReservationModel>();
        }
        catch (Exception)
        {
            return new List<ReservationModel>();
        }
    }

    public static void WriteAll(List<ReservationModel> reservations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        try
        {
            string json = JsonSerializer.Serialize(reservations, options);
            File.WriteAllText(path, json);
        }
        catch (Exception)
        {
            return;
        }
    }
}