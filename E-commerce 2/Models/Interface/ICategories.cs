
namespace E_commerce_2.Models.Interface

{
    public interface ICategories
    {
        Task<List<Categories>> GetCategories();
        Task<Categories> GetCategory(int id);

        Task<Categories> GetCategoryWithProducts(int id);
        Task<Categories> Create(Categories categories, IFormFile file);
        Task<Categories> UpdateCategories(int id, Categories categories);
        Task Delete(int id);
        Task<Product> AddProductToCategories(int categoriesId, Product product, IFormFile file);
        Task deleteProductFromCategories(int categoriesId, int productId);
    }
}