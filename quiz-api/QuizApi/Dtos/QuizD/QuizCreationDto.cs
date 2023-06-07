using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos.QuizD
{
    public class QuizCreationDto
    {
        [Required(ErrorMessage = "The name for the Quiz is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Quiz requires a description.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Quiz requires a TopicIds.")]
        public List<string>? TopicId { get; set; }
    }
}
