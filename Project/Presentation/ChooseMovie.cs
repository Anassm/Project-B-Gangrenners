public static class ChooseMovie
{
    private static readonly ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    public static string MovieToWatch;
    public static MovieModel Movie;


    public static (List<SeatModel>, ShowtimeModel) StartMovie()
    {
        var choice = MakeChoice();
        if (choice == null)
        {
            return StartMovie();
        }

        var showtime = GetShowTimes(choice.Id);
        return SeatChoice(showtime.Id);
    }

    public static (List<SeatModel>, ShowtimeModel) StartMovie(MovieModel movie, int amountOfSeats, DateTime day)
    {
        var choice = movie;
        var showtime = GetShowTimes(choice, day, amountOfSeats);
        return SeatChoice(showtime, amountOfSeats);
    }

    public static (List<SeatModel>, ShowtimeModel) StartMovie(MovieModel movie, int amountOfSeats, DateTime beginDay, DateTime endDay)
    {
        var choice = movie;
        var showtime = GetShowTimes(choice, beginDay, endDay, amountOfSeats);
        return SeatChoice(showtime, amountOfSeats);
    }




    public static MovieModel MakeChoice()
    {
        PresentationHelper.PrintYellow("Please enter the name of the movie you would like to see:");
        MovieToWatch = Console.ReadLine();

        if (string.IsNullOrEmpty(MovieToWatch))
        {
            PresentationHelper.PrintRed("Please enter a valid movie name.");
            return MakeChoice();
        }

        if (CheckChoice(MovieToWatch))
        {
            return MoviesLogic.GetMovieByName(MovieToWatch);
        }

        return MakeChoice();
    }


    public static bool CheckChoice(string chosenMovie)
    {
        if (!MoviesLogic.CheckIfMovieInMovies(chosenMovie))
        {
            string StartMessage = $"There is no movie with the (partial) name {chosenMovie}\nWould you like to choose a different movie?.";
            bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
            if (YesNo)
            {
                MakeChoice();
            }
            else
            {
                Menu.MainMenu();
            }
            
        }

        var choice = MoviesLogic.GetMovieByName(chosenMovie);
        string StartMessage2 = $"You have chosen the following movie:\n{choice.ToStringUsers()}";
        bool YesNo2 = SelectingMenu.YesNoSelect(StartMessage2);
        if (YesNo2)
        {
            PresentationHelper.ClearConsole();
            return true;
        }
        else
        {
            Console.Clear();
            Menu.MainMenu();
        }
        PresentationHelper.ClearConsole();
        return false;
    }


    public static ShowtimeModel GetShowTimes(int movieId)
    {
        var showtimes = ShowtimesLogic.GetShowtimesByMovieId(movieId);
        if (!showtimes.Any())
        {
            PresentationHelper.PrintRed("There are no screening times.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to go back to the menu.");
            PresentationHelper.PressAnyToContinue(Menu.Start);
            return null;
        }

        var newShowtimes = showtimes.Where(st => st.Time >= DateTime.Now && st.MoviesId == movieId).ToList();
        newShowtimes = newShowtimes.OrderBy(st => st.Time).ToList();
        if (!newShowtimes.Any())
        {
            PresentationHelper.PrintRed("There are no upcoming screening times.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to go back to the menu.");
            PresentationHelper.PressAnyToContinue(Menu.Start);
            return null;
        }

        Console.WriteLine("A list of all the times:");
        Console.WriteLine("----------------------------");

        for (int i = 0; i < newShowtimes.Count; i++)
        {
            var showtime = newShowtimes[i];
            Console.WriteLine($"Number: {i + 1}");
            Console.WriteLine($"Date / Time: {showtime.Time}");
            Console.WriteLine($"Hall: {showtime.HallId}");
            Console.WriteLine("----------------------------");
        }

        int showtimeChoice = 1;
        while (true)
        {
            PresentationHelper.PrintYellow("Please choose the number of the corresponding screening time.");
            string choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice) || !int.TryParse(choice, out _) || choice.All(char.IsLetter))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            showtimeChoice = Convert.ToInt32(choice);
            if (showtimeChoice < 1 || showtimeChoice > newShowtimes.Count)
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            break;
        }
        return newShowtimes[showtimeChoice - 1];
    }

    public static ShowtimeModel GetShowTimes(MovieModel movie, DateTime day, int amountOfTickets)
    {
        var showtimes = ShowtimesLogic.GetShowtimesByMovieId(movie.Id);
        if (!showtimes.Any())
        {
            PresentationHelper.PrintRed("There are no screening times.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to go back to the menu.");
            PresentationHelper.PressAnyToContinue(Menu.Start);
            return null;
        }

        var newShowtimes = showtimes.Where(st => st.Time >= DateTime.Now && st.MoviesId == movie.Id && st.Time.Date == day.Date).ToList();
        newShowtimes = newShowtimes.OrderBy(st => st.Time).ToList();
        if (!newShowtimes.Any())
        {
            PresentationHelper.PrintRed("There are no upcoming screening times.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to go back to the menu.");
            PresentationHelper.PressAnyToContinue(Menu.Start);
            return null;
        }

        Console.WriteLine("A list of all the times:");
        Console.WriteLine("----------------------------");

        for (int i = 0; i < newShowtimes.Count; i++)
        {
            var showtime = newShowtimes[i];
            if(ShowtimesLogic.CheckIfEnoughAvailableSeats(showtime, amountOfTickets))
            {
                Console.WriteLine($"Number: {i + 1}");
                Console.WriteLine($"Date / Time: {showtime.Time}");
                Console.WriteLine($"Hall: {showtime.HallId}");
                Console.WriteLine("----------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Number: {i + 1}");
                Console.WriteLine($"Date / Time: {showtime.Time}");
                Console.WriteLine($"Hall: {showtime.HallId}");
                Console.WriteLine("----------------------------");
                Console.ResetColor();
            }
           
        }

        int showtimeChoice = 1;
        while (true)
        {
            PresentationHelper.PrintYellow("Please choose the number of the corresponding screening time.");
            string choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice) || !int.TryParse(choice, out _) || choice.All(char.IsLetter))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            showtimeChoice = Convert.ToInt32(choice);
            if (showtimeChoice < 1 || showtimeChoice > newShowtimes.Count)
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            break;
        }
        if (!ShowtimesLogic.CheckIfEnoughAvailableSeats(newShowtimes[showtimeChoice - 1], amountOfTickets))
        {
            Console.WriteLine("There are not enough available seats for the amount you want to reserve.");
            Console.WriteLine("Please choose a different amount.");
            return GetShowTimes(movie, day, amountOfTickets);
        }
        return newShowtimes[showtimeChoice - 1];
    }

    public static ShowtimeModel GetShowTimes(MovieModel movie, DateTime beginDay, DateTime endDay, int amountOfTickets)
    {
        var showtimes = ShowtimesLogic.GetShowtimesByMovieId(movie.Id);
        if (!showtimes.Any())
        {
            PresentationHelper.PrintRed("There are no screening times.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to go back to the menu.");
            PresentationHelper.PressAnyToContinue(Menu.Start);
            return null;
        }

        var newShowtimes = showtimes.Where(st => st.Time >= DateTime.Now && st.MoviesId == movie.Id && st.Time.Date <= endDay.Date).ToList();
        newShowtimes = newShowtimes.OrderBy(st => st.Time).ToList();
        if (!newShowtimes.Any())
        {
            PresentationHelper.PrintRed("There are no upcoming screening times.");
            System.Console.WriteLine();
            PresentationHelper.PrintYellow("Press any key to go back to the menu.");
            PresentationHelper.PressAnyToContinue(Menu.Start);
            return null;
        }

        Console.WriteLine("A list of all the times:");
        Console.WriteLine("----------------------------");

        for (int i = 0; i < newShowtimes.Count; i++)
        {
            var showtime = newShowtimes[i];
            if(ShowtimesLogic.CheckIfEnoughAvailableSeats(showtime, amountOfTickets))
            {
                Console.WriteLine($"Number: {i + 1}");
                Console.WriteLine($"Date / Time: {showtime.Time}");
                Console.WriteLine($"Hall: {showtime.HallId}");
                Console.WriteLine("----------------------------");
            }
            else
            {
                Console.WriteLine($"\x1B[2mNumber: {i + 1}");
                Console.WriteLine($"Date / Time: {showtime.Time}");
                Console.WriteLine($"Hall: {showtime.HallId}\x1B[0m");
                Console.WriteLine("----------------------------");
            }
           
        }

        int showtimeChoice = 1;
        while (true)
        {
            PresentationHelper.PrintYellow("Please choose the number of the corresponding screening time.");
            string choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice) || !int.TryParse(choice, out _) || choice.All(char.IsLetter))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            showtimeChoice = Convert.ToInt32(choice);
            if (showtimeChoice < 1 || showtimeChoice > newShowtimes.Count)
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            break;
        }
        
        if (!ShowtimesLogic.CheckIfEnoughAvailableSeats(newShowtimes[showtimeChoice - 1], amountOfTickets))
        {
            PresentationHelper.PrintRed("There are not enough available seats for the amount you want to reserve.");
            PresentationHelper.PrintYellow("Please choose a different amount.");
            PresentationHelper.PrintYellow("Enter the number of tickets you would like to buy:");
            amountOfTickets = Convert.ToInt32(Console.ReadLine());
            return GetShowTimes(movie, beginDay, endDay, amountOfTickets);
        }
        return newShowtimes[showtimeChoice - 1];
    }



    public static (List<SeatModel>, ShowtimeModel) SeatChoice(int showtimeId)
    {
        PresentationHelper.PrintYellow("Please select the number of seats you would like to reserve:");
        int amountOfSeats;
        while (!int.TryParse(Console.ReadLine(), out amountOfSeats) || amountOfSeats < 1 || amountOfSeats > 450)
        {
            Console.Clear();
            Console.WriteLine("Please enter a valid number of seats.");
        }

        var showtime = ShowtimesLogic.GetShowtimeById(showtimeId);
        if (!ShowtimesLogic.CheckIfEnoughAvailableSeats(showtime, amountOfSeats))
        {
            Console.WriteLine("There are not enough available seats for the amount you want to reserve.");
            Console.WriteLine("Please choose a different amount.");
            return SeatChoice(showtimeId);
        }

        return SeatChoice(showtime, amountOfSeats);
    }

    public static (List<SeatModel>, ShowtimeModel) SeatChoice(ShowtimeModel showtime, int amountOfSeats)
    {
        if (!ShowtimesLogic.CheckIfEnoughAvailableSeats(showtime, amountOfSeats))
        {
            Console.WriteLine("There are not enough available seats for the amount you want to reserve.");
            Console.WriteLine("Please choose a different amount.");
            return SeatChoice(showtime.Id);
        }

        var selectedSeats = new List<SeatModel>();
        int selectedRow = 8, selectedCol = 0;
        bool isSelecting = true;

        Console.Clear();
        Console.WriteLine("\x1b[3J");
        DisplaySeatMap(showtime, selectedRow, selectedCol);
        Console.WriteLine("Use arrow keys to navigate, Enter to select a seat, or Escape to exit.");

        while (isSelecting && selectedSeats.Count < amountOfSeats)
        {
            var key = Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            if (key.Key == ConsoleKey.Enter)
            {
                var selectedSeat = ConfirmSelection(showtime, selectedRow, selectedCol);
                if (selectedSeat != null && !selectedSeats.Contains(selectedSeat))
                {
                    selectedSeats.Add(selectedSeat);
                    showtime.Availability[selectedRow, selectedCol] = 1;
                    Console.WriteLine($"Seat {selectedSeats.Count} selected.");
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                isSelecting = false;
                return (null, null);
            }
            else
            {
                (selectedRow, selectedCol) = HandleArrowKeyPress(selectedRow, selectedCol, showtime, key);
            }

            DisplaySeatMap(showtime, selectedRow, selectedCol);
            Console.WriteLine($"Seats selected: {selectedSeats.Count}/{amountOfSeats}");
        }

        if (selectedSeats.Count == amountOfSeats)
        {
            Console.WriteLine("All seats selected successfully!");
            Console.WriteLine("You selected the following seats:");
            foreach (var seat in selectedSeats)
            {
                Console.WriteLine($"Row: {seat.Row}, Seat: {seat.Seat}");
            }
            string StartMessage = "is this correct?";
            bool YesNo = SelectingMenu.YesNoSelect(StartMessage);
            if (YesNo)
            {
                return (selectedSeats, showtime);
            }
            Console.WriteLine("Seat selection canceled.");
            foreach (var seat in selectedSeats)
            {
                var coordinates = SeatsLogic.GetCoordinatesBySeat(seat);
                showtime.Availability[coordinates[0], coordinates[1]] = 0;
            }

            return SeatChoice(showtime, amountOfSeats);
        }

        Console.WriteLine("Seat selection canceled.");
        return SeatChoice(showtime, amountOfSeats);
    }


    public static void DisplaySeatMap(ShowtimeModel showtime, int selectedRow, int selectedCol)
    {
        var layout = showtime.Availability;
        for (int i = 0; i < layout.GetLength(0); i++)
        {
            Console.Write(i == 1 || i == layout.GetLength(0) - 2 ? "<-" : "  ");
            for (int j = 0; j < layout.GetLength(1); j++)
            {
                if (layout[i, j] == 2)
                {
                    Console.Write("   ");
                    continue;
                }

                var seat = SeatsLogic.GetSeatByCoordinates(showtime.HallId, i, j);
                Console.BackgroundColor = i == selectedRow && j == selectedCol ? ConsoleColor.Green : seat.Type switch
                {
                    1 => ConsoleColor.White,
                    2 => ConsoleColor.Yellow,
                    3 => ConsoleColor.DarkYellow,
                    _ => ConsoleColor.White
                };
                if (layout[i, j] == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                if(layout[i, j] == 1 && i == selectedRow && j == selectedCol)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                Console.Write(layout[i, j] == 0 ? "[ ]" : "[X]");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            var legends = new[]
            {
                "  [ ] = Available seat",
                "  [X] = Taken seat",
                " = Regular seat",
                " = VIP seat",
                " = VIP+ seat",
                " = Selected seat"
            };

            var colors = new[]
            {
                ConsoleColor.Black,
                ConsoleColor.Red,
                ConsoleColor.White,
                ConsoleColor.Yellow,
                ConsoleColor.DarkYellow,
                ConsoleColor.Green
            };

            if (i < legends.Length)
            {
                Console.Write("  ");
                if (i > 1)
                {
                    Console.Write("  ");
                    Console.BackgroundColor = colors[i];
                    Console.Write("[ ]");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(legends[i]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        var screenLength = layout.GetLength(1) * 3;
        var screenText = "SCREEN";
        var padding = (screenLength - screenText.Length) / 2;

        Console.WriteLine(new string('-', screenLength));
        Console.WriteLine(new string(' ', padding) + screenText + new string(' ', padding));
        Console.WriteLine(new string('-', layout.GetLength(1) * 3));
    }

    public static (int, int) HandleArrowKeyPress(int currentRow, int currentCol, ShowtimeModel showtime, ConsoleKeyInfo key)
    {
        int newRow = currentRow, newCol = currentCol;
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                newRow = Math.Max(0, currentRow - 1);
                if (showtime.Availability[newRow, currentCol] == 2) newRow = currentRow;
                break;
            case ConsoleKey.DownArrow:
                newRow = Math.Min(showtime.Availability.GetLength(0) - 1, currentRow + 1);
                if (showtime.Availability[newRow, currentCol] == 2) newRow = currentRow;
                break;
            case ConsoleKey.LeftArrow:
                newCol = Math.Max(0, currentCol - 1);
                if (showtime.Availability[currentRow, newCol] == 2) newCol = currentCol;
                break;
            case ConsoleKey.RightArrow:
                newCol = Math.Min(showtime.Availability.GetLength(1) - 1, currentCol + 1);
                if (showtime.Availability[currentRow, newCol] == 2) newCol = currentCol;
                break;
        }

        return (newRow, newCol);
    }

    public static SeatModel ConfirmSelection(ShowtimeModel showtime, int selectedRow, int selectedCol)
    {
        var seat = SeatsLogic.GetSeatByCoordinates(showtime.HallId, selectedRow, selectedCol);

        if (showtime.Availability[selectedRow, selectedCol] == 1)
        {
            Console.WriteLine("This seat is already taken. Please choose another one.");
            return null;
        }

        if (showtime.Availability[selectedRow, selectedCol] == 2)
        {
            Console.WriteLine("This seat doesn't exist.");
            return null;
        }

        DisplaySeatMap(showtime, selectedRow, selectedCol);
        Console.WriteLine($"You have selected the following seat:\nRow: {seat.Row}\nSeat: {seat.Seat}");
        Console.WriteLine("Please confirm your selection by pressing Enter.");

        if (Console.ReadKey(true).Key == ConsoleKey.Enter)
        {
            Console.WriteLine("Seat selection confirmed!");
            return seat;
        }

        Console.WriteLine("Seat selection canceled.");
        return null;
    }
}
