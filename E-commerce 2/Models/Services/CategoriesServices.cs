using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using E_commerce_2.Data;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Models.Services

{
    public class CategoriesServices : ICategories
    {
        private TheMarketDBContext _context;
        private readonly IConfiguration _Configuration;


        public CategoriesServices(TheMarketDBContext context, IConfiguration config)
        {
            _context = context;
            _Configuration = config;
        }


        public async Task<List<CategoriesProduct>> GetAllProductsForCategory(int categoryId)
        {
            var productsForCategory = await _context.CategoriesProducts
                .Include(pc => pc.product)
                .Where(pc => pc.CategoriesId == categoryId)
                .ToListAsync();

            return productsForCategory;
        }



        public async Task<Product> AddProductToCategories(int categoriesId, Product product, IFormFile file)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(_Configuration.GetConnectionString("StorageAccount"), "images");
                await container.CreateIfNotExistsAsync();

                BlobClient blob = container.GetBlobClient(file.FileName);
                using var stream = file.OpenReadStream();

                BlobUploadOptions options = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
                };

                if (!await blob.ExistsAsync())
                {
                    await blob.UploadAsync(stream, options);
                }

                product.ImageURL = blob.Uri;

                _context.Products.Add(product);
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
            catch (Exception ex)
            {
                throw ex;
            }

           

        }



        public async Task deleteProductFromCategories(int categoriesId, int productId)
        {
            // Find the category and product
            var category = await _context.Categories
                .Include(c => c.categoriesProducts)
                .FirstOrDefaultAsync(x => x.Id == categoriesId);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (category != null && product != null)
            {
                var associationToRemove = category.categoriesProducts
                    .FirstOrDefault(cp => cp.ProductId == productId);

                if (associationToRemove != null)
                {
                    category.categoriesProducts.Remove(associationToRemove);
                    await _context.SaveChangesAsync();
                }
                else
                {

                }
            }
            else
            {

            }
        }

        public async Task<Categories> GetCategoryWithProducts(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.categoriesProducts)
                    .ThenInclude(cp => cp.product)
                .SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Categories> Create(Categories categories, IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(_Configuration.GetConnectionString("StorageAccount"), "images");
            await container.CreateIfNotExistsAsync();

            BlobClient blob = container.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
            };

            if (!await blob.ExistsAsync())
            {
                await blob.UploadAsync(stream, options);
            }

            categories.Logo = blob.Uri;
            
             _context.Entry(categories).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return categories;
        }

       
        public async Task Delete(int Id)
        {
            Categories categories = await GetCategory(Id);

            if (categories != null)
            {
                _context.Entry(categories).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }

        }
        
        public  async Task<List<Categories>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Categories> GetCategory(int Id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
        }

            public async Task<Categories> UpdateCategories(int id, Categories categories)
        {
            _context.Entry(categories).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return categories;
        }

        
    }
}