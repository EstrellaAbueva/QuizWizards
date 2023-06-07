namespace QuizApi.Models
{
    public class TakerAnswer
    {
        /// <summary>
        /// Properties for TakerAnswer
        /// </summary>
        public int Id { get; set; }
        public int TakerId { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string? Answer { get; set; }
    }
}
