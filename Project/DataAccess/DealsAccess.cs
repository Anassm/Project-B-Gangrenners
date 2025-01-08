public class DealsAccess : DataAccessLayer<DealModel>
{
    private static readonly string fileName = "deals";

    public static List<DealModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<DealModel> deals)
    {
        WriteAll(fileName, deals);
    }
}