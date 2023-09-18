using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_commerce_2.Pages.CategoriesPage
{
    public class IndexModel : PageModel
    {

        private readonly ICategories _categories;

        public IndexModel(ICategories categories)
        {
            _categories = categories;
        }

        [BindProperty]
        public List<Categories> categories { get; set; }
        public async Task OnGet()
        {
            categories = await _categories.GetCategories();
        }
    }
}
