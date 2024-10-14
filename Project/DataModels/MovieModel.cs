using System.Text.Json.Serialization;


public class MovieModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }


    public MovieModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

}




