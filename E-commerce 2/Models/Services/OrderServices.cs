using E_commerce_2.Data;
using E_commerce_2.Models.DTO;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_2.Models.Services
{
    public class OrderServices : IOrder
    {
        private readonly TheMarketDBContext _context;

        public OrderServices(TheMarketDBContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return order;
        }
        public async Task<OrderProduct> CreateOrderProduct(OrderProduct orderItem)
        {
            _context.Entry(orderItem).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<Order> GetLatestOrderForUser(string userId)
        {
            var orders = await GetOrdersByUserId(userId);
            return orders.OrderByDescending(order => order.Id).FirstOrDefault();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders.Where(order => order.UserID == userId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<IList<OrderProduct>> GetOrderProductsByOrderId(int orderId)
        {
            return await _context.OrdersProducts.Where(orderItems => orderItems.OrderID == orderId).Include(x => x.Product).ToListAsync();
        }

        public async Task<IEnumerable<OrderProduct>> GetOrderProducts()
        {
            return await _context.OrdersProducts.Include(x => x.Product).ToListAsync();
        }
        public async Task<OrderProduct> UpdateOrderProducts(OrderProduct orderItem)
        {
            var updateOrderItem = new OrderProduct
            {
                OrderID = orderItem.ID,
                ProductID = orderItem.ProductID,
            };
            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateOrderItem;
        }
    }
}
