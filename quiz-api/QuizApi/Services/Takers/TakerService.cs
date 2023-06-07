using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Models;
using QuizApi.Repositories.Takers;

namespace QuizApi.Services.Takers
{

#pragma warning disable
    public class TakerService : ITakerService
    {
        private readonly ITakerRepository _takerRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TakerService(
            ITakerRepository repository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _takerRepository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<TakerDto> CreateTaker(TakerCreationDto takerToCreate)
        {
            var takerModel = _mapper.Map<Taker>(takerToCreate);
            takerModel.Id = await _takerRepository.CreateTaker(takerModel);

            var takerDto = _mapper.Map<TakerDto>(takerModel);
            return takerDto;
        }

        public async Task<IEnumerable<TakerDto>> GetAllTakers()
        {
            var takerModels = await _takerRepository.GetAll();
            return _mapper.Map<IEnumerable<TakerDto>>(takerModels);
        }

        public async Task<IEnumerable<TakerQuizDetailsDto>> GetAllTakers(int quizId)
        {
            var takerModels = await _takerRepository.GetAllByQuizId(quizId);
            return _mapper.Map<IEnumerable<TakerQuizDetailsDto>>(takerModels);
        }

        public async Task<TakerUserNameDto?> GetTakerByUsername(string username)
        {
            var takerModel = await _takerRepository.GetTakerByUsername(username);
            if (takerModel == null) return null;

            return _mapper.Map<TakerUserNameDto>(takerModel);
        }
        public async Task<TakerUserNameDto?> GetTakerById(int id)
        {
            var takerModel = await _takerRepository.GetTaker(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerUserNameDto>(takerModel);
        }

        public async Task<TakerQuizzesDto?> GetTakerWithQuizById(int id)
        {
            var takerModel = await _takerRepository.GetTakerWithQuiz(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerQuizzesDto>(takerModel);
        }

        public async Task<bool> DeleteTaker(int id)
        {
            return await _takerRepository.DeleteTaker(id);
        }

        public async Task<TakerUserNameDto> UpdateTaker(int id, TakerCreationDto takerToUpdate)
        {
            var takerModel = _mapper.Map<Taker>(takerToUpdate);
            takerModel.Id = id;
            await _takerRepository.UpdateTaker(takerModel);
            var takerDto = _mapper.Map<TakerUserNameDto>(takerModel);

            var taker = await _takerRepository.GetTaker(id);
            return _mapper.Map<TakerUserNameDto>(taker);
        }

        public async Task<int> LetTakerTakeQuiz(int takerId, int quizId)
        {
            return await _takerRepository.LetTakerTakeQuiz(takerId, quizId);
        }

        public async Task<bool> HasTakerTakenQuiz(int takerId, int quizId)
        {
            return await _takerRepository.HasTakerTakenQuiz(takerId, quizId);
        }

        public async Task<TakerAnswersDto?> TakerAnswersQuiz(TakerAnswerCreationDto answer)
        {
            var answerModel = _mapper.Map<TakerAnswer>(answer);
            answerModel.Id = await _takerRepository.TakerAnswersQuiz(answerModel);

            var answerDto = _mapper.Map<TakerAnswersDto>(answerModel);
            return answerDto;
        }

        public async Task<TakerQuizDto?> GetTakerQuizScore(int takerId, int quizId)
        {
            var takerModel = await _takerRepository.GetTakerQuizScore(takerId, quizId);
            if (takerModel == null) return null;

            return _mapper.Map<TakerQuizDto>(takerModel);
        }

        public async Task<IEnumerable<TakerAnswersDto?>> GetTakerAnswer(int id)
        {
            var answerModel = await _takerRepository.GetTakerAnswer(id);
            if (answerModel == null) return null;

            return _mapper.Map<IEnumerable<TakerAnswersDto?>>(answerModel);
        }

        public async Task<IEnumerable<TakerAnswersDto>> GetAllAnswers()
        {
            var answerModels = await _takerRepository.GetAllAnswers();
            return _mapper.Map<IEnumerable<TakerAnswersDto>>(answerModels);
        }
        
        public bool VerifyPassword(string password, string inputtedPassword)
        {
            return password == inputtedPassword;
        }

        public string CreateToken(TakerUserNameDto taker)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, taker.Name),
                new Claim(ClaimTypes.StreetAddress, taker.Address),
                new Claim(ClaimTypes.Email, taker.Email),
                new Claim(ClaimTypes.NameIdentifier, taker.Username),
                new Claim(ClaimTypes.Role, taker.TakerType),
            };

            foreach (var quiz in taker.Quizzes)
            {
                claims.Add(new Claim("Quiz", quiz.Name));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<TakerQuizDto> TakerUpdateTakenDate(int takerId, int quizId)
        {
            var taker = await _takerRepository.TakerUpdateTakenDate(takerId, quizId);
            if (taker == null) return null;

            return _mapper.Map<TakerQuizDto>(taker);
        }

        public async Task<TakerQuizDto> TakerUpdateFinishedDate(int takerId, int quizId)
        {
            var taker = await _takerRepository.TakerUpdateFinishedDate(takerId, quizId);
            if (taker == null) return null;

            return _mapper.Map<TakerQuizDto>(taker);
        }

        public async Task<TakerQuizDto> UpdateTakerRetake(int retake, int takerId, int quizId)
        {
            var taker = await _takerRepository.SetQuizRetake(retake, takerId, quizId);
            if (taker == null) return null;

            return _mapper.Map<TakerQuizDto>(taker);
        }

        public async Task<bool> DeleteAnswer(int id)
        {
            return await _takerRepository.DeleteAnswer(id);
        }

        public async Task<TakerAnswersDto?> GetAnswerById(int id)
        {
            var takerModel = await _takerRepository.GetAnswerById(id);
            if (takerModel == null) return null;

            return _mapper.Map<TakerAnswersDto>(takerModel);
        }

        public async Task<TakerAnswersDto?> GetAnswerDetails(int takerId, int quizId, int questionId)
        {
            var takerModel = await _takerRepository.GetAnswerDetails(takerId, quizId, questionId);
            if (takerModel == null) return null;

            return _mapper.Map<TakerAnswersDto>(takerModel);
        }

        public async Task<bool> ValidateCreate(TakerCreationDto taker)
        {
           return await _takerRepository.ValidateCreate(taker);
        }
    }
}
