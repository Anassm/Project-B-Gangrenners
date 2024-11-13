using System.Text.Json.Serialization;

public class HallModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("seats")]
    public List<SeatModel> Seats { get; set; }

    [JsonPropertyName("layout")]
    public int[,] Layout { get; set; }

    public HallModel(int id, string name, List<SeatModel> seats, int[,] layout)
    {
        Id = id;
        Name = name;
        Seats = seats;
        Layout = layout;
    }
}