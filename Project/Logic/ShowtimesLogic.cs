public class ShowtimesLogic
{
    public List<ShowtimeModel> showtimes;

    public ShowtimesLogic()
    {
        showtimes = ShowtimesAccess.LoadAll();
    }

    public List<ShowtimeModel> GetShowtimesByMovieId(int movieId)
    {
        List<ShowtimeModel> allShowtimes = showtimes.FindAll(showtime => showtime.MoviesId == movieId);

        return allShowtimes;
    }

    public ShowtimeModel GetShowtimeById(int id)
    {
        ShowtimeModel showtime = showtimes.Find(showtime => showtime.Id == id);

        return showtime;
    }
}
