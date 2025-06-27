using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Reastaurant.Models;
using System.Linq;





using Microsoft.AspNetCore.Mvc;

namespace Reastaurant.Controllers
{
    public class CartController : Controller
    {
        // Use a List<CartItem> — NOT a class named Carts
        public static List<Carts> Cart = new List<Carts>
        {
            new Carts { MenuItemId = 1, ItemName = "Paneer Pizza", ImageUrl = "/images/pizza.jpg", Price = 299, Quantity = 1 },
            new Carts { MenuItemId = 2, ItemName = "Veg Biryani", ImageUrl = "/images/biryani.jpg", Price = 199, Quantity = 2 }
        };

        public IActionResult Carts()
        {
            return View(); // Pass List<CartItem> to View
        }

        public IActionResult Remove(int id)
        {
            var item = Cart.FirstOrDefault(c => c.MenuItemId == id);
            if (item != null)
                Cart.Remove(item);

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            Cart.Clear();
            return RedirectToAction("Index");
        }
    }
}
