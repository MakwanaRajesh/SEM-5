using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class CityModel
    {
        [Key]
        public int? CityId { get; set; }
        [Required(ErrorMessage = "Country Id Is Required")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "State Id Is Required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "City Name Is Required")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Std Code Is Required")]
        public string StdCode { get; set; }
        [Required(ErrorMessage = "Pin Code Is Required")]
        public string PinCode {  get; set; }
        [Required(ErrorMessage = "User Id Is Required")]
        public int UserId {  get; set; }

    }
}
