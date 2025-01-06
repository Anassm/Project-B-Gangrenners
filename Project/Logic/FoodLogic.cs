public class FoodLogic
{
    private static List<FoodModel> _foodItems { get; set; } = FoodAccess.LoadAll();
    
    public static List<FoodModel> GetAll()
    {
        return _foodItems;
    }

    public static FoodModel GetById(int id)
    {
        return _foodItems.FirstOrDefault(x => x.Id == id);
    }

    public static List<FoodModel> GetByCategory(string category)
    {
        return _foodItems.Where(x => x.SubCategory == category).ToList();
    }
}