using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos.Topic
{
    public class TopicCreationDto
    {
        [Required(ErrorMessage = "Topic name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum lenghth for the name of a topic is 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Atleast one question is required.")]
        public List<string>? QuestionId { get; set; }
    }
}
