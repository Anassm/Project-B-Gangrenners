using System.Text.Json.Serialization;

public class OrderModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservationId")]
    public int ReservationId { get; set; }

    [JsonPropertyName("pickupCode")]
    public string PickupCode { get; set; }

    [JsonPropertyName("items")]
    public List<(int itemId, string fileName, int quantity)> ItemReferences { get; set; }

    [JsonIgnore]
    public List<(IItem item, int quantity)> Items { get; set; }

    [JsonPropertyName("totalPrice")]
    public double TotalPrice { get; set; }

    public OrderModel(int id, int reservationId, string pickupCode)
    {
        Id = id;
        ReservationId = reservationId;
        PickupCode = pickupCode;
    }
}