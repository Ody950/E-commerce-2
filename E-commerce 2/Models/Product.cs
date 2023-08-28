using E_commerce_2.Models;

namespace E_commerce_2.Models

{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri ImageURL { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<CategoriesProduct> categoriesProducts { get; set; }

    }
}