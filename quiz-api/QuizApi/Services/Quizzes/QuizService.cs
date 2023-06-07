using AutoMapper;
using QuizApi.Dtos.QuizD;
using QuizApi.Models;
using QuizApi.Repositories.Quizzes;

namespace QuizApi.Services.Quizzes
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repository;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<QuizDto> CreateQuiz(QuizCreationDto quizToCreate)
        {
            var quizModel = _mapper.Map<Quiz>(quizToCreate);

            quizModel.Id = await _repository.CreateQuiz(quizModel);

            return _mapper.Map<QuizDto>(quizModel);
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzes()
        {
            var quizModels = await _repository.GetAllQuiz();

            return _mapper.Map<IEnumerable<QuizDto>>(quizModels);
        }

        public async Task<QuizDto?> GetQuizById(int id)
        {
            var quizModel = await _repository.GetQuizById(id);
            if (quizModel == null) return null;

            return _mapper.Map<QuizDto>(quizModel);
        }

        public async Task<QuizDto> UpdateQuiz(int id, QuizCreationDto quizToUpdate)
        {
            // Convert Dto to Models
            var quizModel = _mapper.Map<Quiz>(quizToUpdate);
            quizModel.Id = id;
            await _repository.UpdateQuiz(quizModel);
            return _mapper.Map<QuizDto>(quizModel);
        }

        public Task<bool> DeleteQuiz(int id)
        {
            return _repository.DeleteQuiz(id);
        }

        public Task<bool> CheckQuizById(int id)
        {
            return _repository.CheckQuizId(id);
        }

        public Task<bool> CheckQuizValidation(QuizCreationDto quiz)
        {
            return _repository.CheckQuizValidation(quiz);
        }

        public Task<List<QuizLeaderboard>> GetQuizLeaderboardById(int id)
        {
            return _repository.GetQuizLeaderboardById(id);
        }
    }
}
