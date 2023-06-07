using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos.Question
{
    public class QuestionUpdateDto
    {
        [Required(ErrorMessage = "Question is required.")]
        public string? Question { get; set; }

        [Required(ErrorMessage = "CorrectAnswer is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for Question's CorrectAnswer is 50 characters.")]
        public string? CorrectAnswer { get; set; }
    }
}
