using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class OrderService : IOrderService
    {
        private readonly PetMarketDbContext _context;

        public OrderService(PetMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PlaceOrderAsync(string userId, Address address, string paymentMethod, string shippingMethodName)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.ShoppingCartItem)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.ShoppingCartItem.Any()) return null;

            decimal shippingCost = 0;
            string description = "";
            
            const decimal FreeShippingThreshold = 200;

           
            decimal productsTotal = cart.ShoppingCartItem.Sum(i => i.Quantity * i.Product.PriceProduct);

            if (shippingMethodName == "Standard")
            {
                shippingCost = 15.00m;
                description = "Livrare prin curier rapid (2-3 zile)";
            }
            else if (shippingMethodName == "Express")
            {
                shippingCost = 25.00m;
                description = "Livrare prioritară (24h)";
            }
            else if (shippingMethodName == "Ridicare")
            {
                shippingCost = 0.00m;
                description = "Ridicare personală din sediu";
            }

            
            if (productsTotal >= FreeShippingThreshold && shippingMethodName != "Ridicare")
            {
                shippingCost = 0;
                description += " (Transport Gratuit)";
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Address = address,
                TotalAmount = productsTotal + shippingCost, 
                OrderDetails = cart.ShoppingCartItem.Select(item => new OrderDetails
                {
                    IdProduct = item.IdProduct,
                    Quantity = item.Quantity,
                    PriceAtPurchase = item.Product.PriceProduct,
                    Subtotal = item.Quantity * item.Product.PriceProduct
                }).ToList()
            };

            order.Payment = new Payment
            {
                PaymentMethod = paymentMethod,
                Amount = order.TotalAmount,
                PaymentDate = DateTime.UtcNow
            };

            var shipping = new ShippingMethod
            {
                MethodName = shippingMethodName,
                Description = description,
                Cost = shippingCost, 
                FreeShippingThreshold = (int)FreeShippingThreshold
            };
            order.ShippingMethod = new List<ShippingMethod> { shipping };

            _context.Orders.Add(order);
            _context.ShoppingCartItems.RemoveRange(cart.ShoppingCartItem);

            await _context.SaveChangesAsync();
            return order;
        }
    }
}