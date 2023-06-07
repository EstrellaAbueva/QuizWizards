using AutoMapper;
using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class TopicMappings : Profile
    {
        public TopicMappings()
        {
            CreateMap<TopicCreationDto, Topic>();
            CreateMap<Topic, TopicDto>();
            CreateMap<Topic, Problem>();
        }
        
    }
}
