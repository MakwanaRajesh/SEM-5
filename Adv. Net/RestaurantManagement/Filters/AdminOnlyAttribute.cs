using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userEmail = context.HttpContext.Session.GetString("UserEmail");

        if (string.IsNullOrEmpty(userEmail))
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        var connection = config.GetConnectionString("DefaultConnection");

        using (var conn = new SqlConnection(connection))
        {
            var cmd = new SqlCommand("SELECT IsAdmin FROM Users WHERE Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", userEmail);
            conn.Open();

            var isAdmin = cmd.ExecuteScalar();
            if (isAdmin == null || !(bool)isAdmin)
            {
                context.Result = new ContentResult
                {
                    Content = "Access Denied. Admins only.",
                    StatusCode = 403
                };
            }
        }
    }
}
