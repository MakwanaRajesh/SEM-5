using Microsoft.AspNetCore.Mvc;

namespace Reastaurant.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Before_Login_About()
        {
            return View();
            // This returns: Views/About/Before_Login_About.cshtml
        }

        public IActionResult After_Login_About()
        {
            return View();
            // This returns: Views/About/After_Login_About.cshtml
        }
    }
}
