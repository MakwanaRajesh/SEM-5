using Microsoft.AspNetCore.Mvc;

namespace Reastaurant.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Category()
        {
            return View();
        }
    }
}
