using E_commerce_2.Models.DTO;

namespace E_commerce_2.Models.Interface
{
    public interface ICart
    {
       
        Task<List<CartDTO>> GetCarts();
        Task<CartDTO> GetCart(int id);
        Task<CartDTO> Create(CartDTO cart);
        Task<CartDTO> UpdateCart(int id, CartDTO cart);
       
        Task Delete(int id);

        Task<Product> AddProductToCart(int CartId, Product product);

        Task deleteProductFromCart(int CartId, int productId);
    }
}
