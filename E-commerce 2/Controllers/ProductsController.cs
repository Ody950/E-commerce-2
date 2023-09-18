using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_commerce_2.Data;
using E_commerce_2.Models;
using E_commerce_2.Models.DTO;
using E_commerce_2.Models.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using static IdentityServer3.Core.Events.EventConstants;

namespace E_commerce_2.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProduct _product;
        private TheMarketDBContext _context;

        public ProductsController(IProduct product, TheMarketDBContext context)
        {
            _product = product;
            _context = context;
        }



        //GET: Products

        public async Task<IActionResult> Index()
        {
            if(await _product.GetProducts() == null)
            {
                Problem("Entity set 'TheMarketDBContext.Products'  is null.");
            }

            return View(await _product.GetProducts());

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _product.GetProducts == null)
            {
                return NotFound();
            }

            var product = await _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageURL,Price,Amount,Description")] Product product, IFormFile file)
        {


            if (ModelState.IsValid)
            {
                await _product.CreateProduct(product, file);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }



        //GET: Products/ReplacePicture/5
        [Authorize(Roles = "Administrator, Editor")]
        public async Task<IActionResult> ReplacePicture(int id)
        {
            if (id == null || _product.GetProducts == null)
            {
                return NotFound();
            }

            var product = await _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products1/ReplacePicture/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Editor")]

        public async Task<IActionResult> ReplacePicture(int Id, IFormFile file)
        {
          

            if (ModelState.IsValid)
            {
                try
                {
                    await _product.ReplacePictureProduct(Id, file);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var theProduct = await _product.GetProduct(Id);

                    if (theProduct == null)
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
            return RedirectToAction(nameof(Index));
        }



        //GET: Products/Edit/5
        [Authorize(Roles = "Administrator, Editor")]

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _product.GetProducts == null)
            {
                return NotFound();
            }

            var product = await _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Editor")]

        public async Task<IActionResult> Edit([Bind("Id,Name,ImageURL,Price,Amount,Description")] int Id, Product product)
        {
            if (Id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _product.UpdateProduct(Id, product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var theProduct = await _product.GetProduct(Id);

                    if (theProduct == null)
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
            return View(product);
        }





        //// GET: Products/Create
        //[Authorize(Roles = "Editor, Administrator")]
        //public IActionResult AddProductToCategories(int ProductId, int CategoryId)
        //{
        //    CategoriesProduct categoryProduct = new CategoriesProduct()
        //    {
        //        CategoriesId = CategoryId,
        //        ProductId = ProductId
        //    };

        //    return View(categoryProduct);
        //}

        [HttpGet]
        [Authorize(Roles = "Editor, Administrator")]
        public IActionResult AddProductToCategories(int ProductId)
        {
            CategoriesProduct categoryProduct = new CategoriesProduct()
            {
                ProductId = ProductId
            };
            ViewBag.Categories = _context.Categories.ToList();
            return View(categoryProduct);

        }

        [Authorize(Roles = "Editor, Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddProductToCategories(CategoriesProduct categoryProduct)
        {
            if (ModelState.IsValid)
            {
                await _product.AddProductToCategories(categoryProduct.CategoriesId, categoryProduct.ProductId);
                TempData["AlertMessage"] = "A new product Added to a Category successfully :)";
                return RedirectToAction("Details", "Categories", new { id = categoryProduct.CategoriesId });
            }
            else
            {
                return View(categoryProduct);
            }
        }



        //[Authorize(Roles = "Editor, Administrator")]
        //[HttpPost]
        //public async Task<IActionResult> AddProductToCategories(CategoriesProduct categoryProduct)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _product.AddProductToCategories(categoryProduct.CategoriesId, categoryProduct.ProductId);


        //        return RedirectToAction("Details", "Categories", new { id = categoryProduct.CategoriesId });
        //    }
        //    else
        //    {
        //        return RedirectToAction("ViewAllProducts", "Products");
        //    }

        //}







        // GET: Products/DeleteView/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteView(int id)
        {
            if (id == null || _product.GetProducts == null)
            {
                return NotFound();
            }

            var product = await _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // POST: Products/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_product.GetProducts == null)
            {
                return Problem("Entity set 'TheMarketDBContext.Products'  is null.");
            }
            var product = await _product.GetProduct(id);
            if (product != null)
            {
                await _product.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //    return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }



}
