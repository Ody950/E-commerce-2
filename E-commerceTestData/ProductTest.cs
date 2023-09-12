//using E_commerce_2.Models;
//using E_commerce_2.Models.Interface;
//using E_commerce_2.Models.Services;
//using E_commerceTestData.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System.Numerics;


//namespace E_commerceTestData
//{
//    public class ProductTest : Mock
//    {


//        [Fact]
//        protected async void createProduct()
//        {
//            var productService = new ProductService(_db);
//            List<Product> list = await productService.GetProducts();
//            Assert.Equal(3, list.Count);
//            var newproduct = new Product { Id = 5, Name = "Odai", ImageURL = new Uri("https://example.com/image.jpg"), Price = 100000, Amount = 1, Description = "Gold" };
//            _db.Products.Add(newproduct);
//            await _db.SaveChangesAsync();
//            List<Product> list2 = await productService.GetProducts();
//            Assert.Equal(4, list2.Count);
//        }


//        //2-  Update 
//        [Fact]
//        public async void UpdateProduct()
//        {
//            Product newproduct = new Product { Id = 5, Name = "Odai", ImageURL = new Uri("https://example.com/image.jpg"), Price = 100000, Amount = 1, Description = "Gold" };

//            var productService = new ProductService(_db);
//            await productService.CreateProduct(newproduct);
//            newproduct.Name = "TestUpdateMethod";
//            var result = await productService.UpdateProduct(newproduct.Id, newproduct);
//            Assert.Equal("TestUpdateMethod", result.Name);
//        }
//        // 3- Delete 
//        [Fact]
//        public async void DeleteProduct()
//        {
//            Product product = new Product { Id = 10, Name = "Odai", ImageURL = new Uri("https://example.com/image.jpg"), Price = 100000, Amount = 1, Description = "Gold" };
//            var productService = new ProductService(_db);
//            await productService.CreateProduct(product);
//            List<Product> p1 = await productService.GetProducts();
//            Assert.Equal(4, p1.Count);
//            await productService.DeleteProduct(product.Id);
//            List<Product> p2 = await productService.GetProducts();
//            Assert.Equal(3, p2.Count);
//        }
//        // 4- Get 
//        [Fact]
//        public async void GetProductById()
//        {
//            var productService = new ProductService(_db);
//            var c1 = await productService.GetProduct(1);

//            Assert.Equal(1, c1.Id);
//            Assert.Equal("Name1", c1.Name);
//        }
//        // 5- Get all
//        [Fact]
//        public async void GetProducts()
//        {
//            var productService = new ProductService(_db);
//            List<Product> c1 = await productService.GetProducts();
//            Assert.Equal(3, c1.Count);
//        }
//    }
//}
