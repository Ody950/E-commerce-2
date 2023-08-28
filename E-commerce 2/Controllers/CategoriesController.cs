using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_commerce_2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategories _Categories;

        public CategoriesController(ICategories Categories)
        {
            _Categories = Categories;
        }
        public async Task<IActionResult> Index()
        {
            var list1 = await _Categories.GetCategories();

            return View(list1);
        }
        public async Task<IActionResult> Details(int id)
        {
            var category = await _Categories.GetCategory(id);

            return View(category);
        }
    }
}