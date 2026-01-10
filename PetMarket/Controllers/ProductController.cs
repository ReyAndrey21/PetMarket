using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Threading.Tasks;

namespace PetMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/products")]
    public class ProductController : Controller
    {
        
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListProduct(string? searchTerm)
        {
            ViewData["Title"] = "Manage Products";

            ViewData["CurrentFilter"] = searchTerm;
            var products = await _productService.GetAllProductsAsync(searchTerm);
            return View(products);
        }


        [HttpGet("create")]
        public async Task<IActionResult> AddProduct()
        {
            ViewData["Title"] = "Add Product";
            ViewBag.Categories = await _productService.GetAvailableCategoriesAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _productService.GetAvailableCategoriesAsync();
                return View(product);
            }

            
            if (await _productService.CreateProductAsync(product, product.ImageProduct))
            {
                return RedirectToAction(nameof(ListProduct));
            }

            ModelState.AddModelError(string.Empty, "Eroare la crearea produsului.");
            ViewBag.Categories = await _productService.GetAvailableCategoriesAsync();
            return View(product);
        }

        
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);
            if (product == null) return NotFound();

            ViewData["Title"] = "Edit Product";
            ViewBag.Categories = await _productService.GetAvailableCategoriesAsync();
            return View(product);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditProduct(int id, Product updatedProduct)
        {
            if (id != updatedProduct.IdProduct) return BadRequest();

            if (ModelState.IsValid)
            {
                
                if (await _productService.UpdateProductAsync(updatedProduct, updatedProduct.ImageProduct))
                {
                    return RedirectToAction(nameof(ListProduct));
                }
                ModelState.AddModelError(string.Empty, "A apărut o eroare la actualizarea produsului.");
            }

            ViewBag.Categories = await _productService.GetAvailableCategoriesAsync();
            return View(updatedProduct);
        }

        
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);
            if (product == null) return NotFound();

            ViewData["Title"] = "Delete Product";
            return View(product);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmedProduct(int id)
        {
            
            if (await _productService.DeleteProductAsync(id))
            {
                return RedirectToAction(nameof(ListProduct));
            }

            return NotFound();
        }
    }
}