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

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    public MovieModel(int id, string name, string genre, int duration, bool promoted, string summary)
    {
        Id = id;
        Name = name;
        Genre = genre;
        Duration = duration;
        Promoted = promoted;
        Summary = summary;
    }

    public override string ToString()
    {
        return $"ID: {Id}\n" + $"Name: {Name}\n" + $"Genre: {Genre}\n" + $"Duration: {Duration}\n" + $"Promoted: {Promoted}";
    }

    public string ToStringComplete()
    {
        return $"ID: {Id}\n" + $"Name: {Name}\n" + $"Genre: {Genre}\n" + $"Duration: {Duration}\n" + $"Promoted: {Promoted}\n" + $"Summary: {Summary}";
    }
}