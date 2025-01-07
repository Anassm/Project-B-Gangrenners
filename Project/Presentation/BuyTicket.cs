static class BuyTicket
{
    static private SeatsLogic _seatsLogic = new SeatsLogic();
    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    static private ReservationsLogic _reservationsLogic = new ReservationsLogic();

    public static void Start((List<SeatModel> seats, ShowtimeModel showtime) info)
    {
        // Check if the input is valid
        if (info.seats == null || info.showtime == null)
        {
            Menu.MainMenu();
        }

        // Calculate the total price of the order
        double TotalPrice = 0;
        foreach (SeatModel seat in info.seats)
        {
            TotalPrice += seat.Price;
        }

        // declare variables
        string movieName = MoviesLogic.GetMovieById(info.showtime.MoviesId).Name;
        string seatTypes = SeatsLogic.GetSeatTypes(info.seats);
        string time = info.showtime.Time.ToString();
        string seats = "";
        foreach (SeatModel seat in info.seats)
        {
            seats += $"Row: {seat.Row}, Seat: {seat.Seat}\n";
        }

    
        Console.Clear();
        Console.WriteLine("This is what your order looks like now:");
        Console.WriteLine("Movie: " +  movieName);
        Console.WriteLine("Seat types: " + seatTypes);
        Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice,2).ToString("0.00"));
        Console.WriteLine("Time of the movie: " + time);
        Console.WriteLine("Hall: " + info.showtime.HallId);
        Console.WriteLine("Seats: " + seats);
        string StartMessage = 
            $"This is what your order looks like now:" +
            $"\nMovie: {movieName}" +
            $"\nSeat types: {seatTypes}" +
            $"\nPrice: {Math.Round(TotalPrice,2).ToString("0.00")}" +
            $"\nTime of the movie: {time}" +
            $"\nHall: {info.showtime.HallId}" +
            $"\nSeats: {seats}" +
            "Do you want to continue with the purchase?";


        bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
        if (YesNo)
        {
            Console.Clear();
            try
            {
                foreach(SeatModel seat in info.seats)
                {
                    _showtimesLogic.ReserveSeat(info.showtime.Id, seat.Row, seat.Seat);
                    int[] coordinates = SeatsLogic.GetCoordinatesBySeat(seat);
                    info.showtime.Availability[coordinates[0], coordinates[1]] = 1;
                }

                List<int> seatIds = new List<int>();
                foreach (SeatModel seat in info.seats)
                {
                    seatIds.Add(seat.Id);
                }

                List<string> codes = _reservationsLogic.GenerateUniqueCodes(info.seats.Count);

                int accountId = 0;
                if (AccountsLogic.CurrentAccount != null)
                {
                    accountId = AccountsLogic.CurrentAccount.Id;
                }

                ReservationModel reservation = new ReservationModel(_reservationsLogic.GetNextId(),seatIds, info.showtime.Id, accountId, TotalPrice, codes);
                _reservationsLogic.AddReservation(reservation);
                _showtimesLogic.UpdateList(info.showtime);
                Console.WriteLine("Your reservation has been made");
                Console.WriteLine("Movie: " +  movieName);
                Console.WriteLine("Seat types: " + seatTypes);
                Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice,2).ToString("0.00"));
                Console.WriteLine("Time of the movie: " + time);
                Console.WriteLine("Hall: " + info.showtime.HallId);
                Console.WriteLine("Seats: " + seats);
                if (codes.Count == 1)
                {
                    Console.WriteLine("Your code is: " + codes[0]);
                }
                else
                {
                    Console.WriteLine("Your codes are: " + string.Join(", ", codes));
                }
                Console.WriteLine("Thank you for your purchase");
                PresentationHelper.PrintYellow("Press any key to return to the main menu");
                ConsoleKeyInfo key = Console.ReadKey(true);
                PresentationHelper.PressAnyToContinue(Menu.MainMenu);
            }
            catch (Exception e)
            {
                PresentationHelper.PrintRed("Something went wrong, please try again");
            }
        }
        else
        {
            foreach(SeatModel seat in info.seats)
            {
                int[] coordinates = SeatsLogic.GetCoordinatesBySeat(seat);
                info.showtime.Availability[coordinates[0], coordinates[1]] = 0;
            }

            Console.Clear();
            Menu.MainMenu();
        }
    }
}