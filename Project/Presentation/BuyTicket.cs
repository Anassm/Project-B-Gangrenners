static class BuyTicket
{
    static private SeatsLogic seatsLogic = new SeatsLogic();
    static private ShowtimesLogic showtimesLogic = new ShowtimesLogic();

    public static void Start(SeatModel seat)
    {
        Console.WriteLine("Welcome to the ticket buying page");
        seatsLogic.GetPriceBySeat(seat);
        Console.WriteLine("This is what your order looks like now:");
        Console.WriteLine("Movie: " + showtimesLogic.GetShowtimeById(seat.TimeId).MoviesId);
        Console.WriteLine("Seat type: " + seat.Type);
        Console.WriteLine("Price: " + seat.Price);
        Console.WriteLine("Time of the movie: " + showtimesLogic.GetShowtimeById(seat.TimeId).Time);
        Console.WriteLine("Hall: " + showtimesLogic.GetShowtimeById(seat.TimeId).HallId);
        Console.WriteLine("Do you want to proceed with the purchase?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
        {
            ReservationModel reservation = new ReservationModel(0, seat.Id, DateTime.Now);
        }
        else
        {
            Console.WriteLine("Thank you for visiting our page");
        }

    }

    public static void Start(int seatId)
    {
        SeatModel seat = seatsLogic.GetSeatById(seatId);
        Start(seat);
    }
}