using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class QuizWiseQuestionModel
    {
        public int QuizWiseQuestionsID { get; set; }

        [Required(ErrorMessage = "Quiz Name is Required")]
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Question Name is Required")]
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        public int UserID { get; set; }

        public string? QuestionText { get; set; }
        public string? QuizName { get; set; }
        public string? UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
}
