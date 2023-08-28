using E_commerce_2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Data

{
    public class TheMarketDBContext : DbContext
    {
        public TheMarketDBContext(DbContextOptions options) : base(options)
        {
        }
       
        public DbSet<Categories> Categories { get; set; }
        public DbSet<CategoriesProduct> CategoriesProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<CategoriesProduct>().HasKey
             (x => new
             {
                 x.CategoriesId,
                 x.ProductId
             });


            modelBuilder.Entity<Product>().HasData(

                new Product { Id = 1, Name = "Name1", ImageURL = new Uri("https://odai.core.windows.net/images/.jpg"), Price = 5, Description = "1111111111111111"},
                new Product { Id = 2, Name = "Name2", ImageURL = new Uri("https://odai.core.windows.net/images/.jpg"), Price = 10, Description = "2222222222222222" },
                new Product { Id = 3, Name = "Name3 ", ImageURL = new Uri("https://odai.core.windows.net/images/.jpg"), Price = 15, Description = "3333333333333333333"}
                
            );

            modelBuilder.Entity<Categories>().HasData(
              new Categories { Id = 1, Name = "Beauty", Logo = new Uri("https://odai.core.windows.net/images/.jpg"), Description = "111111111111111111111111" },
              new Categories { Id = 2, Name = "Clothes", Logo = new Uri("https://odai.core.windows.net/images/.jpg"), Description = "22222222222222222" }
             

            );


            modelBuilder.Entity<CategoriesProduct>().HasData(

                new CategoriesProduct { CategoriesId = 1, ProductId = 1 },
                new CategoriesProduct { CategoriesId = 1, ProductId = 2 },
                 new CategoriesProduct { CategoriesId = 2, ProductId = 3 }

                );



        }
        private void SeedRole(ModelBuilder modelBuilder, string roleName)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);
        }
    }
}