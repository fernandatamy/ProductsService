namespace ProductsAPI.Data.Repository
{
    public interface IProductRepository
    {
        void SaveChanges();
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        bool ProductExists(int id);
    }
}
