static class BuyTicket
{
    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    static private ReservationsLogic _reservationsLogic = new ReservationsLogic();

    public static void Start((List<SeatModel> seats, ShowtimeModel showtime) info)
    {
        // Check if the input is valid
        if (info.seats == null || info.showtime == null)
        {
            Menu.MainMenu();
        }

        // declare variables
        double TotalPrice = SeatsLogic.CalculateTotalPrice(info.seats);
        string movieName = MoviesLogic.GetMovieById(info.showtime.MoviesId).Name;
        string seatTypes = SeatsLogic.GetSeatTypes(info.seats);
        string time = info.showtime.Time.ToString();
        string seats = "";
        foreach (SeatModel seat in info.seats)
        {
            seats += $"Row: {seat.Row}, Seat: {seat.Seat}\n";
        }


        PresentationHelper.ClearConsole();
        Console.WriteLine("This is what your order looks like now:");
        Console.WriteLine("Movie: " + movieName);
        Console.WriteLine("Seat types: " + seatTypes);
        Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice, 2).ToString("0.00"));
        Console.WriteLine("Time of the movie: " + time);
        Console.WriteLine("Hall: " + info.showtime.HallId);
        Console.WriteLine("Seats: " + seats);

        bool YesNo = false;
        string StartMessage =
                $"This is what your order looks like now:" +
                $"\nMovie: {movieName}" +
                $"\nSeat types: {seatTypes}" +
                $"\nPrice: {Math.Round(TotalPrice, 2).ToString("0.00")}" +
                $"\nTime of the movie: {time}" +
                $"\nHall: {info.showtime.HallId}" +
                $"\nSeats: {seats}";

        StartMessage += $"\nWould you like to purchase for this reservation, or redeem subscription code{(info.seats.Count > 1 ? "s" : "")}";

        string[] MenuNames = { "Pay for reservation", $"Redeem subscription code{(info.seats.Count > 1 ? "s" : "")}" };
        Action[] Actions = { () => throw new Exception(), () =>
                {
                    int convertedAmount = 0;
                    while (true)
                    {
                        Console.WriteLine("How many codes do you want to redeem?");
                        string amount = Console.ReadLine().ToLower();
                        convertedAmount = 0;
                        if (amount.All(char.IsDigit))
                        {
                            convertedAmount = Convert.ToInt32(amount);
                            if (convertedAmount <= 0)
                            {
                                PresentationHelper.ClearConsole();
                                PresentationHelper.PrintRed("Input has to be larger than 0.");
                            }
                            break;
                        }
                        else
                        {
                            PresentationHelper.ClearConsole();
                            PresentationHelper.PrintRed("Invalid input, Enter a number.");
                        }
                    }

                    List<int> usedCodes = [];
                    List<SeatModel> sortedSeats = info.seats.OrderBy(seat => seat.Type).ToList();

                    for (int i = 0; i < convertedAmount; i++)
                    {
                        while (true)
                        {
                            string code = "";
                            int convertedCode = 0;
                            while (true)
                            {
                                Console.WriteLine("Enter your code: ");
                                code = Console.ReadLine();
                                convertedCode = 0;
                                if (code.All(char.IsDigit))
                                {
                                    convertedCode = Convert.ToInt32(code);
                                    if (convertedCode <= 0)
                                    {
                                        PresentationHelper.ClearConsole();
                                        PresentationHelper.PrintRed("Input has to be larger than 0.");
                                    }
                                    break;
                                }
                                else
                                {
                                    PresentationHelper.ClearConsole();
                                    PresentationHelper.PrintRed("Invalid input, Enter a number.");
                                }
                            }
                            if (code == "")
                            {
                                PresentationHelper.ClearConsole();
                                PresentationHelper.PrintRed("Can not input nothing, please give an input.");
                            }
                            else
                            {
                                if (SubscriptionLogic.CheckCode(convertedCode) == true && !usedCodes.Contains(convertedCode))
                                {
                                        SubscriptionLogic.UseViewByCode(convertedCode);
                                        Console.WriteLine("Code is valid");
                                        usedCodes.Add(convertedCode);
                                        TotalPrice -= sortedSeats[0].Price;
                                        sortedSeats.RemoveAt(0);

                                        break;
                                }
                                else
                                {
                                    PresentationHelper.PrintRed("Code is invalid, or already used");
                                    continue;
                                }
                            }
                        }
                    }

                    throw new Exception();
                }
        };



        try
        {
            SelectingMenu.MenusSelect(MenuNames, Actions, StartMessage);
        }
        catch (Exception e)
        {
            YesNo = true;
        }

        if (YesNo)
        {
            PresentationHelper.ClearConsole();
            try
            {
                
                SeatsLogic.UpdateAvailability(info.showtime, info.seats);
                List<int> seatIds = SeatsLogic.MakeSeatList(info.seats);
                List<string> codes = _reservationsLogic.GenerateUniqueCodes(info.seats.Count);

                int accountId = AccountsLogic.GetCurrentId();

                ReservationModel reservation = new ReservationModel(_reservationsLogic.GetNextId(), seatIds, info.showtime.Id, accountId, TotalPrice, codes);
                _reservationsLogic.AddReservation(reservation);
                _showtimesLogic.UpdateList(info.showtime);
                Console.WriteLine("Do you want to add extras to your order?");
                BuyExtras.ProductMenu(true, reservation);

                Console.WriteLine("Your reservation has been made");
                Console.WriteLine("Movie: " + movieName);
                Console.WriteLine("Seat types: " + seatTypes);
                Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice, 2).ToString("0.00"));
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
                try
                {
                    System.Console.WriteLine("You have added extras to your reservation");
                    System.Console.WriteLine("This is your order:");
                    System.Console.WriteLine(OrdersLogic.GetProductString(OrdersLogic.GetOrderByReservationId(reservation.Id)));


                }
                catch
                {
                    System.Console.WriteLine("No extras added");
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
            SeatsLogic.ResetAvailability(info.showtime, info.seats);
            PresentationHelper.ClearConsole();
            Menu.MainMenu();
        }
    }

    public static void ReservationOverview(ReservationModel reservation)
    {
        List<SeatModel> seats = ReservationsLogic.GetSeatsByReservation(reservation);
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
        PresentationHelper.ClearConsole();
        Console.WriteLine("This is your reservation:");
        Console.WriteLine("Movie: " + MoviesLogic.GetMovieById(ShowtimesLogic.GetShowtimeById(reservation.ShowtimeId).MoviesId).Name);
        Console.WriteLine("Seat types: " + seatTypes);
        Console.WriteLine("Price: \u20AC" + Math.Round(TotalPrice, 2).ToString("0.00"));
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
        try
        {
            OrdersLogic.GetOrderByReservationId(reservation.Id);
            string test = OrdersLogic.GetOrderById(OrdersLogic.GetOrderByReservationId(reservation.Id)).PickupCode;
            System.Console.WriteLine("");
            System.Console.WriteLine("You have added extras to your reservation");
            System.Console.WriteLine("This is your order:");
            System.Console.WriteLine(OrdersLogic.GetProductString(OrdersLogic.GetOrderByReservationId(reservation.Id)));
            System.Console.WriteLine("Pickup code: " + OrdersLogic.GetOrderById(OrdersLogic.GetOrderByReservationId(reservation.Id)).PickupCode);
            System.Console.WriteLine("Price of the items: \u20AC" + OrdersLogic.GetTotalPrice(OrdersLogic.GetOrderByReservationId(reservation.Id)).ToString("0.00"));
            System.Console.WriteLine("Total price of the reservation: \u20AC" + (TotalPrice + OrdersLogic.GetTotalPrice(OrdersLogic.GetOrderByReservationId(reservation.Id))).ToString("0.00"));
        }
        catch
        {
            System.Console.WriteLine("No extras added");
        }
        System.Console.WriteLine("");
        Console.WriteLine("Thank you for your purchase");
        Console.WriteLine("Press any key to return to the main menu");
        ConsoleKeyInfo key = Console.ReadKey(true);
        PresentationHelper.PressAnyToContinue(Menu.MainMenu);
    }
}