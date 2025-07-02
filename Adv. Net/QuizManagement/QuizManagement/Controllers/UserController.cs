using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagement.Models;
using static QuizManagement.Models.UserModel;
using Microsoft.Extensions.Configuration;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class UserController : Controller
    {
        private IConfiguration configuration;

        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult UserList()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_User_SelectAll";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return View(table);
                    }
                }
            }
        }

        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_User_Delete";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "table QuizList deleted successfully.";
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Quiz: " + ex.Message;
                return RedirectToAction("UserList");
            }
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        public IActionResult UserAddEdit(int UserID)
        {
            UserDropDown();
            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection Connection = new SqlConnection(connectionString);
            Connection.Open();
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_User_SelectByID";
            command.Parameters.AddWithValue("@QuizID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            UserModel model = new UserModel();
            foreach (DataRow row in datatable.Rows)
            {
                model.UserName = @row["UserName"].ToString();
                model.Password = Convert.ToString(@row["Password"]);
                model.Email = Convert.ToString(@row["Email"]);
                model.Mobile = Convert.ToString(@row["Mobile"]);
                model.IsActive = (byte)@row["IsActive"];
                model.IsAdmin = (byte)@row["IsAdmin"];
                model.UserID = Convert.ToInt32(@row["UserID"]);
            }
            return View("QuizAddEdit", model);
        }

        public IActionResult UserSave(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                UserDropDown();
                string connectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (userModel.UserID == null)
                {
                    command.CommandText = "PR_MST_User_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_User_Update";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = userModel.UserID;
                }
                command.Parameters.Add("@Username", SqlDbType.VarChar).Value = userModel.UserName;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = userModel.Password;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = userModel.Email;
                command.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = userModel.Mobile;
                //command.Parameters.Add("@UserID", SqlDbType.Int).Value = userModel.UserID;
                //command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userModel.IsActive;
                //command.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = userModel.IsAdmin;
                command.ExecuteNonQuery();
                return RedirectToAction("UserList");
            }
            UserDropDown();
            return View("UserAdd", userModel);
        }
        public void UserDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "PR_MST_User_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<UserDropdownModel> userList = new List<UserDropdownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                UserDropdownModel userDropDown = new UserDropdownModel();
                userDropDown.UserID = Convert.ToInt32(data["UserID"]);
                userDropDown.UserName = data["UserName"].ToString();
                userList.Add(userDropDown);
            }
            ViewBag.UserList = userList;
        }

        //public class LoginController : Controller
        //{
        //    private readonly IConfiguration _configuration;

        //    public LoginController(IConfiguration configuration)
        //    {
        //        _configuration = configuration;
        //    }

        //    [HttpPost]
        //    public IActionResult LogIn(UserModel userLoginModel)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(userLoginModel);
        //        }

        //        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ABConnectionString")))
        //        {
        //            conn.Open();
        //            using (SqlCommand objCmd = new SqlCommand("PR_MST_User_Login", conn))
        //            {
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
        //                objCmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

        //                using (SqlDataReader objSDR = objCmd.ExecuteReader())
        //                {
        //                    if (!objSDR.HasRows)
        //                    {
        //                        ModelState.AddModelError(string.Empty, "Invalid Username or Password");
        //                        return View(userLoginModel);
        //                    }

        //                    DataTable dtLogin = new DataTable();
        //                    dtLogin.Load(objSDR);

        //                    // Load session data
        //                    foreach (DataRow dr in dtLogin.Rows)
        //                    {
        //                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
        //                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
        //                        HttpContext.Session.SetString("MobileNo", dr["MobileNo"].ToString());
        //                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
        //                    }

        //                    return RedirectToAction("Index", "Home");
        //                }
        //            }
        //        }
        //    }
        //}

        [HttpPost]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }
        }

    }
