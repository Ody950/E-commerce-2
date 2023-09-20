using E_commerce_2.Auth.Models;
using E_commerce_2.Models.Interface;
using E_commerce_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using E_commerce_2.Models.DTO;

namespace E_commerce_2.Pages.TheCart
{
    public class ProductModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProduct _product;
        private readonly ICart _cart;


        public ProductModel(IProduct product, ICart cart, UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
            _product = product;
            _cart = cart;
        }


        public Product SingleProduct { get; set; }

        [BindProperty]
        public ProductInput Input { get; set; }

        public async Task OnGet(int id)
        {
            var cart = new Cart
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            await _cart.Create(cart);
            SingleProduct = await _product.GetProduct(id);
        }


        public async Task<IActionResult> OnPost(int id)
        {

            string urlAnterior = Request.Headers["Referer"].ToString();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Auth/Index");

            }
            var cart = await _cart.GetCart(user.Id);
            if (ModelState.IsValid)
            {
                var cartItem = new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = id,
                    Quantity = Input.Quantity
                };
                if (await _cart.GetCartProductByProductIdForUser(user.Id, id) != null)
                {
                    CartProduct existingCartItem = await _cart.GetCartProductByProductIdForUser(user.Id, id);
                    existingCartItem.Quantity += Input.Quantity;
                    await _cart.UpdateCartProduct(existingCartItem);
                }
                else
                {
                    await _cart.CreateCartProduct(cartItem);
                }
            }
            return Redirect(urlAnterior);
        }

        public class ProductInput
        {
            public int Quantity { get; set; } = 1;
        }
    }
}

