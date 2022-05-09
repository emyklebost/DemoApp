namespace DemoApp.Domain.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
    }
}