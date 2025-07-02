using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AddressBook.Models;

namespace AddressBook.Controllers
{   
    public class CityController : Controller
    {
        private IConfiguration configuration;
        public CityController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult CityList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Pr_City_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult CityDelete(int CityId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Pr_City_Delete";
                    command.Parameters.Add("@CityId", SqlDbType.Int).Value = CityId;
                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "City deleted successfully.";
                return RedirectToAction("CityList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the city: " + ex.Message;
                return RedirectToAction("CityList");
            }
        }
        public IActionResult AddCity(int? CityId)
        {
            StateDropDown();
            CountryDropDown();
            UserDropDown();
            if (CityId == null)
            {
                return View();
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_City_SelectByID";
            command.Parameters.AddWithValue("@CityId", CityId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            CityModel model = new CityModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.CountryId = Convert.ToInt32(dataRow["CountryId"]);
                model.StateId = Convert.ToInt32(dataRow["StateId"]);
                model.CityName = dataRow["CityName"].ToString();
                model.StdCode = dataRow["StdCode"].ToString();
                model.PinCode = dataRow["PinCode"].ToString();
                model.UserId = Convert.ToInt32(dataRow["UserId"]);
            }
            return View(model);
        }
        public void StateDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_State_Dropdown";
            //command2.Parameters.Add("@UserId", SqlDbType.Int).Value = CommonVariable.UserId();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<StateDropDownModel> stateList = new List<StateDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                StateDropDownModel model = new StateDropDownModel();
                model.StateId = Convert.ToInt32(data["StateId"]);
                model.StateName = data["StateName"].ToString();
                stateList.Add(model);
            }
            ViewBag.StateList = stateList;
        }
        public void CountryDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_Country_Dropdown";
            //command2.Parameters.Add("@UserId", SqlDbType.Int).Value = CommonVariable.UserId();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<CountryDropDownModel> countryList = new List<CountryDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                CountryDropDownModel model = new CountryDropDownModel();
                model.CountryId = Convert.ToInt32(data["CountryId"]);
                model.CountryName = data["CountryName"].ToString();
                countryList.Add(model);
            }
            ViewBag.CountryList = countryList;
        }
        public void UserDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_User_Dropdown";
            //command2.Parameters.Add("@UserId", SqlDbType.Int).Value = CommonVariable.UserId();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<UserDropDownModel> userList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                UserDropDownModel model = new UserDropDownModel();
                model.UserId = Convert.ToInt32(data["UserId"]);
                model.UserName = data["UserName"].ToString();
                userList.Add(model);
            }
            ViewBag.UserList = userList;
        }
        [HttpPost]
        public IActionResult CitySave(CityModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.CityId == 0 || model.CityId == null)
                {
                    command.CommandText = "PR_City_Insert";
                }
                else
                {
                    command.CommandText = "PR_City_Update";
                    command.Parameters.Add("@CityId", SqlDbType.Int).Value = model.CityId;
                }

                command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = model.CityName;
                command.Parameters.Add("@CountryId", SqlDbType.VarChar).Value = model.CountryId;
                command.Parameters.Add("@StateId", SqlDbType.VarChar).Value = model.StateId;
                command.Parameters.Add("@StdCode", SqlDbType.VarChar).Value = model.StdCode;
                command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = model.PinCode;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserId;
                command.ExecuteNonQuery();
                return RedirectToAction("CityList");
            }
            StateDropDown();
            CountryDropDown();
            UserDropDown();
            return View("AddCity", model);
        }
        //public IActionResult CountAll()
        //{
        //    string connectionstr = _configuration.GetConnectionString("ConnectionString");

        //    using (SqlConnection conn = new SqlConnection(connectionstr))
        //    {
        //        conn.Open();
        //        using (SqlCommand objCmd = conn.CreateCommand())
        //        {
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.CommandText = "PR_LOC_CountOfAll";

        //            SqlDataReader objSDR = objCmd.ExecuteReader();
        //            if (objSDR.Read())
        //            {
        //                ViewData["CountCity"] = Convert.ToInt32(objSDR["CountCity"]);
        //                ViewData["CountState"] = Convert.ToInt32(objSDR["CountState"]);
        //                ViewData["CountCountry"] = Convert.ToInt32(objSDR["CountCountry"]);
        //            }
        //            conn.Close();
        //        }
        //    }

        //    return View();
        //}
        public IActionResult CountAll()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_CountOfAll";
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ViewData["CountCity"] = Convert.ToInt32(reader["CountCity"]);
                ViewData["CountState"] = Convert.ToInt32(reader["CountState"]);
                ViewData["CountCountry"] = Convert.ToInt32(reader["CountCountry"]);
            }
            connection.Close();
            return View();
        }
    }
}
