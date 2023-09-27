using E_commerce_2.Auth.Models;
using E_commerce_2.Auth.Models.Interface;
using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Exchange.WebServices.Data;
using Stripe;
using Stripe.Checkout;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace E_commerce_2.Pages.Receipt
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmail _email;
        private readonly ICart _cart;
        private readonly IOrder _order;
        private readonly IConfiguration _configuration;
        public IndexModel(UserManager<ApplicationUser> userManager, IEmail emailSender, ICart cart, IOrder order, IConfiguration configuration)
        {
            _userManager = userManager;
            _email = emailSender;
            _cart = cart;
            _order = order;
            _configuration = configuration;
        }
        [BindProperty]

        public ReceiptInput Input { get; set; }

        [BindProperty]
        public IEnumerable<CartProduct> CartProduct { get; set; }




        public async Task<IActionResult> OnPostAsync()
        {
            try
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


                StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings:SecretKey").Get<string>();

                var domain = "https://localhost:7048/";

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + "Receipt/OrderConfirmation",
                    CancelUrl = domain + "TheCart/Index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };
                foreach (var item in cartItems)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            UnitAmount = (long)(item.product.Price * 100), // 20.50 => 2050
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name = item.product.Name
                            }
                        },
                        Quantity = item.Quantity
                    };

                    options.LineItems.Add(sessionLineItem);
                }
                bool isCompleted = true;
                TempData["IsCompleted"] = isCompleted;
                var service = new SessionService();
                var session = service.Create(options);

                var sessionId = session.Id;

                TempData["sessionId"] = sessionId;


                Response.Headers.Add("Location", session.Url);


                return new StatusCodeResult(303);
            }
            catch (Exception ex)
            {
                TempData["IsCompleted"] = false; 
                return Page(); 
            }


        }
    

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToPage("/Error");
                }

                CartProduct = await _cart.GetCartProductByUserId(user.Id);

                if (CartProduct == null)
                {
                    return RedirectToPage("/Error");
                }

                return Page();
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }
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
