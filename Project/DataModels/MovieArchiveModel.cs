using System.Text.Json.Serialization;


public class MovieArchiveModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public MovieArchiveModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

}




