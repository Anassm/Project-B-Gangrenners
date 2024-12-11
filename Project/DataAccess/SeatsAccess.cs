public class SeatsAccess : DataAccessLayer<SeatModel>
{
    private static readonly string fileName = "seats";

    public static List<SeatModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<SeatModel> seats)
    {
        WriteAll(fileName, seats);
    }
}