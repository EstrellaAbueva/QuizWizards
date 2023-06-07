using QuizApi.Dtos.Question;
using QuizApi.Models;
using AutoMapper;
using QuizApi.Repositories;

namespace QuizApi.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository repository, IMapper mapper)
        {
            _questionRepository = repository;
            _mapper = mapper;
        }

        public async Task<QuestionDto> CreateQuestion(QuestionCreationDto questionToCreate)
        {
            var questionModel = _mapper.Map<Problem>(questionToCreate);

            questionModel.Id = await _questionRepository.CreateQuestion(questionModel);

            return _mapper.Map<QuestionDto>(questionModel);
        }

        public Task<bool> DeleteQuestion(int id)
        {
            return _questionRepository.DeleteQuestion(id);
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestions();
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto?> GetQuestionById(int id)
        {
            var question = await _questionRepository.GetQuestionById(id);
            if (question == null) return null;

            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsByTopicId(int id)
        {
            var questions = await _questionRepository.GetAllQuestionsByTopicId(id);
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto> UpdateQuestion(int id, QuestionUpdateDto questionToUpdate)
        {
            var questionModel = _mapper.Map<Problem>(questionToUpdate);
            questionModel.Id = id;
            await _questionRepository.UpdateQuestion(questionModel);
            var questionDto = _mapper.Map<QuestionDto>(questionModel);

            return questionDto;
        }

        public async Task<bool> ValidateQuestion(QuestionCreationDto question)
        {
            return await _questionRepository.ValidateQuestion(question);
        }

        public async Task<bool> ValidateQuestion(QuestionUpdateDto question)
        {
            return await _questionRepository.ValidateQuestion(question);
        }
    }
}
