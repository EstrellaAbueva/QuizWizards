namespace QuizApi.Models
{
    public class TakerQuiz
    {
        public int Id { get; set; }
        public int TakerId { get; set; }
        public int QuizId { get; set; }
        public DateTime? AssignedDate { get; set; } = DateTime.Now;
        public int Score { get; set; }
        public DateTime? TakenDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public int CanRetake { get; set; }
    }
}
