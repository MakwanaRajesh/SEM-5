using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Models;


namespace AddressBook.Controllers
{
    public class CountryController : Controller
    {

        private IConfiguration configuration;

        public CountryController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult CountryList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Pr_Country_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult CountryDelete(int CountryId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Pr_Country_Delete";
                    command.Parameters.Add("@CountryId", SqlDbType.Int).Value = CountryId;
                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Country deleted successfully.";
                return RedirectToAction("CountryList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the country: " + ex.Message;
                return RedirectToAction("CountryList");
            }
        }
        public IActionResult AddCountry(int? countryId)
        {
            UserDropDown();
            if (countryId == null)
            {
                return View();
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Country_SelectByID";
            command.Parameters.AddWithValue("@CountryId", countryId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            CountryModel model = new CountryModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.CountryName = dataRow["CountryName"].ToString();
                model.CountryCode = dataRow["CountryCode"].ToString();
                model.UserId = Convert.ToInt32(dataRow["UserId"]);
            }
            return View(model);
            
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
        public IActionResult CountrySave(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.CountryId == 0 || model.CountryId == null)
                {
                    command.CommandText = "PR_Country_Insert";
                }
                else
                {
                    command.CommandText = "PR_Country_Update";
                    command.Parameters.Add("@CountryId", SqlDbType.Int).Value = model.CountryId;
                }

                command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = model.CountryName;
                command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = model.CountryCode;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserId;
                command.ExecuteNonQuery();
                return RedirectToAction("CountryList");
            }
            UserDropDown();
            return View("AddCountry", model);
        }
    }
}
