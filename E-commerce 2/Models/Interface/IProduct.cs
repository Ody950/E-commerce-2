
namespace E_commerce_2.Models.Interface

{
    public interface IProduct
    {
        Task<Product> CreateProduct(Product product, IFormFile file);
        Task<Product> GetProduct(int Id);
        Task<List<Product>> GetProducts();
        Task<Product> UpdateProduct(int Id, Product product);
        Task<CategoriesProduct> AddProductToCategories(int ProductId, int CategoryId);
        Task<Product> ReplacePictureProduct(int Id, IFormFile file);
        Task Delete(int Id);
    }
}