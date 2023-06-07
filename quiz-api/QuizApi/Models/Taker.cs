using QuizApi.Dtos.QuizD;

namespace QuizApi.Models
{
    public class Taker
    {
        /// <summary>
        /// Properties for Taker
        /// </summary>
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
    }
}
