using System.Text.Json.Serialization;


public class ShowtimeModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("moviesid")]
    public int MoviesId { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("hallid")]
    public int HallId { get; set; }

    [JsonPropertyName("availability")]
    public int[,] Availability { get; set; }

    public ShowtimeModel(int id, int moviesid, DateTime time, int hallid, int[,] availability)
    {
        Id = id;
        MoviesId = moviesid;
        Time = time;
        HallId = hallid;
        Availability = availability;
    }
}




