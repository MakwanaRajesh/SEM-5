using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class StateController : Controller
    {
        private IConfiguration configuration;
        public StateController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IActionResult StateList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Pr_State_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult StateDelete(int StateId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Pr_State_Delete";
                    command.Parameters.Add("@StateId", SqlDbType.Int).Value = StateId;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "State deleted successfully.";
                return RedirectToAction("StateList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the state: " + ex.Message;
                return RedirectToAction("StateList");
            }
        }
        public IActionResult AddState(int? StateId)
        {
            UserDropDown();
            CountryDropDown(); 
            if (StateId == null)
            {
                return View();
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_SelectById";
            command.Parameters.AddWithValue("@StateId", StateId);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            StateModel model = new StateModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.StateName = dataRow["StateName"].ToString();
                model.CountryId = Convert.ToInt32(dataRow["CountryId"]);
                model.StateCode = dataRow["StateCode"].ToString();
                model.UserId = Convert.ToInt32(dataRow["UserId"]);
            }
            return View(model);
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
        public IActionResult StateSave(StateModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.StateId == 0 || model.StateId == null)
                {
                    command.CommandText = "PR_State_Insert";
                }
                else
                {
                    command.CommandText = "PR_State_Update";
                    command.Parameters.Add("@StateId", SqlDbType.Int).Value = model.StateId;
                }
                
                command.Parameters.Add("@StateName", SqlDbType.VarChar).Value = model.StateName;
                command.Parameters.Add("@CountryId", SqlDbType.Int).Value = model.CountryId;
                command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = model.StateCode;
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserId;
                command.ExecuteNonQuery();
                return RedirectToAction("StateList");
            }
            UserDropDown();
            CountryDropDown();
            return View("AddState", model);
        }
    }
}
