using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;

namespace QuizApi.Dtos.QuizD
{
    public class QuizTakersDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<TakerDto> Takers { get; set; } = new List<TakerDto>();
    }
}
