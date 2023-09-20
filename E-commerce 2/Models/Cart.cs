using E_commerce_2.Models.DTO;

namespace E_commerce_2.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set;}
        public int Count { get; set;}

        // Navigation Proparites
        public List<CartProduct>? cartProducts { get; set; }

        public static implicit operator Cart(CartDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
