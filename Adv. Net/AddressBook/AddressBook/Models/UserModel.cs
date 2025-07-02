using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mobile Number Is Required")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email Id Is Required")]
        public string EmailId { get; set; }
    }
    public class UserDropDownModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
