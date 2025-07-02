using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult MenuList()
        {
            List<MenuItem> items = new List<MenuItem>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"SELECT mi.MenuItemId, mi.Name, mi.Description, mi.Price, mi.ImagePath, c.CategoryName 
                             FROM MenuItems mi
                             JOIN Categories c ON mi.CategoryId = c.CategoryId";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new MenuItem
                    {
                        MenuItemId = (int)reader["MenuItemId"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"],
                        ImagePath = reader["ImagePath"].ToString(),
                        //CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }

            return View(items);
        }

        [HttpGet]
        public IActionResult AddMenuItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMenuItem(MenuItem item)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"INSERT INTO MenuItems (Name, Description, Price, ImagePath, CategoryId)
                             VALUES (@Name, @Description, @Price, @ImagePath, @CategoryId)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@ImagePath", item.ImagePath);
                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("MenuList");
        }

        // TODO: Add Edit & Delete
        [HttpGet]
        public IActionResult EditMenuItem(int id)
        {
            MenuItem item = new MenuItem();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT * FROM MenuItems WHERE MenuItemId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    item.MenuItemId = (int)reader["MenuItemId"];
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.Price = (decimal)reader["Price"];
                    item.ImagePath = reader["ImagePath"].ToString();
                    item.CategoryId = (int)reader["CategoryId"];
                }
            }

            return View(item);
        }

        [HttpPost]
        public IActionResult EditMenuItem(MenuItem item)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"UPDATE MenuItems 
                         SET Name = @Name, Description = @Description, Price = @Price, 
                             ImagePath = @ImagePath, CategoryId = @CategoryId 
                         WHERE MenuItemId = @MenuItemId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MenuItemId", item.MenuItemId);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@ImagePath", item.ImagePath);
                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("MenuList");
        }
        [HttpPost]
        public IActionResult DeleteMenuItem(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "DELETE FROM MenuItems WHERE MenuItemId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("MenuList");
        }


    }
}
