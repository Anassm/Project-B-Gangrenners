public class ShowtimesAccess : DataAccessLayer<ShowtimeModel>
{
    private static readonly string fileName = "showtimes";

    public static List<ShowtimeModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<ShowtimeModel> showtimes)
    {
        WriteAll(fileName, showtimes);
    }
}