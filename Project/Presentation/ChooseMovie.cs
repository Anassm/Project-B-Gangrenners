
public static class ChooseMovie
{
    private static readonly MoviesLogic _movieLogic = new MoviesLogic();
    private static readonly ShowtimesLogic _showtimesLogic = new ShowtimesLogic();
    private static readonly SeatsLogic _seatsLogic = new SeatsLogic();
    
    public static string? MovieToWatch { get; private set; }
    public static MovieModel? SelectedMovie { get; private set; }

    public static (SeatModel, ShowtimeModel) StartMovie()
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
        if (CheckChoice(MovieToWatch))
        {
            return MoviesLogic.GetMovieByName(MovieToWatch);
        }
        return MakeChoice();
    }

    public static bool CheckChoice(string ChosenMovie)
    {
        string CorrectChoice;
        if (_movieLogic.CheckIfMovieInMovies(ChosenMovie) != true)
        {
            System.Console.WriteLine($"There is no movie with the (partial) name {ChosenMovie}");
            System.Console.WriteLine("Please choose a different movie.");
            return false;
        }
        MovieModel Choice = MoviesLogic.GetMovieByName(ChosenMovie);
        System.Console.WriteLine($"You have chosen the movie {Choice.Name}.");
        System.Console.WriteLine("Is this correct?");
        System.Console.WriteLine("[Y]es / [N]o");
        CorrectChoice = System.Console.ReadLine().ToLower();
        if (CorrectChoice == "y" || CorrectChoice == "yes")
        {
            Console.Clear();
            return true;
        }
        Console.Clear();
        return false;
    }

    public static ShowtimeModel GetShowTimes(int movieId)
    {
        int number = 1;
        List<ShowtimeModel> NewShowtimes = [];
        List<ShowtimeModel> showtimelist = _showtimesLogic.GetShowtimesByMovieId(movieId);
        System.Console.WriteLine("A list of all al the times:");
        System.Console.WriteLine($"----------------------------");
        foreach (ShowtimeModel showTime in showtimelist)
        {
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
        System.Console.WriteLine("Please choose the number of the corresponding showtime.");
        int showtimeChoice = Convert.ToInt32(Console.ReadLine());
        return NewShowtimes[showtimeChoice - 1];
    }

    public static (SeatModel, ShowtimeModel) SeatChoice(int showtimeId)
    {
        int number = 1;
        Console.Clear();
        List<SeatModel> Seats = SeatsAccess.LoadAll();
        List<SeatModel> NewSeats = [];
        System.Console.WriteLine("Here are the seats for this Time:");
        System.Console.WriteLine($"----------------------------");
        int selectedRow = 8, selectedCol = 0;
        bool isSelecting = true;

        ShowtimeModel showtime = _showtimesLogic.GetShowtimeById(showtimeId);
        DisplaySeatMap(showtime, selectedRow, selectedCol);
        Console.WriteLine("Use arrow keys to navigate, Enter to select a seat, or Escape to exit.");
        (selectedRow, selectedCol) = HandleArrowKeyPress(selectedRow, selectedCol, showtime);
        Console.Clear();
        while (isSelecting)
        {
            
            DisplaySeatMap(showtime, selectedRow, selectedCol);
            Console.WriteLine("Press Enter to confirm your selection.");
            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.Clear();
            if (key.Key == ConsoleKey.Enter)
            {
                SeatModel selectedSeat = ConfirmSelection(showtime, selectedRow, selectedCol);
                if (selectedSeat != null)
                {
                    return (selectedSeat, showtime);
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                isSelecting = false;
            }
            else
            {
                (selectedRow, selectedCol) = HandleArrowKeyPress(selectedRow, selectedCol, showtime, key);
            }
            
        }
        
        return (null, null);
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
                // SeatModel seat = SeatsLogic.GetSeatByCoordinates(showtime.HallId, i, j);
                // if (i == selectedRow && j == selectedCol)
                // {
                //     Console.BackgroundColor = ConsoleColor.Green;
                // }
                // else
                // {
                //     Console.BackgroundColor = seat.Type switch
                //     {
                //         1 => ConsoleColor.White,
                //         2 => ConsoleColor.Yellow,
                //         _ => ConsoleColor.White
                //     };
                // }

                Console.BackgroundColor = (i == selectedRow && j == selectedCol) ? ConsoleColor.Green : GetSeatColor(showtime, i, j);
                Console.Write(layout[i, j] == 1 ? "[X]" : "[ ]");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
    }

    private static ConsoleColor GetSeatColor(ShowtimeModel showtime, int row, int col)
    {
        SeatModel seat = SeatsLogic.GetSeatByCoordinates(showtime.HallId, row, col);

        if (showtime.Availability[row, col] == 1)  // Reserved seat
        {
            return ConsoleColor.Red;
        }

        // Colors based on seat type
        return seat.Type switch
        {
            1 => ConsoleColor.Blue,
            2 => ConsoleColor.Yellow,
            3 => ConsoleColor.DarkYellow,
            _ => ConsoleColor.Gray
        };
    }

    public static (int, int) HandleArrowKeyPress(int currentRow, int currentCol, ShowtimeModel showtime, ConsoleKeyInfo key)
    {
        int newRow = currentRow;
        int newCol = currentCol;
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                newRow = Math.Max(0, currentRow - 1);
                break;
            case ConsoleKey.DownArrow:
                newRow = Math.Min(showtime.Availability.GetLength(0) - 1, currentRow + 1);
                break;
            case ConsoleKey.LeftArrow:
                newCol = Math.Max(0, currentCol - 1);
                break;
            case ConsoleKey.RightArrow:
                newCol = Math.Min(showtime.Availability.GetLength(1) - 1, currentCol + 1);
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
        else
        {
            DisplaySeatMap(showtime, selectedRow, selectedCol);
            Console.WriteLine("You have selected the following seat:");
            Console.WriteLine($"Row: {seat.Row}");
            Console.WriteLine($"Seat: {seat.Seat}");
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                _showtimesLogic.ReserveSeat(showtime.Id, seat.Row, seat.Seat);
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