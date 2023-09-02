using E_commerce_2.Data;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Models.Services

{
    public class ProductService : IProduct
    {
            private TheMarketDBContext _context;

            public ProductService(TheMarketDBContext context)
            {
                _context = context;
            }

        //public async Task<Product> CreateProduct(Product product)
        //{
        //    _context.Entry(product).State = EntityState.Added;
        //    await _context.SaveChangesAsync();
        //    return product;
        //}



        public async Task<Product> CreateProduct(Product product)
        {
         _context.Entry(product).State = EntityState.Added;
            await _context.SaveChangesAsync();
            product.Id = product.Id;
            return product;
        }



        public async Task DeleteProduct(int Id)
        {
            Product product = await GetProduct(Id);

            if (product != null)
            {
                _context.Entry(product).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProduct(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProduct(int Id, Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}