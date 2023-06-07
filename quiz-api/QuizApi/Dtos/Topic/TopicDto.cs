using QuizApi.Dtos.Question;
using QuizApi.Models;

namespace QuizApi.Dtos.Topic
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? NumberOfQuestions { get; set; }
        public List<Problem> Questions { get; set; } = new List<Problem>();
    }
}
