using E_commerce_2.Auth.Models;
using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_2.Pages.Receipt
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmail _email;
        private readonly ICart _cart;
        private readonly IOrder _order;

        public IndexModel(UserManager<ApplicationUser> userManager, IEmail emailSender, ICart cart, IOrder order)
        {
            _userManager = userManager;
            _email = emailSender;
            _cart = cart;
            _order = order;
        }
        [BindProperty]

        public ReceiptInput Input { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);

                Order order = new Order
                {
                    UserID = user.Id,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Address = Input.Address,
                    City = Input.City,
                    State = Input.State,
                    Zip = Input.Zip,
                    Timestamp = DateTime.Now.ToString()
                };

                await _order.CreateOrder(order);

                order = await _order.GetLatestOrderForUser(user.Id);

                IEnumerable<CartProduct> cartItems = await _cart.GetCartProductByUserId(user.Id);
                IList<OrderProduct> orderProducts = new List<OrderProduct>();
                double total = 0;

                foreach (var cartItem in cartItems)
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        OrderID = order.Id,
                        ProductID = cartItem.ProductId,
                        Quantity = cartItem.Quantity
                    };
                    orderProducts.Add(orderProduct);
                    total += cartItem.product.Price * cartItem.Quantity;
                }

                double finalCost = total * 1.1;

                string subject = "Purhcase Summary From Cosmetic Store!";
                string message =
                    $"<p>Hello {user.UserName},</p>" +
                    $"<p>&nbsp;</p>" +
                    $"<p>Below is your recent purchase summary</p>" +
                    $"<p>Total: ${finalCost.ToString("F")}\n</p>" + "<a href=\"https://e-commerce2.azurewebsites.net/\">Click here to shop more!<a>";

                await _email.SendEmailAsync(user.Email, subject, message);
                foreach (var item in orderProducts)
                {
                    await _order.CreateOrderProduct(item);
                }
                await _cart.RemoveCartProducts(cartItems);

                return RedirectToAction("");
            }
            return Page();
        }
        public class ReceiptInput
        {
            [Display(Name = "Purchased Date:")]
            public DateTime Date { get; set; }

            [Required]
            [Display(Name = "First Name:")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name:")]
            public string LastName { get; set; }

            [Required]
            public string Address { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string State { get; set; }

            [Required]
            [DataType(DataType.PostalCode)]
            [Compare("Zip", ErrorMessage = "The is an invalid zip code")]
            public string Zip { get; set; }
        }

    }
}
