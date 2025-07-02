using Microsoft.AspNetCore.Mvc;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class RegisterController : Controller
    {
        public IActionResult RegIn()
        {
            return View();
        }
    }
}
