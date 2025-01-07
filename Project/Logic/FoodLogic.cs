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

    public static List<String> GetCategories()
    {
        return _foodItems.Select(x => x.SubCategory).Distinct().ToList();
    }

    public static FoodModel GetByName(string name)
    {
        return _foodItems.FirstOrDefault(x => x.Name == name);
    }
}