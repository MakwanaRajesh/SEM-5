using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: /Order/AddToBasket
        [HttpPost]
        public IActionResult AddToBasket(int menuItemId, int quantity)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            int userId = GetUserIdByEmail(userEmail);

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("AddOrder", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Basket");
        }

        // GET: /Order/Basket
        public IActionResult Basket()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            int userId = GetUserIdByEmail(userEmail);
            List<OrderDetail> items = new List<OrderDetail>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"
                SELECT od.OrderDetailId, od.Quantity, mi.Name, mi.Price
                FROM Orders o
                JOIN OrderDetails od ON o.OrderId = od.OrderId
                JOIN MenuItems mi ON od.MenuItemId = mi.MenuItemId
                WHERE o.UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new OrderDetail
                    {
                        OrderDetailId = (int)reader["OrderDetailId"],
                        Quantity = (int)reader["Quantity"],
                        MenuItem = new MenuItem
                        {
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"]
                        }
                    });
                }
            }

            return View(items);
        }

        // POST: /Order/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int orderDetailId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("UpdateOrderDetail", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Basket");
        }

        // POST: /Order/DeleteItem
        [HttpPost]
        public IActionResult DeleteItem(int orderDetailId)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("DeleteOrderDetail", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Basket");
        }

        private int GetUserIdByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("SELECT UserId FROM Users WHERE Email = @Email", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login", "Account");

            int userId = GetUserIdByEmail(email);

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "UPDATE Orders SET IsPlaced = 1 WHERE UserId = @UserId AND IsPlaced = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["OrderSuccess"] = "Order placed successfully!";
            return RedirectToAction("Basket");
        }

        public IActionResult UserOrders()
        {
            string email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login", "Account");

            int userId = GetUserIdByEmail(email);
            List<OrderDetail> orders = new List<OrderDetail>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"
            SELECT od.OrderDetailId, od.Quantity, mi.Name, mi.Price, o.OrderId
            FROM Orders o
            JOIN OrderDetails od ON o.OrderId = od.OrderId
            JOIN MenuItems mi ON od.MenuItemId = mi.MenuItemId
            WHERE o.UserId = @UserId AND o.IsPlaced = 1
            ORDER BY o.OrderId DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderDetail
                    {
                        OrderDetailId = (int)reader["OrderDetailId"],
                        Quantity = (int)reader["Quantity"],
                        MenuItem = new MenuItem
                        {
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"]
                        }
                    });
                }
            }

            return View(orders);
        }


    }
}
