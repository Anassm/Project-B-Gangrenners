public class MoviesAccess : DataAccessLayer<MovieModel>
{
    private static readonly string fileName = "movies";

    public static List<MovieModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<MovieModel> movies)
    {
        WriteAll(fileName, movies);
    }
}