using AutoMapper;
using QuizApi.Dtos.Taker;
using QuizApi.Models;

namespace QuizApi.Mappings
{
    public class TakerMappings : Profile
    {
        public TakerMappings()
        {
            CreateMap<TakerCreationDto, Taker>();
            CreateMap<Taker, TakerDto>();
            CreateMap<TakerQuiz, TakerQuizDto>();
            CreateMap<Taker, TakerQuizDto>();
            CreateMap<Taker, TakerQuizDetailsDto>()
                .ForMember(dto => dto.QuizName, opt => opt.MapFrom(st => st.Quizzes!.Single().Name));
            CreateMap<Taker, TakerQuizzesDto>();
            CreateMap<TakerAnswer, TakerAnswersDto>();
            CreateMap<TakerAnswer, TakerAnswerCreationDto>();
            CreateMap<TakerAnswerCreationDto, TakerAnswer>();
            CreateMap<Taker, TakerUserNameDto>();
            CreateMap<Taker, TakerUpdateDto>();
            CreateMap<TakerQuizTaker, TakerQuizTakerDto>();
            CreateMap<TakerQuizTaker, TakerUserNameDto>();
            CreateMap<Taker, TakerQuizTaker>();
        }
    }
}
