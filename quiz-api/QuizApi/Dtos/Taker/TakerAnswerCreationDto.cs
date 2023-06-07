using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos.Taker
{
    public class TakerAnswerCreationDto
    {
        [Required(ErrorMessage = "The Id of the Taker required.")]
        public int TakerId { get; set; }

        [Required(ErrorMessage = "The Id of the Quiz required.")]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "The Id of the Question required.")]
        public int QuestionId { get; set; }
        public string? Answer { get; set; }
    }
}
