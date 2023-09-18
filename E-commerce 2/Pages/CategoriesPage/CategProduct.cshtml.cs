using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_commerce_2.Pages.CategoriesPage
{
    public class CategProductModel : PageModel
    {

        private readonly ICategories _categories;

        public CategProductModel(ICategories categories)
        {
            _categories = categories;
        }


        [BindProperty]
        public Categories categories { get; set; }
        public async Task OnGet(int id)
        {
            categories = await _categories.GetCategoryWithProducts(id);
        }
  
    }
}
