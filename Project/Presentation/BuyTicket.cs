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
        Console.WriteLine("Welcome to the ticket buying page");
        Console.WriteLine("This is what your order looks like now:");
        Console.WriteLine("Movie: " +  movieName);
        Console.WriteLine("Seat types: " + seatTypes);
        Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice,2).ToString("0.00"));
        Console.WriteLine("Time of the movie: " + time);
        Console.WriteLine("Hall: " + info.showtime.HallId);
        Console.WriteLine("Seats: " + seats);
        string StartMessage = "Do you want to proceed with the purchase?";
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
                Console.WriteLine("Do you want to add extras to your order?");
                BuyExtras.ProductMenu(true, reservation);
                
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
                if (OrdersLogic.GetOrderByReservationId(reservation.Id) != null)
                {
                    System.Console.WriteLine("You have added extras to your reservation");
                    System.Console.WriteLine("This is your order:");
                    System.Console.WriteLine(OrdersLogic.GetProductString(OrdersLogic.GetOrderByReservationId(reservation.Id)));


                }
                Console.WriteLine("Thank you for your purchase");
                Console.WriteLine("Press any key to return to the main menu");
                ConsoleKeyInfo key = Console.ReadKey(true);
                PresentationHelper.PressAnyToContinue(Menu.MainMenu);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong, please try again");
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

    public static void ReservationOverview(ReservationModel reservation)
    {
        System.Console.WriteLine("This is your reservation:");
        List<SeatModel> seats = new List<SeatModel>();
        foreach (int seatId in reservation.SeatIds)
        {
            seats.Add(SeatsLogic.GetSeatById(seatId));
        }
        string seatTypes = SeatsLogic.GetSeatTypes(seats);
        string time = ShowtimesLogic.GetShowtimeById(reservation.ShowtimeId).Time.ToString();
        double TotalPrice = reservation.TotalPrice;
        string hall = ShowtimesLogic.GetShowtimeById(reservation.ShowtimeId).HallId.ToString();
        List<string> codes = reservation.Codes;

        string seatsstring = "";
        foreach (SeatModel seat in seats)
        {
            seatsstring += $"Row: {seat.Row}, Seat: {seat.Seat}";
        }
        Console.Clear();
        Console.WriteLine("This is your reservation:");
        Console.WriteLine("Movie: " + MoviesLogic.GetMovieById(ShowtimesLogic.GetShowtimeById(reservation.ShowtimeId).MoviesId).Name);
        Console.WriteLine("Seat types: " + seatTypes);
        Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice,2).ToString("0.00"));
        Console.WriteLine("Time of the movie: " + time);
        Console.WriteLine("Hall: " + hall);
        Console.WriteLine("Seats: " + seatsstring);
        if (codes.Count == 1)
        {
            Console.WriteLine("Your code is: " + codes[0]);
        }
        else
        {
            Console.WriteLine("Your codes are: " + string.Join(", ", codes));
        }
        if (OrdersLogic.GetOrderByReservationId(reservation.Id) != 0)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("You have added extras to your reservation");
            System.Console.WriteLine("This is your order:");
            System.Console.WriteLine(OrdersLogic.GetProductString(OrdersLogic.GetOrderByReservationId(reservation.Id)));
            System.Console.WriteLine("Pickup code: " + OrdersLogic.GetOrderById(OrdersLogic.GetOrderByReservationId(reservation.Id)).PickupCode);
            System.Console.WriteLine("Price of the items: \u20AC" + OrdersLogic.GetTotalPrice(OrdersLogic.GetOrderByReservationId(reservation.Id)).ToString("0.00"));
            System.Console.WriteLine("Total price of the reservation: \u20AC" + (TotalPrice + OrdersLogic.GetTotalPrice(OrdersLogic.GetOrderByReservationId(reservation.Id))).ToString("0.00"));
        }
        System.Console.WriteLine("");
        Console.WriteLine("Thank you for your purchase");
        Console.WriteLine("Press any key to return to the main menu");
        ConsoleKeyInfo key = Console.ReadKey(true);
        PresentationHelper.PressAnyToContinue(Menu.MainMenu);
    }
}