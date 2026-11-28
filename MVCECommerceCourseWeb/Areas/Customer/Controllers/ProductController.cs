using Microsoft.AspNetCore.Mvc;
using MVCECommerceCourse.Models;
using MVCECommerceCourse.Data;
using MVCECommerceCourse.Business.Services.IServices;

namespace MVCECommerceCourseWeb.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        //Since ApplicationDbContext is already created, no need to make a new one and just get the context
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            //Because we use GetAll for product, we dont need to ask for the product again
            //var products = await _productService.GetAllProductsAsync();
            return View("Index"); 
        }

        public async Task<IActionResult> Upsert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> UpsertPOST(Product product)
        {
            //Default error by checking on the DB requirement
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                TempData["success"] = "Product created successfully";

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

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");
        }


        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);

            return Json(new { data = products });
        }
        #endregion
    }
}
