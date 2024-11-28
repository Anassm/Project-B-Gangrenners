public class ReservationsAccess : DataAccessLayer<ReservationModel>
{
    private static readonly string fileName = "reservations";

    public static List<ReservationModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<ReservationModel> reservations)
    {
        WriteAll(fileName, reservations);
    }
}