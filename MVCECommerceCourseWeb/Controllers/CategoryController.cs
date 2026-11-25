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

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePOST(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
