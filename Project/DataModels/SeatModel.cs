using System.Text.Json.Serialization;


public class SeatModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("hallid")]
    public int HallId { get; set; }

    [JsonPropertyName("timeid")]
    public int TimeId { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("availability")]
    public bool Availability { get; set; }

    public SeatModel(int id, int hallid, int timeid, int type, double price, bool availability)
    {
        Id = id;
        HallId = hallid;
        TimeId = timeid;
        Type = type;
        Price = price;
        Availability = availability;
    }

}




