using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("seatid")]
    public int SeatId { get; set; }

    [JsonPropertyName("showtimeid")]
    public int ShowtimeId { get; set; }

    [JsonPropertyName("accountid")]
    public int AccountId { get; set; }

    [JsonPropertyName("totalprice")]
    public double TotalPrice { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    public ReservationModel(int id, int seatId, int showtimeId, int accountId, double totalPrice, string code)
    {
        Id = id;
        SeatId = seatId;
        ShowtimeId = showtimeId;
        AccountId = accountId;
        TotalPrice = totalPrice;
        Code = code;
    }

    public override string ToString()
    {
        return $"ID: {Id}\n" + $"Seat ID: {SeatId}\n" + $"Showtime ID: {ShowtimeId}\n" + $"Account ID: {AccountId}\n" + $"Total Price: {TotalPrice}\n" + $"Code: {Code}";
    }
}