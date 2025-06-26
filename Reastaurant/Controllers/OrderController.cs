using Microsoft.AspNetCore.Mvc;

namespace Reastaurant.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Order()
        {
            return View();
        }
    }
}
