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
}
