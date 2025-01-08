public class SubscriptionAccess : DataAccessLayer<SubscriptionModel>
{
    private static readonly string fileName = "subscriptions";

    public static List<SubscriptionModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<SubscriptionModel> subscriptions)
    {
        WriteAll(fileName, subscriptions);
    }
}