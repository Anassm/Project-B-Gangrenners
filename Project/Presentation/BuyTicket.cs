static class BuyTicket
{
    static private SeatsLogic _seatsLogic = new SeatsLogic();
    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    static private ReservationsLogic _reservationsLogic = new ReservationsLogic();

    public static void Start(SeatModel seat)
    {
        Console.WriteLine("Welcome to the ticket buying page");
        _seatsLogic.GetPriceBySeat(seat);
        Console.WriteLine("This is what your order looks like now:");
        Console.WriteLine("Movie: " + _showtimesLogic.GetShowtimeById(seat.TimeId).MoviesId);
        Console.WriteLine("Seat type: " + seat.Type);
        Console.WriteLine("Price: " + seat.Price);
        Console.WriteLine("Time of the movie: " + _showtimesLogic.GetShowtimeById(seat.TimeId).Time);
        Console.WriteLine("Hall: " + _showtimesLogic.GetShowtimeById(seat.TimeId).HallId);
        Console.WriteLine("Do you want to proceed with the purchase?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
        {
            try
            {
                ReservationModel reservation = new ReservationModel(_reservationsLogic.GetNextId(), seat.Id, seat.TimeId, 1, seat.Price, _reservationsLogic.GenerateCode());
                _reservationsLogic.UpdateList(reservation);
                Console.WriteLine("Your reservation has been made, your code is: " + reservation.Code);
                Console.WriteLine("Thank you for your purchase");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong, please try again");
            }

        }
        else
        {
            Console.WriteLine("Thank you for visiting our page");
        }

    }

    public static void Start(int seatId)
    {
        SeatModel seat = _seatsLogic.GetSeatById(seatId);
        Start(seat);
    }
}