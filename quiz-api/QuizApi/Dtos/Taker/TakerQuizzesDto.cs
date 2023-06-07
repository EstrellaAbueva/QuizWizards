using QuizApi.Dtos.QuizD;

namespace QuizApi.Dtos.Taker
{
    public class TakerQuizzesDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? TakerType { get; set; }
        public DateTime? AssignedDate { get; set; } = DateTime.Now;
        public int Score { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public List<QuizDto> Quizzes { get; set; } = new List<QuizDto>();  
        public int CanRetake { get; set; }
    }
}
