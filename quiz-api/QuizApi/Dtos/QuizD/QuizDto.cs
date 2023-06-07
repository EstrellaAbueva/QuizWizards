using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Dtos.QuizD
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public int MaxScore { get; set; }
        public List<TopicDto>? Topics { get; set; } = new List<TopicDto>();
    }
}
