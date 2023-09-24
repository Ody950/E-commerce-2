using E_commerce_2.Models.DTO;

namespace E_commerce_2.Models.Interface
{
    public interface ICart
    {
       
        Task<Cart> GetCart(string userId);
        Task<Cart> Create(Cart cart);
        //Task<CartDTO> UpdateCart(int id, CartDTO cart);
        Task<CartProduct> CreateCartProduct(CartProduct cartProduct);
        Task Delete(int id);
        Task<IEnumerable<CartProduct>> GetCartProductByUserId(string userId);
        Task<CartProduct> GetCartProductByProductIdForUser(string userId, int productId);
        Task<CartProduct> UpdateCartProduct(CartProduct cartProduct);

        Task<Product> AddProductToCart(int CartId, Product product);

        Task RemoveCartProduct(string userId, int productId);
        Task RemoveCartProducts(IEnumerable<CartProduct> cartProduct);

    }
}
