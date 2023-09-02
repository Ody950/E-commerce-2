using E_commerce_2.Models;

namespace E_commerce_2.Models

{
    public class CategoriesProduct
    {
        public int CategoriesId { get; set; }
        public int ProductId { get; set; }
        public Categories? categories { get; set; }
        public Product? product { get; set; }
    }
}