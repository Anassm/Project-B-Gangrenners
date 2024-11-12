static class BuyTicket
{
    static private SeatsLogic _seatsLogic = new SeatsLogic();
    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    static private ReservationsLogic _reservationsLogic = new ReservationsLogic();

    public static void Start((SeatModel seat, ShowtimeModel showtime) info)
    {
        Console.WriteLine("Welcome to the ticket buying page");
        _seatsLogic.GetPriceBySeat(info.seat);
        Console.WriteLine("This is what your order looks like now:");
        Console.WriteLine("Movie: " + info.showtime.MoviesId);
        Console.WriteLine("Seat type: " + info.seat.Type);
        Console.WriteLine("Price: \u20AC" + Math.Round(info.seat.Price,2).ToString("0.00"));
        Console.WriteLine("Time of the movie: " + info.showtime.Time);
        Console.WriteLine("Hall: " + info.showtime.HallId);
        Console.WriteLine("Do you want to proceed with the purchase?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
        {
            Console.Clear();
            try
            {
                _showtimesLogic.ReserveSeat(info.showtime.Id, info.seat.Row, info.seat.Seat);
                ReservationModel reservation = new ReservationModel(_reservationsLogic.GetNextId(), info.seat.Id, info.showtime.Id, 1, info.seat.Price, _reservationsLogic.GenerateCode());
                _reservationsLogic.UpdateList(reservation);
                Console.WriteLine("Your reservation has been made, your code is: " + reservation.Code);
                Console.WriteLine("Thank you for your purchase");
                Menu.MainMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong, please try again");
            }

        }
        else
        {   
            Console.Clear();
            Menu.MainMenu();
        }

    }

    public static void Start(int seatId, int showtimeId)
    {
        SeatModel seat = _seatsLogic.GetSeatById(seatId);
        ShowtimeModel showtime = _showtimesLogic.GetShowtimeById(showtimeId);
        Start((seat, showtime) );
    }
}