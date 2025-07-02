using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class StateModel
    {
        [Key]
        public int? StateId { get; set; }
        [Required(ErrorMessage = "State Name Is Required")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Country Id Is Required")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "State Code Is Required")]
        public string StateCode { get; set; }
        [Required(ErrorMessage = "User Id Is Required")]
        public int UserId {get; set; }
    }
    public class StateDropDownModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
