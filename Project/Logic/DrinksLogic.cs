public class DrinksLogic
{
    private static List<DrinkModel> _drinkItems { get; set; } = DrinksAccess.LoadAll();

    public static List<DrinkModel> GetAll()
    {
        return _drinkItems;
    }

    public static DrinkModel GetById(int id)
    {
        return _drinkItems.FirstOrDefault(x => x.Id == id);
    }

    public static List<DrinkModel> GetByCategory(string category)
    {
        return _drinkItems.Where(x => x.SubCategory == category).ToList();
    }
    
}
