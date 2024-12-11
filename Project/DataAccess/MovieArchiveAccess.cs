public class MovieArchiveAccess : DataAccessLayer<MovieModel>
{
    private static readonly string fileName = "moviearchive";

    public static List<MovieModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<MovieModel> movies)
    {
        WriteAll(fileName, movies);
    }
}