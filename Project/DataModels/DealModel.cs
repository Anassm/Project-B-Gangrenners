using Newtonsoft.Json;
public class DealModel : IItem
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("price")]
    public double Price { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonIgnore]
    public string FileName => "deals";

    public DealModel(int id, string name, double price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name} - {Description} - \u20AC{Price}";
    }
}