using Microsoft.AspNetCore.Mvc;
using MVCECommerceCourse.Models;
using MVCECommerceCourse.Data;
using MVCECommerceCourse.Business.Services.IServices;

namespace MVCECommerceCourseWeb.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {
        //Since ApplicationDbContext is already created, no need to make a new one and just get the context
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return View("Index", categories); //Passing categories to be used on Views

            //return View("Test");  Just for example of trying to find the "Test" file on the Views instead of "Index"
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST(Category category)
        {
            //Check Custom error and Display error message for specific errors outside of DB requirement
            if (!String.IsNullOrEmpty(category.Name)
                && !await _categoryService.IsCategoryNameUniqueAsync(category.Name))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }

            //Default error by checking on the DB requirement
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(category);
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePOST(Category category)
        {
            //Check Custom error and Display error message for specific errors outside of DB requirement
            if (!String.IsNullOrEmpty(category.Name)
               && !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }

            //Default error by checking on the DB requirement
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
