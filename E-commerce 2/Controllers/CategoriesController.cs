using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_commerce_2.Data;
using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using E_commerce_2.Models.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace E_commerce_2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategories _categories;
        private readonly IProduct _product;
        private readonly ICategoriesProduct _categoriesProduct;
        private TheMarketDBContext _context;

        public CategoriesController(ICategories categories, IProduct product, ICategoriesProduct categoriesProduct, TheMarketDBContext context)
        {
            _categories = categories;
            _product = product;
            _categoriesProduct = categoriesProduct;
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            if (await _categories.GetCategories() == null)
            {
                Problem("Entity set 'TheMarketDBContext.Categories'  is null.");
            }

            return View(await _categories.GetCategories());
        }



        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _categories.GetCategories == null)
            {
                return NotFound();
            }

            var categories = await _categories.GetCategoryWithProducts(id);
            var products = _categoriesProduct.GetAllProductsForCategory(id);

            if (categories == null)
            {
                return NotFound();
            }
            ViewBag.Products = products;
            return View(categories); 
        }



        // GET: Categories1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Logo,Description")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                await _categories.Create(categories);

                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }


        [Authorize(Roles = "Editor")]
        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _categories.GetCategories == null)
            {
                return NotFound();
            }

            var categories = await _categories.GetCategory(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }


        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Logo,Description")] int Id, Categories categories)
        {
            if (Id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categories.UpdateCategories(Id, categories);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var theCategories = await _categories.GetCategory(Id);

                    if (theCategories == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            if (id == null || _categories.GetCategories == null)
            {
                return NotFound();
            }

            var theCategories = await _categories.GetCategory(id);
            if (theCategories == null)
            {
                return NotFound();
            }

            return View(theCategories);
        }

        // POST: Categories/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_categories.GetCategories == null)
            {
                return Problem("Entity set 'TheMarketDBContext.Categories'  is null.");
            }
            var theCategories = await _categories.GetCategory(id);
            if (theCategories != null)
            {
                await _categories.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool CategoriesExists(int id)
        //{
        //    return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        //}





        // GET: Products/Create
        [Authorize(Roles = "Editor")]
        public IActionResult AddProductToCategories(int CategoryId)
        {
            CategoriesProduct categoryProduct = new CategoriesProduct()
            {
                CategoriesId = CategoryId
            };

            return View(categoryProduct);
        }
        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> AddProductToCategories(CategoriesProduct categoryProduct)
        {
            if (ModelState.IsValid)
            {
                await _categories.AddProductToCategories(categoryProduct.CategoriesId, categoryProduct.product);
                TempData["AlertMessage"] = "A new product Added to a Category successfully :)";
                return RedirectToAction("Details", "Categories", new { id = categoryProduct.CategoriesId });
            }
            else
            {
                return View(categoryProduct);
            }
        }

        public IActionResult CategoryDropdown()
        {
            var categories = _categories.GetCategories();
            return PartialView("_CategoryDropdown", categories);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RemoveProductFromCategory(int CategoryId, int ProductId)
        {
            await _categories.deleteProductFromCategories(CategoryId, ProductId);
            TempData["AlertMessage"] = "The product Removed From a Category successfully :)";
            return RedirectToAction("Details", "Categories", new { id = CategoryId });
        }

        // GET: Categories/GetAllProductsForCategory/5

        public async Task<IActionResult>GetAllProductsForCategory(int CategoryId)
        {
            var products = await _categoriesProduct.GetAllProductsForCategory(CategoryId);
            return View(products);
        }




        [HttpPost]
        public async Task<IActionResult> _GetAllProductsForCategory(int categoryId, string sortOrder)
        {
            var category = await _categories.GetCategory(categoryId);

            if (category == null)
            {
                return NotFound();
            }


            var productsQuery = _context.CategoriesProducts
                            .Include(cp => cp.product)
                            .Where(cp => cp.CategoriesId == categoryId);
            switch (sortOrder)
            {
                case "HighToLowPrice":
                    productsQuery = productsQuery.OrderByDescending(p => p.product.Price);
                    break;
                case "LowToHighPrice":
                    productsQuery = productsQuery.OrderBy(p => p.product.Price);
                    break;
                case "OrderByAlphaBetAsend":
                    productsQuery = productsQuery.OrderByDescending(p => p.product.Name);
                    break;
                case "OrderByAlphaBetDesend":
                    productsQuery = productsQuery.OrderBy(p => p.product.Name);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.product.Name);
                    break;
            }
            var sortedProducts = await productsQuery.ToListAsync();


            return View("_GetAllProductsForCategory", sortedProducts);
        }

        //return View("_GetAllProductsForCategory", sortedProducts);

        //ViewBag.SortedProducts = sortedProducts;

        // Return the Details view with the sorted products
        // Store the sortedProducts in TempData
        //TempData["SortedProducts"] = sortedProducts;

        // Redirect to the "Details" action of the "Categories" controller with the categoryId parameter
        //return RedirectToAction("Details", "Categories", new { id = categoryId });


        //return View("Details", "Categories", new { id = categoryId }, sortedProducts);
        //return RedirectToAction("Details", "Categories", new { id = categoryId, sortedProducts});







    }
}
