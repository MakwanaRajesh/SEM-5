using Microsoft.AspNetCore.Mvc;

namespace Reastaurant.Controllers
{
    public class User_DashBoardController : Controller
    {
        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

    }
}
