using E_commerce_2.Data;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Models.Services

{
    public class CategoriesServices : ICategories
    {

        private readonly TheMarketDBContext _context;

        public CategoriesServices(TheMarketDBContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductToCategories(int categoriesId, Product product)
        {

            _context.Entry(product).State = EntityState.Added;

            await _context.SaveChangesAsync();

            CategoriesProduct categoryProduct = new CategoriesProduct()
            {
                ProductId = product.Id,
                CategoriesId = categoriesId
            };

            _context.Entry(categoryProduct).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Categories> Create(Categories categories)
        {
            _context.Entry(categories).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return categories;
        }

        public async Task Delete(int id)
        {
            Categories categories = await GetCategory(id); ;

            if (categories != null)
            {
                _context.Entry(categories).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }

        }

        public Task deleteProductFromCategories(int categoriesId, int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Categories>> GetCategories()
        {
            return await _context.Categories.Include(x => x.categoriesProducts).ThenInclude(y => y.product).ToListAsync();
        }

        public async Task<Categories> GetCategory(int id)
        {
            return await _context.Categories.Include(x => x.categoriesProducts).ThenInclude(y => y.product).FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task<Categories> UpdateCategories(int id, Categories categories)
        {
            _context.Entry(categories).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return categories;
        }
    }
}