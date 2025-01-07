public class ProductsLogic
{
    private static List<ProductModel> _productItems { get; set; } = ProductsAccess.LoadAll();

    public static List<ProductModel> GetAll()
    {
        return _productItems;
    }

    public static ProductModel GetById(int id)
    {
        return _productItems.FirstOrDefault(x => x.Id == id);
    }

    public static List<ProductModel> GetByCategory(string category)
    {
        return _productItems.Where(x => x.SubCategory == category).ToList();
    }

    public static List<String> GetCategories()
    {
        return _productItems.Select(x => x.SubCategory).Distinct().ToList();
    }

    public static ProductModel GetByName(string name)
    {
        return _productItems.FirstOrDefault(x => x.Name == name);
    }
    
}