using System.Text.Json.Serialization;


public class ShowtimeModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("moviesid")]
    public int MoviesId { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("hallid")]
    public int HallId { get; set; }

    public ShowtimeModel(int id, int moviesid, string time, int hallid)
    {
        Id = id;
        MoviesId = moviesid;
        Time = time;
        HallId = hallid;
    }

}




