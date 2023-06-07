using AutoMapper;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class QuizMappings : Profile
    {
        public QuizMappings()
        {
            CreateMap<QuizCreationDto, Quiz>();
            CreateMap<Quiz, QuizDto>();
            CreateMap<Quiz, TopicDto>();
            CreateMap<Quiz, Problem>();
            CreateMap<Quiz, QuizTakerDto>();
            CreateMap<Quiz, QuizTakersDto>();
            CreateMap<Quiz, QuizTopicsDto>();
            CreateMap<Quiz, QuizLeaderboard>();
        }
    }
}
