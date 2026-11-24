using Microsoft.AspNetCore.Mvc;

namespace MVCECommerceCourseWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
