using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace PetMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/categories")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListCategory()
        {
            ViewData["Title"] = "Manage Categories";
            var categories = await _categoryService.GetCategoriesForListAsync();
            return View(categories);
        }

        [HttpGet("create")]
        public async Task<IActionResult> AddCategory(int? parentId)
        {
            ViewData["Title"] = "Add Category";

            ViewBag.Categories = await _categoryService.GetParentCategoriesAsync();


            if (parentId.HasValue)
            {
                ViewBag.ParentCategoryId = parentId;
            }

            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {

                if (await _categoryService.CreateCategoryAsync(category))
                {
                    return RedirectToAction(nameof(ListCategory));
                }
                ModelState.AddModelError(string.Empty, "Eroare la crearea categoriei.");
            }


            ViewBag.Categories = await _categoryService.GetParentCategoriesAsync();
            return View(category);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditCategory(int id)
        {

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            ViewData["Title"] = "Edit Category";
            ViewBag.Categories = await _categoryService.GetParentCategoriesAsync();
            return View(category);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditCategory(int id, Category category)
        {
            if (id != category.IdCategory) return BadRequest();

            if (ModelState.IsValid)
            {

                if (await _categoryService.UpdateCategoryAsync(category))
                {
                    return RedirectToAction(nameof(ListCategory));
                }
                ModelState.AddModelError(string.Empty, "Eroare la actualizarea categoriei.");
            }

            ViewBag.Categories = await _categoryService.GetParentCategoriesAsync();
            return View(category);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            var category = await _categoryService.GetCategoryForEditOrDeleteAsync(id);

            if (category == null) return NotFound();

            ViewData["Title"] = "Delete Category";
            return View(category);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmedCategory(int id)
        {

            if (await _categoryService.DeleteCategoryAsync(id))
            {
                return RedirectToAction(nameof(ListCategory));
            }

            return NotFound();
        }


    }
}