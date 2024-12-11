using System.Diagnostics;

public class ShowtimesLogic
{
    static private List<ShowtimeModel> _showtimes { get; set; } = ShowtimesAccess.LoadAll();

    public ShowtimesLogic()
    {
    }

    public static List<ShowtimeModel> GetShowtimesByMovieId(int movieId)
    {
        List<ShowtimeModel> showtimes = _showtimes.FindAll(showtime => showtime.MoviesId == movieId);

        return showtimes;
    }

    static public ShowtimeModel GetShowtimeById(int id)
    {
        ShowtimeModel showtime = _showtimes.Find(showtime => showtime.Id == id);

        if (showtime == null)
        {
            return null;
        }
        return showtime;
    }

    public static void AddMovieFromAddShowtimes(string name, string genre, int duration, string summary, double cost)
    {
        if (MoviesArchiveLogic.CheckIfMovieInArchive(name) && summary != "")
        {
            MoviesLogic.AddMovie(name, genre, duration, summary, cost);
        }
        else if (MoviesArchiveLogic.CheckIfMovieInArchive(name))
        {
            MoviesLogic.AddMovie(name, genre, duration, cost);
        }
    }

    public static List<DateTime> GenerateDateTimesList(DateOnly begindate, DateOnly enddate, TimeOnly time, int interval)
    {
        List<DateTime> Times = [];
        while (begindate <= enddate)
        {
            Times.Add(begindate.ToDateTime(time));
            begindate = begindate.AddDays(interval + 1);
        }
        return Times;
    }

    public static List<ShowtimeModel> GenerateShowTimesList(string movieName, int HallId, List<DateTime> times)
    {
        MovieModel movie = MoviesLogic.GetMovieByName(movieName);
        List<ShowtimeModel> ShowTimes = [];
        foreach (DateTime time in times)
        {
            ShowTimes.Add(new ShowtimeModel(GetNextId(), movie.Id, time, HallId, HallsLogic.GetHallLayout(HallId)));
        }
        return ValdidateAllShowtimes(ShowTimes);
    }

    public static List<ShowtimeModel> ValdidateAllShowtimes(List<ShowtimeModel> showtimes)
    {
        
        List<ShowtimeModel> returnshowtimes = showtimes;
        foreach (ShowtimeModel showtime in showtimes)
        {
            int minutesToAdd = MoviesLogic.GetMovieById(showtime.MoviesId).Duration + 30;
            DateTime endtime = showtime.Time.AddMinutes(minutesToAdd);
            foreach (ShowtimeModel showtime2 in _showtimes)
            {
                if (endtime >= showtime2.Time && showtime.HallId == showtime2.HallId)
                {
                    returnshowtimes.Remove(showtime);
                }
            }
        }
        return returnshowtimes;
    }

    public static void AddShowTimes(List<ShowtimeModel> ShowtimesToAdd)
    {
        foreach (ShowtimeModel showtime in ShowtimesToAdd)
        {
            AddShowtime(showtime);
        }
    }

    public static bool AddShowtime(ShowtimeModel showtime)
    {
        int minutesToAdd = MoviesLogic.GetMovieById(showtime.MoviesId).Duration + 30;
        DateTime endtime = showtime.Time.AddMinutes(minutesToAdd);
        foreach (ShowtimeModel _showtime in _showtimes)
        {
            if (endtime >= _showtime.Time && showtime.HallId == _showtime.HallId)
            {
                return false;
            }
        }
        _showtimes.Add(showtime);
        ShowtimesAccess.WriteAll(_showtimes);
        return true;
    }

    public static void AddShowtime(int movieId, DateTime time, int hallId)
    {

        int id = _showtimes.Count > 0 ? _showtimes.Max(showtime => showtime.Id) + 1 : 1;

        ShowtimeModel showtime = new ShowtimeModel(id, movieId, time, hallId, HallsLogic.GetHallLayout(hallId));

        _showtimes.Add(showtime);
        ShowtimesAccess.WriteAll(_showtimes);
    }

