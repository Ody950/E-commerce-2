using E_commerce_2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_commerce_2.Auth.Models;

namespace E_commerce_2.Data

{
    public class TheMarketDBContext : IdentityDbContext<ApplicationUser>
    {

            public TheMarketDBContext(DbContextOptions options) : base(options)
        {
        }
       
        public DbSet<Categories> Categories { get; set; }
        public DbSet<CategoriesProduct> CategoriesProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartsProducts { get; set; }
        public DbSet<OrderProduct> OrdersProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<CategoriesProduct>()
        .HasKey(cp => new { cp.CategoriesId, cp.ProductId });

            modelBuilder.Entity<CategoriesProduct>()
                .HasOne(cp => cp.categories)
                .WithMany(c => c.categoriesProducts)
                .HasForeignKey(cp => cp.CategoriesId);

            modelBuilder.Entity<CategoriesProduct>()
                .HasOne(cp => cp.product)
                .WithMany(p => p.categoriesProducts)
                .HasForeignKey(cp => cp.ProductId);

            
            modelBuilder.Entity<CartProduct>().HasKey
            (x => new
            {
                x.CartId,
                x.ProductId
            });
            modelBuilder.Entity<OrderProduct>().HasKey
            (x => new
            {
                x.OrderId,
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



            //modelBuilder.Entity<CategoriesProduct>().HasData(

            //    new CategoriesProduct { CategoriesId = 1, ProductId = 1 },
            //    new CategoriesProduct { CategoriesId = 1, ProductId = 2 },
            //     new CategoriesProduct { CategoriesId = 2, ProductId = 3 }

            //    );

            SeedRole(modelBuilder, "Administrator");
            SeedRole(modelBuilder, "Editor");
            SeedRole(modelBuilder, "Users");

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
        public DbSet<E_commerce_2.Models.Cart> Cart { get; set; } = default!;
    }
}
