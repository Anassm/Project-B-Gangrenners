public class DrinksAccess : DataAccessLayer<DrinkModel>
{
    private static readonly string fileName = "drinks";

    public static List<DrinkModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<DrinkModel> drinks)
    {
        WriteAll(fileName, drinks);
    }
}