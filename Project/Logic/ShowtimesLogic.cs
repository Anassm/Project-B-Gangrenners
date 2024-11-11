using System.Dynamic;

public class ShowtimesLogic
{
    private List<ShowtimeModel> _showtimes;

    public ShowtimesLogic()
    {
        _showtimes = ShowtimesAccess.LoadAll();
    }

    public List<ShowtimeModel> GetShowtimesByMovieId(int movieId)
    {
        List<ShowtimeModel> showtimes = _showtimes.FindAll(showtime => showtime.MoviesId == movieId);

        return showtimes;
    }

    public ShowtimeModel GetShowtimeById(int id)
    {
        ShowtimeModel showtime = _showtimes.Find(showtime => showtime.Id == id);

        return showtime;
    }

    public void AddShowtime(ShowtimeModel showtime)
    {
        _showtimes.Add(showtime);
        ShowtimesAccess.WriteAll(_showtimes);
    }

    public void AddShowTime(int movieId, DateTime time, int hallId)
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
                showtime.Availability[coordinates[0], coordinates[1]] = 1;
                ShowtimesAccess.WriteAll(_showtimes);
                return true;
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


}
