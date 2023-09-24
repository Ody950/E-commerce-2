using E_commerce_2.Models.DTO;

namespace E_commerce_2.Models.Interface
{
    public interface IOrder
    {

        Task<Order> CreateOrder(Order order);
        Task<OrderProduct> CreateOrderProduct(OrderProduct orderProducts);
        Task<Order> GetLatestOrderForUser(string userId);
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        Task<IEnumerable<Order>> GetOrders();
        Task<IList<OrderProduct>> GetOrderProductsByOrderId(int orderId);
        Task<IEnumerable<OrderProduct>> GetOrderProducts();
        Task<OrderProduct> UpdateOrderProducts(OrderProduct orderProducts);





    }
}
