using QuizApi.Dtos.QuizD;
using QuizApi.Models;

namespace QuizApi.Dtos.Taker
{
    public class TakerQuizTakerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? TakerType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public List<QuizDto> Quizzes { get; set; } = new List<QuizDto>();
        public List<TakerAnswer> TakerAnswers { get; set; } = new List<TakerAnswer>();
        public DateTime? AssignedDate { get; set; } = DateTime.Now;
        public int Score { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public int CanRetake { get; set; }
    }
}
