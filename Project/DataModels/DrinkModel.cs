using System.Text.Json.Serialization;

public class DrinkModel : IItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("subcategory")]
    public string SubCategory { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("alcoholic")]
    public bool Alcoholic { get; set; }

    [JsonPropertyName("volume")]
    public double Volume { get; set; }

    [JsonIgnore]
    public string FileName => "drinks";

    public DrinkModel(int id, string name, string subCategory, double price, bool alcoholic, double volume)
    {
        Id = id;
        Name = name;
        SubCategory = subCategory;
        Price = price;
        Alcoholic = alcoholic;
        Volume = volume;
    }

    public override string ToString()
    {

        return $"{Name} - {Volume}L - \u20AC{Price}" + (Alcoholic ? " - 18+" : "");
    }
}