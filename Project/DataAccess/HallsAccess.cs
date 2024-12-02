public class HallsAccess: DataAccessLayer<HallModel>
{
    private static readonly string fileName = "halls";

    public static List<HallModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<HallModel> halls)
    {
        WriteAll(fileName, halls);
    }
}