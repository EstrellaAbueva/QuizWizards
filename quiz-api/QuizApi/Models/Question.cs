namespace QuizApi.Models
{
    public class Problem
    {
        /// <summary>
        /// Properties for Problem (Questions table)
        /// </summary>
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? CorrectAnswer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
    }
}
