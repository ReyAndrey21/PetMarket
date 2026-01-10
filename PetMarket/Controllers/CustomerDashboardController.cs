
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Services.Interfaces;

namespace PetMarket.Controllers
{

    public class CustomerDashboardController : Controller
    {
        private readonly PetMarketDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService; 
        private readonly IPetService _petService;



        public CustomerDashboardController(PetMarketDbContext context, ICategoryService categoryService, IProductService productService, IPetService petService)
        {
            _context = context;
            _categoryService = categoryService;
            _productService = productService;
            _petService = petService;
        }


        [HttpGet("")]
        [HttpGet("/home")]
        public async Task<IActionResult> HomePage()
        {
            ViewData["Title"] = "Home Page";
            var pets = await _petService.GetAllPetsAsync();
            return View(pets);
        }

        
        [HttpGet("/products")]
        [HttpGet("/products/{id}")] 
        public async Task<IActionResult> Products(int? id)
        {
            IEnumerable<Product> products;
            string title;

            if (id.HasValue)
            {
                
                products = await _productService.GetProductsByCategoryIdAsync(id.Value);

                
                var category = await _categoryService.GetCategoryWithSubcategoriesAsync(id.Value);

                if (category != null)
                {
                    title = $"Products in {category.NameCategory}";
                }
                else
                {
                    title = "Products - Category Not Found";
                }
            }
            else
            {
                
                products = await _productService.GetAllProductsAsync();
                title = "All Products";
            }

            ViewData["Title"] = title; 

            return View(products);
        }



        [HttpGet("/categories")]
        [HttpGet("/categories/{id}")]
        public async Task<IActionResult> Categories(int? id)
        {
            IEnumerable<Category> categoriesToDisplay;
            string title;
            int? parentIdForBackLink = null;

            if (id == null)
            {

                categoriesToDisplay = await _categoryService.GetTopLevelCategoriesForCustomerAsync();
                title = "Main Categories";
            }
            else
            {

                var parentCategory = await _categoryService.GetCategoryWithSubcategoriesAsync(id.Value);

                if (parentCategory == null)
                {

                    categoriesToDisplay = new List<Category>();
                    title = "Category Not Found";
                }
                else
                {
                    categoriesToDisplay = parentCategory.Subcategories;
                    title = parentCategory.NameCategory;

                    parentIdForBackLink = parentCategory.ParentCategoryId;
                }
            }

            ViewData["Title"] = title;
            ViewData["ParentCategoryId"] = parentIdForBackLink;

            return View(categoriesToDisplay);
        }

        [HttpGet("/products/details")]
        public async Task<IActionResult> ViewDetailsProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Review) 
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.IdProduct == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}