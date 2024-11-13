using System.Text.Json.Serialization;


public class MovieModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("promoted")]
    public bool Promoted { get; set; }

    public MovieModel(int id, string name, string genre, int duration, bool promoted)
    {
        Id = id;
        Name = name;
        Genre = genre;
        Duration = duration;
        Promoted = promoted;
    }

    public override string ToString()
    {
        return $"ID: {Id}\n" + $"Name: {Name}\n" + $"Genre: {Genre}\n" + $"Duration: {Duration}\n" + $"Promoted: {Promoted}";
    }
}