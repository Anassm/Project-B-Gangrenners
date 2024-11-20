using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("seatids")]
    public List<int> SeatIds { get; set; }

    [JsonPropertyName("showtimeid")]
    public int ShowtimeId { get; set; }

    [JsonPropertyName("accountid")]
    public int AccountId { get; set; }

    [JsonPropertyName("totalprice")]
    public double TotalPrice { get; set; }

    [JsonPropertyName("codes")]
    public List<string> Codes { get; set; }

    public ReservationModel(int id, List<int> seatIds, int showtimeId, int accountId, double totalPrice, List<string> codes)
    {
        Id = id;
        SeatIds = seatIds;
        ShowtimeId = showtimeId;
        AccountId = accountId;
        TotalPrice = totalPrice;
        Codes = codes;
    }

    public override string ToString()
    {
        return $"ID: {Id}\n" + $"Seat IDs: {string.Join(", ", SeatIds)}\n" + $"Showtime ID: {ShowtimeId}\n" + $"Account ID: {AccountId}\n" + $"Total Price: {TotalPrice}\n" + $"Codes: {string.Join(", ", Codes)}";    
    }

}