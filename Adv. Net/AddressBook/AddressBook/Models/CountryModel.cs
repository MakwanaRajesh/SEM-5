using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class CountryModel
    {
        [Key]
        public int? CountryId { get; set; }
        [Required(ErrorMessage = "Country Name Is Required")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Country Code Is Required")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "User Id Is Required")]
        public int UserId { get; set; }
    }
    public class CountryDropDownModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
