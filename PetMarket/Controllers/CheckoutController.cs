using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class CheckoutController : Controller
{
    private readonly PetMarketDbContext _context;
    private readonly IOrderService _orderService;

    public CheckoutController(PetMarketDbContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cart = await _context.ShoppingCarts
            .Include(c => c.ShoppingCartItem)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.ShoppingCartItem.Any())
            return RedirectToAction("Index", "ShoppingCart");

        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Address address, string paymentMethod, string shippingMethodName)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _orderService.PlaceOrderAsync(userId, address, paymentMethod, shippingMethodName);

        if (order != null) return View("OrderSucces", order);
        return RedirectToAction("Index");
    }
}