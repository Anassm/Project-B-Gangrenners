static class BuyTicket
{
    static private SeatsLogic _seatsLogic = new SeatsLogic();
    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    static private ReservationsLogic _reservationsLogic = new ReservationsLogic();
    static private string[] _seatTypes = {"Normal", "VIP", "VIP+"};

    public static string GetSeatTypes(List<SeatModel> seats)
    {
        int countNormal = 0;
        int countVIP = 0;
        int countVIPPlus = 0;
        foreach (SeatModel seat in seats)
        {
            if (seat.Type == 1)
            {
                countNormal++;
            }
            else if (seat.Type == 2)
            {
                countVIP++;
            }
            else if (seat.Type == 3)
            {
                countVIPPlus++;
            }
        }
        string seatTypes = "";
        if (countNormal > 0)
        {
            seatTypes += countNormal + "x normal";
        }
        if (countVIP > 0)
        {
            if (countNormal > 0)
            {
                seatTypes += ", ";
            }
            seatTypes += countVIP + "x VIP";
        }
        if (countVIPPlus > 0)
        {
            if (countNormal > 0 || countVIP > 0)
            {
                seatTypes += ", ";
            }
            seatTypes += countVIPPlus + "x VIP+";
        }

        if (seats.Count > 1)
        {
            seatTypes += " seats";
        }
        else
        {
            seatTypes += " seat";
        }
        return seatTypes;
    }

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
        string seatTypes = GetSeatTypes(info.seats);
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
        Console.WriteLine("Do you want to proceed with the purchase?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
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
                Console.WriteLine("Your reservation has been made, your codes are: " + string.Join(", ", codes));
                Console.WriteLine("Thank you for your purchase");
                Console.WriteLine("Press any key to return to the main menu");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != null)
                {
                    Console.Clear();
                    Menu.MainMenu();
                }
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
}