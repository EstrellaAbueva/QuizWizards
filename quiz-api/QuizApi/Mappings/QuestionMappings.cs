using AutoMapper;
using QuizApi.Dtos.Question;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class QuestionMappings : Profile
    {
        public QuestionMappings()
        {
            CreateMap<Problem, QuestionDto>();
            CreateMap<QuestionUpdateDto, Problem>();
            CreateMap<QuestionCreationDto, Problem>();
        }
    }
}
