namespace E_commerce_2.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set;}
        public int Count { get; set;}

        // Navigation Proparites
        public List<CartProduct>? cartProducts { get; set; } 
    }
}
