using E_commerce_2.Auth.Models;
using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce_2.ViewComponents
{
    public class CartViewComponent : ViewComponent 
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICart _cart;

        public CartViewComponent(UserManager<ApplicationUser> userManager, ICart cart)
        {
            _userManager = userManager;
            _cart = cart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId((ClaimsPrincipal)User);
            var cart = new Cart
            {
                UserId = userId
            };
            await _cart.Create(cart);
            var cartItems = await _cart.GetCartProductByUserId(userId);

            return View(cartItems);
        }

    }
}
