using E_commerce_2.Data;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Models.Services
{
    public class CategoriesProductService : ICategoriesProduct
    {

        private TheMarketDBContext _context;

        public CategoriesProductService(TheMarketDBContext context)
        {
            _context = context;
        }
        public async Task<List<CategoriesProduct>> GetAllProductsForCategory(int categoryId)
        {
            var productsForCategory = await _context.CategoriesProducts
                .Include(pc => pc.product)
                .Where(pc => pc.CategoriesId == categoryId)
                .ToListAsync();

            return productsForCategory;
        }


    }
}