
public static class ChooseMovie
{
    static private MoviesLogic _movieLogic = new MoviesLogic();
    static private ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    static private SeatsLogic _seatsLogic = new SeatsLogic();
    public static string MovieToWatch;
    public static MovieModel Movie;
    public static (List<SeatModel>, ShowtimeModel) StartMovie()
    {
        MovieModel Choice1 = MakeChoice();
        if (Choice1 == null)
        {
            StartMovie();
        }
        ShowtimeModel ShowtimeOfChoice = GetShowTimes(Choice1.Id);
        return SeatChoice(ShowtimeOfChoice.Id);
    }
    public static MovieModel MakeChoice()
    {
        System.Console.WriteLine("Please enter the name of the movie you would like to see:");
        MovieToWatch = System.Console.ReadLine();
        if (MovieToWatch is null || MovieToWatch == "")
        {
            System.Console.WriteLine("Please enter a valid movie name.");
            return MakeChoice();
        }
        if (CheckChoice(MovieToWatch))
        {
            return MoviesLogic.GetMovieByName(MovieToWatch);
        }
        return MakeChoice();
    }

    public static bool CheckChoice(string ChosenMovie)
    {
        string CorrectChoice;
        if (MoviesLogic.CheckIfMovieInMovies(ChosenMovie) != true)
        {
            System.Console.WriteLine($"There is no movie with the (partial) name {ChosenMovie}");
            System.Console.WriteLine("Please choose a different movie.");
            return false;
        }
        MovieModel Choice = MoviesLogic.GetMovieByName(ChosenMovie);
        System.Console.WriteLine($"You have chosen the following movie.");
        System.Console.WriteLine($"{Choice.ToStringUsers()}");
        System.Console.WriteLine("Is this correct?");
        System.Console.WriteLine("[Y]es / [N]o");
        CorrectChoice = System.Console.ReadLine().ToLower();
        if (CorrectChoice == "y" || CorrectChoice == "yes")
        {
            Console.Clear();
            return true;
        }
        else if (CorrectChoice == "n" || CorrectChoice == "no")
        {
            Console.Clear();
            Menu.MainMenu();
        }
        Console.Clear();
        return false;
    }

