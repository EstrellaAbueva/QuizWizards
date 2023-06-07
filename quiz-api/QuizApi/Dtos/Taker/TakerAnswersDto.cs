using QuizApi.Dtos.Question;
using QuizApi.Models;

namespace QuizApi.Dtos.Taker
{
    public class TakerAnswersDto
    {
        public int Id { get; set; }
        public int TakerId { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string? Answer { get; set; }

    }
}
