using Newtonsoft.Json;

public class FoodModel : IItem 
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("subcategory")]
    public string SubCategory { get; set; }

    [JsonProperty("price")]
    public double Price { get; set; }

    [JsonProperty("calories")]
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

    public override string ToString()
    {
        return $"{Name} - {Calories}kcal - \u20AC{Price}";
    }
}