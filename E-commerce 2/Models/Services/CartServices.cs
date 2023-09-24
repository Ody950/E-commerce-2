using E_commerce_2.Data;
using E_commerce_2.Models.DTO;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Web.Helpers;

namespace E_commerce_2.Models.Services
{
    public class CartServices : ICart
    {
        private readonly TheMarketDBContext _context;

        public CartServices(TheMarketDBContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductToCart(int CartId, Product product)
        {
            _context.Entry(product).State = EntityState.Added;

            await _context.SaveChangesAsync();

            CartProduct cartProduct = new CartProduct()
            {
                ProductId = product.Id,
                CartId = CartId
            };

            _context.Entry(cartProduct).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Cart> Create(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<CartProduct> CreateCartProduct(CartProduct cartProduct) 
        {
            _context.Entry(cartProduct).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return cartProduct;
        }

        public async Task Delete(int id)
        {
            Cart existingDoc = await _context.Carts.FindAsync(id);
            if (existingDoc != null)
            {
                _context.Entry(existingDoc).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException("The Cart Id is not exist.");
            }
        }

        public async Task deleteProductFromCart(int CartId, int productId)
        {
            Product productCart = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (productCart != null)
            {
                _context.Entry(productCart).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCart(string userId) 
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(cart => cart.UserId == userId);
            if (cart == null)
            {
                throw new InvalidOperationException($"Cart with UserId {userId} not found.");
            }
            
            return cart;
        }

        public async Task<IEnumerable<CartProduct>> GetCartProductByUserId(string userId) 
        {
            Cart cart = await GetCart(userId);
            return _context.CartsProducts.Where(cartItem => cartItem.CartId == cart.Id).Include(x => x.product);
        }




        public async Task<CartProduct> GetCartProductByProductIdForUser(string userId, int productId) 
        {
            var cartProduct = await GetCartProductByUserId(userId);
            return cartProduct.FirstOrDefault(cart => cart.ProductId == productId);
        }


        public async Task<CartProduct> UpdateCartProduct(CartProduct cartProduct)
        {
            var updateCartProduct = new CartProduct
            {
                CartId = cartProduct.CartId,
                ProductId = cartProduct.ProductId,
                Quantity = cartProduct.Quantity
            };
            _context.Entry(cartProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateCartProduct;

        }

        public async Task RemoveCartProduct(string userId, int productId) 
        {
            CartProduct cartItem = await GetCartProductByProductIdForUser(userId, productId);
            _context.CartsProducts.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartProducts(IEnumerable<CartProduct> cartProduct) 
        {
            _context.CartsProducts.RemoveRange(cartProduct);
            await _context.SaveChangesAsync();
        }
        public async Task<List<CartDTO>> GetCarts()
        {
            var carts = await _context.Carts
                            .Select(x => new CartDTO
                            {
                                Id = x.Id,
                                UserId = x.UserId,
                                TotalPrice = x.TotalPrice,
                                Count = x.Count,
                            })
                            .ToListAsync();

            return carts;
        }

    }
}
