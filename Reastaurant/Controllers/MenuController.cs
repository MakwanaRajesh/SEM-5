using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reastaurant.Models;
using Reastaurant.Models.ViewModels;

namespace Reastaurant.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Menu()
        {
            var menuItems = _context.MenuItems.ToList(); // Example
            if (menuItems == null)
            {
                // Optional: handle null from DB
                return View(new List<MenuItem>());
            }
            return View(menuItems);
        }

    }
}
