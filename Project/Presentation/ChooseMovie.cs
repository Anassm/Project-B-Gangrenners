using System.Reflection.Metadata.Ecma335;

public class ChooseMovie
{
    static private MoviesLogic _movieLogic = new MoviesLogic();
    public string MovieToWatch;
    public MovieModel Movie;
    public SeatModel StartMovie()
    {
        MovieModel Choice1 = MakeChoice();
        if (Choice1 == null)
        {
            StartMovie();
        }
        ShowtimeModel ShowtimeOfChoice = GetShowTimes(Choice1.Id);
        return SeatChoice(ShowtimeOfChoice.Id);
    }
    public MovieModel MakeChoice()
    {
        System.Console.WriteLine("Please enter the name of the movie you would like to see:");
        MovieToWatch = System.Console.ReadLine();
        if (CheckChoice(MovieToWatch))
        {
            return _movieLogic.GetMovieByName(MovieToWatch);
        }
        return null;
    }

    public bool CheckChoice(string ChosenMovie)
    {
        string CorrectChoice;
        if (_movieLogic.CheckIfMovieExists(ChosenMovie) != true)
        {
            System.Console.WriteLine($"There is no movie with the (partial) name {ChosenMovie}");
            System.Console.WriteLine("Please choose a different movie.");
            MakeChoice();
        }
        MovieModel Choice = _movieLogic.GetMovieByName(ChosenMovie);
        System.Console.WriteLine($"You have chosen the movie {Choice.Name}.");
        System.Console.WriteLine("Is this correct?");
        System.Console.WriteLine("[Y]es / [N]o");
        CorrectChoice = System.Console.ReadLine().ToLower();
        if (CorrectChoice == "y" || CorrectChoice == "yes")
        {
            return true;
        }
        return false;
    }

    public ShowtimeModel GetShowTimes(int movieId)
    {
        int number = 1;
        List<ShowtimeModel> NewShowtimes = [];
        List<ShowtimeModel> showtimelist = ShowtimesLogic.GetShowtimesByMovieId(MakeChoice().Id);
        System.Console.WriteLine("A list of all al the times:");
        System.Console.WriteLine($"----------------------------");
        foreach(ShowtimeModel showTime in showtimelist)
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
        return NewShowtimes[showtimeChoice-1];
    }

    public SeatModel SeatChoice(int showtimeId)
    {
        int number = 1;
        List<SeatModel> Seats = SeatsAccess.LoadAll();
        List<SeatModel> NewSeats = [];
        System.Console.WriteLine("Here are the seats for this Time:");
        System.Console.WriteLine($"----------------------------");
        foreach (SeatModel seat in Seats)
        {
            if (seat.TimeId == showtimeId)
            {
                System.Console.WriteLine($"Number: {number}");
                System.Console.WriteLine($"Hall: {seat.HallId}");
                System.Console.WriteLine($"Type: {seat.Type}");
                System.Console.WriteLine($"Price: {seat.Price}");
                System.Console.WriteLine($"Availability: {seat.Availability}");
                System.Console.WriteLine($"----------------------------");
                NewSeats.Add(seat);
                number++;
            }
        }
        System.Console.WriteLine("Please choose the number of the corresponding seat.");
        int seatChoice = Convert.ToInt32(Console.ReadLine());
        return NewSeats[seatChoice-1];
    }
}