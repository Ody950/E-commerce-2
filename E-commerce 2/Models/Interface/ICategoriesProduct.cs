
namespace E_commerce_2.Models.Interface
{
    public interface ICategoriesProduct
    {
        Task<List<CategoriesProduct>> GetAllProductsForCategory(int categoryId);

    }
}
