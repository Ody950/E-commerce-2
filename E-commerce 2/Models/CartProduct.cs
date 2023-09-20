namespace E_commerce_2.Models
{
    public class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart? cart  { get; set; }
        public Product? product { get; set; }
    }
}
