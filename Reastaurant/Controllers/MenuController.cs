using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reastaurant.Models;
using Microsoft.Data.SqlClient;
using Reastaurant.Models.ViewModels;

namespace Reastaurant.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }
    }
}