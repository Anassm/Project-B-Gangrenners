using Newtonsoft.Json;

public class OrderModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("reservationId")]
    public int ReservationId { get; set; }

    [JsonProperty("pickupCode")]
    public string PickupCode { get; set; }

    [JsonProperty("items")]
    public List<(int itemId, string fileName, int quantity)> ItemReferences { get; set; }

    [JsonIgnore]
    public List<(IItem item, int quantity)> Items { get; set; }

    [JsonProperty("totalPrice")]
    public double TotalPrice { get; set; }

    public OrderModel(int id, int reservationId, string pickupCode)
    {
        Id = id;
        ReservationId = reservationId;
        PickupCode = pickupCode;
    }

    public override string ToString()
    {
        return $"Pickup code: {PickupCode}\n" + $"Total price: {TotalPrice}";
    }
}