using E_commerce_2.Data;
using E_commerce_2.Models.DTO;
using E_commerce_2.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task<CartDTO> Create(CartDTO newCartDTO)
        {

            Cart newCart = new Cart()
            {

                Id = newCartDTO.Id,
                UserId = newCartDTO.UserId,
                TotalPrice = newCartDTO.TotalPrice,
                Count = newCartDTO.Count,

            };
            _context.Entry(newCart).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newCartDTO;
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

        public async Task<CartDTO> GetCart(int id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(d => d.Id == id);
            if (cart == null)
            {
                throw new InvalidOperationException($"Cart with ID {id} not found.");
            }
            var cartDTO = await _context.Carts.Select(x => new CartDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                TotalPrice = x.TotalPrice,
                Count = x.Count,
            }).FirstOrDefaultAsync(x => x.Id == id);

            return cartDTO;
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

        public async Task<CartDTO> UpdateCart(int id, CartDTO upDateCartDTO)
        {
            var theCart = await _context.Carts.FirstOrDefaultAsync(d => d.Id == id);
            if (theCart == null)
            {
                throw new InvalidOperationException($"Cart with ID {id} not found.");
            }
            Cart updateCart = new Cart
            {
                Id = upDateCartDTO.Id,
                UserId = upDateCartDTO.UserId,
                TotalPrice = upDateCartDTO.TotalPrice,
                Count = upDateCartDTO.Count,
            };
            _context.Entry(updateCart).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return upDateCartDTO;
        }
    }
}
