using E_commerce_2.Models.Interface;

namespace E_commerce_2.Models.Interface

{
    public interface ICategories
    {
        Task<List<Categories>> GetCategories();
        Task<Categories> GetCategory(int id);
        Task<Categories> Create(Categories categories);
        Task<Categories> UpdateCategories(int id, Categories categories);
        Task Delete(int id);
        Task<Product> AddProductToCategories(int categoriesId, Product product);
        Task deleteProductFromCategories(int categoriesId, int productId);
    }
}