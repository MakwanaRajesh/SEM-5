using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Reastaurant.Models;
namespace Reastaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        // ========== Log In ==========
        [HttpPost]
        [ActionName("Login")]
        public IActionResult Login(UserLoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Email is required");

            if (string.IsNullOrEmpty(model.Password))
                ModelState.AddModelError("Password", "Password is required");

            if (!ModelState.IsValid)
                return View("Login", model); // Still use the model

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("PR_NewUsers_Login", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.FieldCount == 1 && reader.GetName(0) == "ErrorMessage")
                        {
                            TempData["ErrorMessage"] = reader["ErrorMessage"].ToString();
                            return View("Login", model);
                        }

                        HttpContext.Session.SetString("UserId", reader["UserId"].ToString());
                        HttpContext.Session.SetString("Name", reader["Name"].ToString());
                        HttpContext.Session.SetString("Email", reader["Email"].ToString());
                        HttpContext.Session.SetString("Mobile", reader["Mobile"].ToString());

                        Console.WriteLine("Login success: Redirecting to Dashboard");

                        TempData["SuccessMessage"] = "Session set, redirecting to dashboard.";

                        return RedirectToAction("Dashboard", "User_Dashboard");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong.";
                        return View("Login", model);
                    }
                }
            }
        }




        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
                return View("Register", user); // Use 'user' not 'model'

            string connectionString = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("PR_NewUsers_SignUp", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
                cmd.Parameters.AddWithValue("@Mobile", user.Mobile);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    TempData["SuccessMessage"] = "Registration successful. Please log in.";
                    return RedirectToAction("Login","Account");
                }
                catch (SqlException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View("Register", user);
                }
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }



    }
}
