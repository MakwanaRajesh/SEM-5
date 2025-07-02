using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Models
{
    //    public class User
    //    {
    //    }
    //}


    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}