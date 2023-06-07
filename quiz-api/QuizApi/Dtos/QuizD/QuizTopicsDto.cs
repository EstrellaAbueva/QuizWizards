using QuizApi.Dtos.Topic;

namespace QuizApi.Dtos.QuizD
{
    public class QuizTopicsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
    }
}
