using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class QuestionLevelModel
    {
        [Required(ErrorMessage = "Country Name is Required")]
        public int QuestionLevelID { get; set; }

        [Required(ErrorMessage = "Question Level is Required")]
        public string QuestionLevel { get; set; }

        public string? QuestionText { get; set; }

        [Required(ErrorMessage = "User name is Required")]
        public int UserID { get; set; }
        public string? UserName { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
    public class QuestionLevelDropdownModel
    {
        public int QuestionLevelID { get; set; }
        public string QuestionLevel { get; set; }
    }

}
