using E_commerce_2.Auth.Models;
using E_commerce_2.Models;
using E_commerce_2.Models.DTO;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;



namespace E_commerce_2.Pages.TheCart
{
    public class CartModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICart _cart;
        public CartModel(UserManager<ApplicationUser> userManager, ICart cart) 
        {
            _cart = cart;
            _userManager = userManager;
        }

        public IEnumerable<CartProduct> CartProducts { get; set; }


        public async Task OnGet() 
        {
            var cart = new Cart
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            await _cart.Create(cart);
            ApplicationUser user = await _userManager.GetUserAsync(User);
            CartProducts = await _cart.GetCartProductByUserId(user.Id);

        }

        public async Task<IActionResult> OnPostUpdate(int id)
        {
            int updatedQuantity = Convert.ToInt32(Request.Form["Quantity"]);
            ApplicationUser user = await _userManager.GetUserAsync(User);
            CartProduct cartItem = await _cart.GetCartProductByProductIdForUser(user.Id, id);

            cartItem.Quantity = updatedQuantity;
            await _cart.UpdateCartProduct(cartItem);

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostDelete(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            await _cart.RemoveCartProduct(user.Id, id);

            return RedirectToPage();
        }



    }
}
