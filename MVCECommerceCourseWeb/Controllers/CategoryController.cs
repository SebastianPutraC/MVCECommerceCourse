using Microsoft.AspNetCore.Mvc;
using MVCECommerceCourseWeb.Data;
using MVCECommerceCourseWeb.Models;

namespace MVCECommerceCourseWeb.Controllers
{
    public class CategoryController : Controller
    {
        //Since ApplicationDbContext is already created, no need to make a new one and just get the context
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            return View("Index", categories); //Passing categories to be used on Views

            //return View("Test");  Just for example of trying to find the "Test" file on the Views instead of "Index"
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult CreatePOST(Category category)
        {
            //Check Custom error and Display error message for specific errors outside of DB requirement
            if (!String.IsNullOrEmpty(category.Name)
                && _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("", "Category name already exists!");
            }

            //Default error by checking on the DB requirement
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
