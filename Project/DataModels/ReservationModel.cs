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

public string ToStringWithSeats()
    {
        ShowtimeModel showtime = ShowtimesLogic.GetShowtimeById(ShowtimeId);
        string movieName = MoviesLogic.GetMovieById(showtime.MoviesId).Name;
        string hallId = Convert.ToString(showtime.HallId);
        string time = showtime.Time.ToString();
        string seats = "";
        foreach (int seatId in SeatIds)
        {
            SeatModel seat = SeatsLogic.GetSeatById(seatId);
            seats += $"Row: {seat.Row}, Seat: {seat.Seat}\n";
        }
        return $"Movie: {movieName}\n" + $"Time: {time}\n" + $"Hall ID: {hallId}\n" + $"Seats:\n{seats}\n" + $"Total Price: \u20AC {Math.Round(TotalPrice,2).ToString("0.00")}\n" + $"Codes: {string.Join(", ", Codes)}";
    }

}