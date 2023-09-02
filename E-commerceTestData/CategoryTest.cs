using E_commerce_2.Models.Interface;
using E_commerce_2.Models.Services;
using E_commerce_2.Models;
using E_commerceTestData.Mocks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceTestData
{
  
    public class CategoryTest : Mock
    {
        
        [Fact]
        protected async void CreatCategoreis()
        {
            var categoreisServices = new CategoriesServices(_db);
            List<Categories> list = await categoreisServices.GetCategories();
            Assert.Equal(2, list.Count);
            var newCategories = new Categories { Id = 5, Name = "Odai", Logo = new Uri("https://example.com/image.jpg"), Description = "Gold" };
            _db.Categories.Add(newCategories);
            await _db.SaveChangesAsync();
            List<Categories> list2 = await categoreisServices.GetCategories();
            Assert.Equal(3, list2.Count);
        }


        //2-  Update 
        [Fact]
        public async void UpdateCategoreis()
        {
            Categories category = new Categories { Id = 10, Name = "Odai", Logo = new Uri("https://example.com/image.jpg"), Description = "Gold" };
            var categoreisServices = new CategoriesServices(_db);
            await categoreisServices.Create(category);
            category.Name = "TestUpdate";
            var result = await categoreisServices.UpdateCategories(3, category);
            Assert.Equal("TestUpdate", result.Name);
        }
        // 3-  Delete 
        [Fact]
        public async void DeleteCategory()
        {
            Categories category = new Categories { Id = 5, Name = "Odai", Logo = new Uri("https://example.com/image.jpg"), Description = "Gold" }; ;
            var categoreisServices = new CategoriesServices(_db);
            await categoreisServices.Create(category);
            List<Categories> c1 = await categoreisServices.GetCategories();
            Assert.Equal(3, c1.Count);
            await categoreisServices.Delete(3);
            int Id = category.Id;
            List<Categories> c2 = await categoreisServices.GetCategories();
            Assert.Equal(3, c2.Count);
        }
        // 4-  Get 
        [Fact]
        public async void GetCategoryById()
        {
            var categoreisServices = new CategoriesServices(_db);
            var c1 = await categoreisServices.GetCategory(2);

            Assert.Equal(2, c1.Id);
            Assert.Equal("Clothes", c1.Name);
        }
        // 5-  Get all 
        [Fact]
        public async void GetCategories()
        {
            var categoreisServices = new CategoriesServices(_db);
            List<Categories> c1 = await categoreisServices.GetCategories();
            Assert.Equal(2, c1.Count); 
        }

    }
}