public class DealsLogic
{
    private static List<DealModel> _dealItems { get; set; } = DealsAccess.LoadAll();

    public static List<DealModel> GetAll()
    {
        return _dealItems;
    }

    public static DealModel GetById(int id)
    {
        return _dealItems.FirstOrDefault(x => x.Id == id);
    }

    public static DealModel GetByName(string name)
    {
        return _dealItems.FirstOrDefault(x => x.Name == name);
    }


}