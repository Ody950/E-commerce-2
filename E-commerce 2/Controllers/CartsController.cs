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
using E_commerce_2.Models.DTO;

namespace E_commerce_2.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICart _cart;

        public CartsController(ICart Cart)
        {
            _cart = Cart;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _cart.GetCarts());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cart = await _cart.GetCart(id);
            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartDTO cartDTO)
        {
          
            var newCart = await _cart.Create(cartDTO);

            return View(newCart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int id, CartDTO upDateCartDTO)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _cart.UpdateCart( id, upDateCartDTO);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

       

        //// GET: Carts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Carts == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Carts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        //// POST: Carts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Carts == null)
        //    {
        //        return Problem("Entity set 'TheMarketDBContext.Carts'  is null.");
        //    }
        //    var cart = await _context.Carts.FindAsync(id);
        //    if (cart != null)
        //    {
        //        _context.Carts.Remove(cart);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CartExists(int id)
        //{
        //  return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
