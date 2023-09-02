
namespace E_commerce_2.Models.Interface

{
    public interface IProduct
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProduct(int Id);
        Task<List<Product>> GetProducts();
        Task<Product> UpdateProduct(int Id, Product product);
        Task DeleteProduct(int Id);
    }
}