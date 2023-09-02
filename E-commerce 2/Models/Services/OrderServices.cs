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
        public async Task<Product> AddProductToOrder(int OrderId, Product product)
        {
            _context.Entry(product).State = EntityState.Added;

            await _context.SaveChangesAsync();

            OrderProduct orderProduct = new OrderProduct()
            {
                ProductId = product.Id,
                OrderId = OrderId
            };

            _context.Entry(orderProduct).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
            Order newOrder = new Order()
            {

                Id = orderDTO.Id,
                UserId = orderDTO.UserId,
                TotalPrice = orderDTO.TotalPrice,
                FirstName = orderDTO.FirstName,
                LastName = orderDTO.LastName,
                Address = orderDTO.Address,
                City = orderDTO.City,
                Stat = orderDTO.Stat,
                Zip = orderDTO.Zip,
                Timestamp = orderDTO.Timestamp,

            };
            _context.Entry(newOrder).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return orderDTO;
        }

        public async Task Delete(int id)
        {
            Order existingDoc = await _context.Orders.FindAsync(id);
            if (existingDoc != null)
            {
                _context.Entry(existingDoc).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException("The Order Id is not exist.");
            }
        }

        public async Task deleteProductFromOrder(int OrderId, int productId)
        {
            Product productOrder = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (productOrder != null)
            {
                _context.Entry(productOrder).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDTO>> GetOrderByUserID(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDTO>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTO> UpdateOrder(int id, OrderDTO order)
        {
            throw new NotImplementedException();
        }
    }
}
