using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers
{
    public class MenuController : Controller
    {
        private readonly IConfiguration _configuration;

        public MenuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            List<MenuItem> menuItems = new List<MenuItem>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                // Get categories
                SqlCommand categoryCmd = new SqlCommand("SELECT * FROM Categories", conn);
                SqlDataReader catReader = categoryCmd.ExecuteReader();
                while (catReader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryId = (int)catReader["CategoryId"],
                        CategoryName = catReader["CategoryName"].ToString()
                    });
                }
                catReader.Close();

                // Get menu items
                SqlCommand itemCmd = new SqlCommand("SELECT * FROM MenuItems", conn);
                SqlDataReader itemReader = itemCmd.ExecuteReader();
                while (itemReader.Read())
                {
                    menuItems.Add(new MenuItem
                    {
                        MenuItemId = (int)itemReader["MenuItemId"],
                        Name = itemReader["Name"].ToString(),
                        Description = itemReader["Description"].ToString(),
                        Price = (decimal)itemReader["Price"],
                        ImagePath = itemReader["ImagePath"].ToString(),
                        CategoryId = (int)itemReader["CategoryId"]
                    });
                }
            }

            ViewBag.Categories = categories;
            return View(menuItems);
        }
    }
}
