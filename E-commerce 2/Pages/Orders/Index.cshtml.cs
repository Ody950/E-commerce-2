using E_commerce_2.Models.Interface;
using E_commerce_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;


namespace E_commerce_2.Pages.Orders
{

    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IOrder _order;

        public IndexModel(IOrder order)  
        {
            _order = order;
        }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderProduct> OrderItems { get; set; }
        public async Task OnGetAsync() 
        {
            Orders = await _order.GetOrders();
            OrderItems = await _order.GetOrderProducts();
        }
    }
}