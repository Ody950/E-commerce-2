using E_commerce_2.Models;

namespace E_commerce_2.Controllers
{
    internal class CategoryProductsViewModel
    {
        public Categories Category { get; set; }
        public List<CategoriesProduct> SortedProducts { get; set; }
    }
}