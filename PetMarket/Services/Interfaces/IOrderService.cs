using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(string userId, Address address, string paymentMethod, string shippingMethodName);
    }
}