    public static ShowtimeModel GetShowTimes(int movieId)
    {
        int number = 1;
        List<ShowtimeModel> NewShowtimes = [];
        List<ShowtimeModel> showtimelist = _showtimesLogic.GetShowtimesByMovieId(movieId);
        if (showtimelist.Count() != 0)
        {
            System.Console.WriteLine("A list of all al the times:");
            System.Console.WriteLine($"----------------------------");
            foreach (ShowtimeModel showTime in showtimelist)
            {
                if (showTime.Time < DateTime.Now)
                {
                    continue;
                }
                if (showTime.MoviesId == movieId)
                {
                    System.Console.WriteLine($"number: {number}");
                    System.Console.WriteLine($"Date / Time: {showTime.Time}");
                    System.Console.WriteLine($"Hall: {showTime.HallId}");
                    System.Console.WriteLine($"----------------------------");
                    NewShowtimes.Add(showTime);
                    number++;
                }
            }
            System.Console.WriteLine("Please choose the number of the corresponding screening time.");
            int showtimeChoice = Convert.ToInt32(Console.ReadLine());
            return NewShowtimes[showtimeChoice - 1];
        }
        else
        {
            System.Console.WriteLine("There are no screening times.");
            System.Console.WriteLine("Give any input to go back to the menu.");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key != null)
            {
                Console.Clear();
                Menu.Start();
            }
            return null;
        }
    }

    public static (List<SeatModel>, ShowtimeModel) SeatChoice(int showtimeId)
    {
        System.Console.WriteLine("Please select the number of seats you would like to reserve:");
        int amountOfSeats;
        while (!int.TryParse(Console.ReadLine(), out amountOfSeats) || amountOfSeats < 1 || amountOfSeats > 450)
        {
            Console.Clear();
            Console.WriteLine("Please enter a valid number of seats.");
        }
        ShowtimeModel showtime = ShowtimesLogic.GetShowtimeById(showtimeId);

        if (!ShowtimesLogic.CheckIfEnoughAvailableSeats(showtime, amountOfSeats))
        {
            Console.WriteLine("There are not enough available seats for the amount you want to reserve.");
            Console.WriteLine("Please choose a different amount.");
            return SeatChoice(showtimeId);
        }

        List<SeatModel> selectedSeats = new List<SeatModel>();
        int selectedRow = 8, selectedCol = 0;
        bool isSelecting = true;

        Console.Clear();
        DisplaySeatMap(showtime, selectedRow, selectedCol);
        Console.WriteLine("Use arrow keys to navigate, Enter to select a seat, or Escape to exit.");

        while (isSelecting && selectedSeats.Count < amountOfSeats)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.Clear();

            if (key.Key == ConsoleKey.Enter)
            {
                SeatModel selectedSeat = ConfirmSelection(showtime, selectedRow, selectedCol);
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
            return (selectedSeats, showtime);
        }
        else
        {
            Console.WriteLine("Seat selection canceled.");
            return (null, null);
        }
    }

    
    public static void DisplaySeatMap(ShowtimeModel showtime, int selectedRow, int selectedCol)
    {
        int[,] layout = showtime.Availability;
        for (int i = 0; i < layout.GetLength(0); i++)
        {
            for (int j = 0; j < layout.GetLength(1); j++)
            {
                if(layout[i, j] == 2)
                {
                    Console.Write("   ");
                    continue;
                }
                SeatModel seat = SeatsLogic.GetSeatByCoordinates(showtime.HallId, i, j);
                if (i == selectedRow && j == selectedCol)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = seat.Type switch
                    {
                        1 => ConsoleColor.White,
                        2 => ConsoleColor.Yellow,
                        3 => ConsoleColor.DarkYellow,
                        _ => ConsoleColor.White
                    };
                }

                if (layout[i, j] == 0)
                {
                    Console.Write("[ ]");
                }
                else if (layout[i, j] == 1)
                {
                    Console.ResetColor();
                    if (i == selectedRow && j == selectedCol)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.Write("[X]");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.BackgroundColor = ConsoleColor.Black;
            }
            // create legend 
            if (i == 0)
            {
                Console.Write("  [ ] = Available seat");
            }
            else if (i == 1)
            {
                Console.Write("  [X] = Taken seat");
            }
            else if (i == 2)
            {
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("[ ]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" = Regular seat");
            }
            else if (i == 3)
            {
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write("[ ]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" = VIP seat");
            }
            else if (i == 4)
            {
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Write("[ ]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" = VIP+ seat");
            }
            else if (i == 5)
            {
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write("[ ]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" = Selected seat");
            }
            Console.WriteLine();
        }
    }

    public static (int, int) HandleArrowKeyPress(int currentRow, int currentCol, ShowtimeModel showtime, ConsoleKeyInfo key)
    {
        int newRow = currentRow;
        int newCol = currentCol;
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                newRow = Math.Max(0, currentRow - 1);
                if (showtime.Availability[newRow, currentCol] == 2)
                {
                    newRow = currentRow;
                }
                break;
            case ConsoleKey.DownArrow:
                newRow = Math.Min(showtime.Availability.GetLength(0) - 1, currentRow + 1);
                if (showtime.Availability[newRow, currentCol] == 2)
                {
                    newRow = currentRow;
                }
                break;
            case ConsoleKey.LeftArrow:
                newCol = Math.Max(0, currentCol - 1);
                if (showtime.Availability[currentRow, newCol] == 2)
                {
                    newCol = currentCol;
                }
                break;
            case ConsoleKey.RightArrow:
                newCol = Math.Min(showtime.Availability.GetLength(1) - 1, currentCol + 1);
                if (showtime.Availability[currentRow, newCol] == 2)
                {
                    newCol = currentCol;
                }
                break;
        }

        return (newRow, newCol);
    }
    public static (int, int) HandleArrowKeyPress(int currentRow, int currentCol, ShowtimeModel showtime)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        return HandleArrowKeyPress(currentRow, currentCol, showtime, key);
    }

    public static SeatModel ConfirmSelection(ShowtimeModel showtime, int selectedRow, int selectedCol)
    {
        SeatModel seat = SeatsLogic.GetSeatByCoordinates(showtime.HallId, selectedRow, selectedCol);

        if (showtime.Availability[selectedRow, selectedCol] == 1)
        {
            Console.WriteLine("This seat is already taken. Please choose another one.");
            return null;
        }
        else if (showtime.Availability[selectedRow, selectedCol] == 2)
        {
            Console.WriteLine("This seat doesnt exist");
            return null;
        }
        else
        {
            DisplaySeatMap(showtime, selectedRow, selectedCol);
            Console.WriteLine("You have selected the following seat:");
            Console.WriteLine($"Row: {seat.Row}");
            Console.WriteLine($"Seat: {seat.Seat}");
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Seat selection confirmed!");
                return seat;
            }
            else
            {
                Console.WriteLine("Seat selection canceled.");
                return null;
            }
        }
    }
}