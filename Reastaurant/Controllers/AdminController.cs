using Microsoft.AspNetCore.Mvc;

namespace Reastaurant.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
