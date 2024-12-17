using System.Text.Json.Serialization;

public class ProductModel : IItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("subcategory")]
    public string SubCategory { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonIgnore]
    public string FileName => "products";

    public ProductModel(int id, string name, double price, string subCategory, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        SubCategory = subCategory;
        Description = description;
    }
}