using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using E_commerce_2.Data;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Models.Services

{
    public class ProductService : IProduct
    {
        private TheMarketDBContext _context;
        private readonly IConfiguration _Configuration;


        public ProductService(TheMarketDBContext context, IConfiguration config)
        {
            _context = context;
            _Configuration = config;
        }

        //public async Task<Product> CreateProduct(Product product)
        //{
        //    _context.Entry(product).State = EntityState.Added;
        //    await _context.SaveChangesAsync();
        //    return product;
        //}



        public async Task<Product> CreateProduct(Product product, IFormFile file)
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

                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Product> ReplacePictureProduct(int Id, IFormFile file)
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

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);

            product.ImageURL = blob.Uri;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
            






        //public async Task<Product> CreateProduct(Product product, IFormFile file)
        //{



        //    BlobContainerClient container = new BlobContainerClient(_Configuration.GetConnectionString("StorageAccount"), "images");

        //    await container.CreateIfNotExistsAsync();
        //    BlobClient blob = container.GetBlobClient(file.FileName);
        //    using var stream = file.OpenReadStream();

        //    BlobUploadOptions options = new BlobUploadOptions()
        //    {
        //        HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
        //    };


        //    if (!blob.Exists())
        //    {
        //        await blob.UploadAsync(stream, options);
        //    }

        //    product.ImageURL = blob.Uri;


        //    _context.Entry(product).State = EntityState.Added;
        //        await _context.SaveChangesAsync();
        //        product.Id = product.Id;
        //        return product;
        //     }


        //_context.Entry(product).State = EntityState.Added;
        //   await _context.SaveChangesAsync();
        //   product.Id = product.Id;
        //   return product;






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