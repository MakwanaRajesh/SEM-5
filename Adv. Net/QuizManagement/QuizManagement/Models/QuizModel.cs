using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuizManagement.Models
{
    public class QuizModel
    {
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Quiz Name Require")]
        public string QuizName { get; set; }

        [Required(ErrorMessage = "Total Question Name Require")]

        public int TotalQuestions { get; set; }
        [Required(ErrorMessage = "Quiz Date Require")]

        public DateTime QuizDate { get; set; }
        public int UserName { get; set; }
        public int UserID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    public class QuizDropdownModel
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be today or in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}