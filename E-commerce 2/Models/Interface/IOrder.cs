using E_commerce_2.Models.DTO;

namespace E_commerce_2.Models.Interface
{
    public interface IOrder
    {
        Task<List<OrderDTO>> GetOrders();
        Task<Order> GetOrder(int id);
        
        Task<OrderDTO> Create(OrderDTO orderDTO);
       
        Task<OrderDTO> UpdateOrder(int id, OrderDTO order);
      
        Task Delete(int id);

        public Task<List<OrderDTO>> GetOrderByUserID(string UserId);
        Task<Product> AddProductToOrder(int OrderId, Product product);
        Task deleteProductFromOrder(int OrderId, int productId);
    }
}
