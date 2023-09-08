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

namespace E_commerce_2.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProduct _product;

        public ProductsController(IProduct product)
        {
            _product = product;
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
        public async Task<IActionResult> Create([Bind("Id,Name,ImageURL,Price,Amount,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _product.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET: Products/Edit/5
        [Authorize(Roles = "Editor")]

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
        [Authorize(Roles = "Editor")]

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


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_product.GetProducts == null)
            {
                return Problem("Entity set 'TheMarketDBContext.Products'  is null.");
            }
            var product = await _product.GetProduct(id);
            if (product != null)
            {
                await _product.DeleteProduct(id);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //    return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
