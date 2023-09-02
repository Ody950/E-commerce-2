namespace E_commerce_2.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order? order { get; set; }
        public Product? product { get; set; }

    }
}
