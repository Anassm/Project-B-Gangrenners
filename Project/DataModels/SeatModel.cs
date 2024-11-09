using System.Text.Json.Serialization;


public class SeatModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("hallid")]
    public int HallId { get; set; }

    [JsonPropertyName("row")]
    public int Row { get; set; }

    [JsonPropertyName("seat")]
    public int Seat { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }


    public SeatModel(int id, int hallid, int row, int seat, int type, double price)
    {
        Id = id;
        HallId = hallid;
        Row = row;
        Seat = seat;
        Type = type;
        Price = price;
    }
}




