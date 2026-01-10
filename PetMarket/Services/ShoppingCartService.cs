using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<ShoppingCart> GetCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, ShoppingCartItem = new List<ShoppingCartItem>() };
                await _cartRepository.AddAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }
            return cart;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cart = await GetCartAsync(userId);
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null || product.StockProduct < quantity) return;

            var existingItem = cart.ShoppingCartItem.FirstOrDefault(i => i.IdProduct == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.ShoppingCartItem.Add(new ShoppingCartItem
                {
                    IdProduct = productId,
                    Quantity = quantity
                });
            }

            await _cartRepository.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return;

            var item = cart.ShoppingCartItem.FirstOrDefault(i => i.IdProduct == productId);
            if (item != null)
            {
                await _cartRepository.RemoveItemAsync(item);
                await _cartRepository.SaveChangesAsync();
            }
        }
    }
}