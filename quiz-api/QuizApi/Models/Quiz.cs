using QuizApi.Dtos.Topic;

namespace QuizApi.Models
{
    public class Quiz
    {
        /// <summary>
        /// Properties for Quiz
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public List<string>? TopicId { get; set; }
        public int MaxScore { get; set; }
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
    }
}
