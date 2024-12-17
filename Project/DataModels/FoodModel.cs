using System.Text.Json.Serialization;

public class FoodModel : IItem 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("subcategory")]
    public string SubCategory { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("calories")]
    public int Calories { get; set; }

    [JsonIgnore]
    public string FileName => "food";

    public FoodModel(int id, string name, string subCategory, double price, int calories)
    {
        Id = id;
        Name = name;
        SubCategory = subCategory;
        Price = price;
        Calories = calories;
    }
}