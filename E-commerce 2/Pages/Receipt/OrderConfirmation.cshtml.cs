using E_commerce_2.Auth.Models;
using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using E_commerce_2.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.BillingPortal;

namespace E_commerce_2.Pages.Receipt
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmail _email;
        private readonly ICart _cart;
        private readonly IOrder _order;
        private readonly IConfiguration _configuration;
        public OrderConfirmationModel(UserManager<ApplicationUser> userManager, IEmail emailSender, ICart cart, IOrder order, IConfiguration configuration)
        {
            _userManager = userManager;
            _email = emailSender;
            _cart = cart;
            _order = order;
            _configuration = configuration;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            bool isCompleted = TempData["IsCompleted"] as bool? ?? false;

            if (isCompleted)
            {
                ViewData["OrderStatusMessage"] = "Order completed successfully.";
                ApplicationUser user = await _userManager.GetUserAsync(User);

                IEnumerable<CartProduct> cartItems = await _cart.GetCartProductByUserId(user.Id);
                IList<OrderProduct> orderProducts = new List<OrderProduct>();
                double total = 0;

                foreach (var cartItem in cartItems)
                {
                    total += cartItem.product.Price * cartItem.Quantity;
                }

                double finalCost = total * 1.1;

                string subject = "Purchase Summary From Cosmetic Store!";
                string message =
                    $"Hello {user.UserName}," +
                    $" Below is your recent purchase summary," +
                    $" The Total: ${finalCost.ToString("F")}\n" +
                    "Click here to shop more: https://e-commerce2.azurewebsites.net/";

                await _email.SendEmailAsync(user.Email, subject, message);

                foreach (var item in orderProducts)
                {
                    await _order.CreateOrderProduct(item);
                }

                await _cart.RemoveCartProducts(cartItems);

                return Page();
            }
            else
            {
                ViewData["OrderStatusMessage"] = "Order did not complete successfully.";
            }
            return Page();
        }



    }
}


