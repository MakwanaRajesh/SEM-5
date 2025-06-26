using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Reastaurant.Models;

namespace Reastaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashBoard()
        {
            return View(DashBoard);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Menu()
        //{
        //               // This action can be used to display the menu items
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult LogIn()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult SignUp(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Save user to database (you can add logic here)
        //        // Example: _context.Users.Add(user); _context.SaveChanges();

        //        TempData["Message"] = "User registered successfully!";
        //        return RedirectToAction("SignUp");
        //    }
        //    return View(user);
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
