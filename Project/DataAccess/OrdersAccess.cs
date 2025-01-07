public class OrdersAccess : DataAccessLayer<OrderModel>
{
    private static readonly string fileName = "orders";

    public static List<OrderModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<OrderModel> orders)
    {
        WriteAll(fileName, orders);
    }
}