    public void RemoveShowtime(int id)
    {
        ShowtimeModel showtime = _showtimes.Find(showtime => showtime.Id == id);

        if (showtime != null)
        {
            _showtimes.Remove(showtime);
            ShowtimesAccess.WriteAll(_showtimes);
        }
    }

    public bool ReserveSeat(int showtimeId, int row, int seat)
    {
        ShowtimeModel showtime = _showtimes.Find(showtime => showtime.Id == showtimeId);

        if (showtime != null)
        {
            try
            {
                int[] coordinates = SeatsLogic.GetCoordinatesBySeat(SeatsLogic.GetSeatByRowAndSeat(showtime.HallId, row, seat));
                if (showtime.Availability[coordinates[0], coordinates[1]] == 0)
                {
                    showtime.Availability[coordinates[0], coordinates[1]] = 1;
                    ShowtimesAccess.WriteAll(_showtimes);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void UpdateList(ShowtimeModel showtime)
    {
        int index = _showtimes.FindIndex(s => s.Id == showtime.Id);

        if (index != -1)
        {
            _showtimes[index] = showtime;
        }
        else
        {
            _showtimes.Add(showtime);
        }
        ShowtimesAccess.WriteAll(_showtimes);
    }

    public static int CheckAvailability(ShowtimeModel showtime)
    {
        int availableSeats = 0;
        foreach (int seat in showtime.Availability)
        {
            if (seat == 0)
            {
                availableSeats++;
            }
        }
        return availableSeats;
    }

    public static bool CheckIfEnoughAvailableSeats(ShowtimeModel showtime, int numberOfSeats)
    {
        if (numberOfSeats <= 0)
        {
            return false;
        }

        int availableSeats = 0;
        foreach (int seat in showtime.Availability)
        {
            if (seat == 0)
            {
                availableSeats++;
            }
        }
        return availableSeats >= numberOfSeats;
    }

    public static bool CheckIfEnoughAvailableSeats(int showtimeId, int numberOfSeats)
    {
        ShowtimeModel showtime = GetShowtimeById(showtimeId);
        return CheckIfEnoughAvailableSeats(showtime, numberOfSeats);
    }

    public static int GetNextId()
    {
        return _showtimes.Count() + 1;
    }

    public static List<ShowtimeModel> GetShowtimesByDay(DateTime day)
    {
        List<ShowtimeModel> showtimes = _showtimes.FindAll(showtime => showtime.Time.Date == day.Date);

        return showtimes;
    }

    public static List<ShowtimeModel> GetShowtimesByDay(DateTime beginDate, DateTime endDate)
    {
        List<ShowtimeModel> showtimes = _showtimes.FindAll(showtime => showtime.Time.Date >= beginDate.Date && showtime.Time.Date <= endDate.Date);

        return showtimes;
    }

    public static string DisplayShowtimes(DateTime dayToShow, int movieId)
    {
        List<ShowtimeModel> showtimes = GetShowtimesByDay(dayToShow);
        string display = "";

        foreach (ShowtimeModel showtime in showtimes)
        {
            if (showtime.MoviesId == movieId)
            {
                display += showtime.ToString() + "\n";
            }
        }
        return display;
    }

    public static List<ShowtimeModel> GetShowtimes(DateTime beginDate, DateTime endDate, MovieModel movie)
    {
        List<ShowtimeModel> showtimes = _showtimes.FindAll(showtime => showtime.Time.Date >= beginDate.Date && showtime.Time.Date <= endDate.Date && showtime.MoviesId == movie.Id);

        return showtimes;
    }

    public static List<ShowtimeModel> GetShowtimes(DateTime beginDate, DateTime endDate, int movieId)
    {
        return GetShowtimes(beginDate, endDate, MoviesLogic.GetMovieById(movieId));
    }
}
