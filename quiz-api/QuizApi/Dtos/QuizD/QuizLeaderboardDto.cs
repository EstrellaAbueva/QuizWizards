using QuizApi.Dtos.Taker;

namespace QuizApi.Dtos.QuizD
{
    public class QuizLeaderboard
    {
        public string? TakerName { get; set; }
        public string? QuizName { get; set; }
        public int? Score { get; set; }
    }
}
