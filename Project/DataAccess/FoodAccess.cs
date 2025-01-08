public class FoodAccess : DataAccessLayer<FoodModel>
{
    private static readonly string fileName = "food";

    public static List<FoodModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<FoodModel> foods)
    {
        WriteAll(fileName, foods);
    }
}