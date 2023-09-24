using E_commerce_2.Models;
using E_commerce_2.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_commerce_2.Pages.Orders
{

    [Authorize(Roles = "Administrator")]
    public class DetailModel : PageModel
    {
        private readonly IOrder _order;

        public DetailModel(IOrder order)
        {
            _order = order;
        }

        public IEnumerable<OrderProduct> OrderItems { get; set; }

        public async Task OnGetAsync(int id) 
        {
            OrderItems = await _order.GetOrderProductsByOrderId(id);
        }
    }
}
