using System.Text.Json.Serialization;
using Newtonsoft.Json;


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

    [JsonPropertyName("cost")]
    public int Cost { get; set; }

    [JsonPropertyName("revenue")]
    public int Revenue { get; set; }

    public MovieModel(int id, string name, string genre, int duration, bool promoted, string summary, int cost, int revenue)
    {
        Id = id;
        Name = name;
        Genre = genre;
        Duration = duration;
        Promoted = promoted;
        Summary = summary;
        Cost = cost;
        Revenue = revenue;
    }

    public override string ToString()
    {
        return $"ID: {Id}\n" + $"Name: {Name}\n" + $"Genre: {Genre}\n" + $"Duration: {Duration}\n" + $"Promoted: {Promoted}";
    }

    public string ToStringComplete()
    {
        return $"ID: {Id}\n" + $"Name: {Name}\n" + $"Genre: {Genre}\n" + $"Duration: {Duration}\n" + $"Promoted: {Promoted}\n" + $"Summary: {Summary}";
    }

    public string ToStringUsers()
    {
        int minutes = Duration % 60;
        int hours = (Duration - minutes) / 60;
        string time = $"{hours}h {minutes}m";

        return $"Name: {Name}\n" + $"Genre: {Genre}\n" + $"Duration: {time} \n" + $"Summary: {Summary}";
    }

    public string ToStringOneLine()
    {
        return $"{Name} - ({Genre}) - {Duration} minutes";
    }
}