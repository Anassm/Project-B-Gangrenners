public class ProductsAccess : DataAccessLayer<ProductModel>
{
    private static readonly string fileName = "products";

    public static List<ProductModel> LoadAll()
    {
        return LoadAll(fileName);
    }

    public static void WriteAll(List<ProductModel> products)
    {
        WriteAll(fileName, products);
    }
}