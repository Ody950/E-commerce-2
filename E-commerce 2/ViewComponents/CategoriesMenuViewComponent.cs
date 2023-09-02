


using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class CategoriesMenuViewComponent : ViewComponent
{
    private readonly ICategories _categories;
    private readonly IMemoryCache _cache;

    public CategoriesMenuViewComponent(ICategories categories, IMemoryCache cache)
    {
        _categories = categories;
        _cache = cache;
    }

    // Asynchronous method
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _cache.GetOrCreateAsync("CategoriesCacheKey", async (cacheEntry) =>
        {
            // Set cache entry options (e.g., cache duration)
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Cache for 30 minutes

            // Retrieve categories from the database
            return await _categories.GetCategories();
        });

        return View(categories);
    }
}









//using E_commerce_2.Models;
//using E_commerce_2.Models.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace E_commerce_2.ViewComponents
//{
//    public class CategoriesMenuViewComponent : ViewComponent
//    {
//        private readonly ICategories _categories;
//        public CategoriesMenuViewComponent(ICategories categories)
//        {
//            _categories = categories;
//        }

//        //Asynchronous method
//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var categories = await _categories.GetCategories();
//            return View(categories);
//        }
//    }
//}
