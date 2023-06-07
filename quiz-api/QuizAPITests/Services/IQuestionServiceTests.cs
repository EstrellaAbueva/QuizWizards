using AutoMapper;
using Moq;
using QuizApi.Dtos.Question;
using QuizApi.Models;
using QuizApi.Repositories;
using QuizApi.Services.Questions;

namespace QuizAPITests.Services
{
    public class IQuestionServiceTests
    {
        private readonly IQuestionService _QuestionService;
        private readonly Mock<IQuestionRepository> _fakeQuestionRepository;
        private readonly Mock<IMapper> _fakeMapper;

        public IQuestionServiceTests()
        {
            _fakeQuestionRepository = new Mock<IQuestionRepository>();
            _fakeMapper = new Mock<IMapper>();
            _QuestionService = new QuestionService(_fakeQuestionRepository.Object, _fakeMapper.Object);
        }

        //CreateQuestion returns QuestionDto
        [Fact]
        public async Task CreateQuestion_WithValidQuestion_ReturnsQuestion()
        {
            //Arrange
            var id = It.IsAny<int>();

            var questionToCreate = new QuestionCreationDto()
            {
                Question = "Test Question",
                CorrectAnswer = "Test Answer"
            };

            var model = new Problem()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var question = new QuestionDto()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _fakeMapper.Setup(m => m.Map<Problem>(questionToCreate)).Returns(model);
            _fakeQuestionRepository.Setup(m => m.CreateQuestion(model)).ReturnsAsync(1);
            _fakeMapper.Setup(m => m.Map<QuestionDto>(model)).Returns(question);

            //Act
            var result = await _QuestionService.CreateQuestion(questionToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(question, result);
        }

        //Create Question returns null
        [Fact]
        public async Task CreateQuestion_WhenCalled_ReturnsNull()
        {
            //Arrange
            var id = It.IsAny<int>();

            var questionToCreate = new QuestionCreationDto()
            {
                Question = "Test Question",
                CorrectAnswer = "Test Answer"
            };

            var model = new Problem()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var question = new QuestionDto()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _fakeMapper.Setup(m => m.Map<Problem>(questionToCreate)).Returns(model);
            _fakeQuestionRepository.Setup(m => m.CreateQuestion(model)).ReturnsAsync(-2);

            //Act
            var result = await _QuestionService.CreateQuestion(questionToCreate);

            //Assert
            Assert.Null(result);
        }

        //Create Question ThrowsException
        [Fact]
        public async Task CreateQuestion_WhenCalled_ThrowsException()
        {
            //Arrange
            var id = It.IsAny<int>();

            var questionToCreate = new QuestionCreationDto()
            {
                Question = "Test Question",
                CorrectAnswer = "Test Answer"
            };

            var model = new Problem()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var question = new QuestionDto()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _fakeMapper.Setup(m => m.Map<Problem>(questionToCreate)).Returns(model);
            _fakeQuestionRepository.Setup(m => m.CreateQuestion(model)).Throws(new Exception("Database connection error"));

            //Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuestionService.CreateQuestion(questionToCreate));

            //Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAllQuestions returns all question
        [Fact]
        public async Task GetAllQuestions_WithValidQuestion_ReturnsAllQuestions() 
        { 
            // Arrange
            var questions = new List<QuestionDto>
            {
                new QuestionDto
                {
                    Id = 1,
                    Question = "Test Question",
                    CorrectAnswer = "Test Answer",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            var question2 = new List<Problem>
            {
                new Problem
                {
                    Id = 1,
                    Question = "Test Question",
                    CorrectAnswer = "Test Answer",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            _fakeQuestionRepository.Setup(m => m.GetAllQuestions()).ReturnsAsync(question2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<QuestionDto>>(question2)).Returns(questions);

            // Act
            var result = await _QuestionService.GetAllQuestions();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuestionDto>>(result);
            Assert.Equal(question2.Count(), result.Count());
        }

        //GetAllQuestions returns Empty
        [Fact]
        public async Task GetAllQuestions_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var questions = new List<QuestionDto>();
            var question2 = new List<Problem>();

            _fakeQuestionRepository.Setup(m => m.GetAllQuestions()).ReturnsAsync(question2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<QuestionDto>>(question2)).Returns(questions);

            // Act
            var result = await _QuestionService.GetAllQuestions();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuestionDto>>(result);
            Assert.Empty(result);
        }

        //GetAllQuestions ThrowsException
        [Fact]
        public async Task GetAllQuestions_WhenCalled_ThrowsException()
        {
            // Arrange
            _fakeQuestionRepository.Setup(m => m.GetAllQuestions())
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuestionService.GetAllQuestions());

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAllQuestionsByTopicId returns QuestionsByTopicId
        [Fact]
        public async Task GetAllQuestionsByTopicId_WithValidQuestion_ReturnsQuestionsByTopicId()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            var questions = new List<QuestionDto>
            {
                new QuestionDto
                {
                    Id = 1,
                    Question = "Test Question",
                    CorrectAnswer = "Test Answer",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            var question2 = new List<Problem>
            {
                new Problem
                {
                    Id = 1,
                    Question = "Test Question",
                    CorrectAnswer = "Test Answer",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            _fakeMapper.Setup(m => m.Map<IEnumerable<Problem>>(questions)).Returns(question2);
            _fakeQuestionRepository.Setup(m => m.GetAllQuestionsByTopicId(topicId)).ReturnsAsync(question2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<QuestionDto>>(question2)).Returns(questions);

            // Act
            var result = await _QuestionService.GetAllQuestionsByTopicId(topicId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuestionDto>>(result);
            Assert.Equal(question2.Count(), result.Count());
        }

        //GetAllQuestionsByTopicId returns empty
        [Fact]
        public async Task GetAllQuestionsByTopicId_WithValidQuestion_ReturnsEmpty()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            var questions = new List<QuestionDto>();
            var question2 = new List<Problem>();

            _fakeMapper.Setup(m => m.Map<IEnumerable<Problem>>(questions)).Returns(question2);
            _fakeQuestionRepository.Setup(m => m.GetAllQuestionsByTopicId(1)).ReturnsAsync(question2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<QuestionDto>>(questions)).Returns(questions);

            // Act
            var result = await _QuestionService.GetAllQuestionsByTopicId(topicId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<QuestionDto>>(result);
            Assert.Empty(result);
        }

        //GetAllQuestionsByTopicId returns ThrowsException
        [Fact]
        public async Task GetAllQuestionsByTopicId_WithValidQuestion_ThrowsException()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            _fakeQuestionRepository.Setup(m => m.GetAllQuestionsByTopicId(topicId))
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuestionService.GetAllQuestionsByTopicId(topicId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetQuestionById returns question
        [Fact]
        public async Task GetQuestionById_WithValidQuestion_ReturnsQuestion()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionDto
            {
                Id = questionId,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var question2 = new Problem
            {
                Id = questionId,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _fakeMapper.Setup(m => m.Map<Problem>(question)).Returns(question2);
            _fakeQuestionRepository.Setup(m => m.GetQuestionById(questionId)).ReturnsAsync(question2);
            _fakeMapper.Setup(m => m.Map<QuestionDto>(question2)).Returns(question);

            // Act
            var result = await _QuestionService.GetQuestionById(questionId);

            // Assert
            Assert.Equal(question, result);
            Assert.NotNull(result);
            Assert.IsType<QuestionDto>(result);
        }

        //GetQuestionById returns Null
        [Fact]
        public async Task GetQuestionById_WhenCalled_ReturnsNull()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionDto();
            var question2 = new Problem();

            _fakeMapper.Setup(m => m.Map<Problem>(question)).Returns(question2);
            _fakeQuestionRepository.Setup(m => m.GetQuestionById(questionId)).ReturnsAsync(question2);
            _fakeMapper.Setup(m => m.Map<QuestionDto>(question2)).Returns(question);

            // Act
            var result = await _QuestionService.GetQuestionById(questionId);

            // Assert
            Assert.Equal(question, result);
        }

        //GetQuestionById ThrowsException
        [Fact]
        public async Task GetQuestionById_WhenCalled_ThrowsException()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            _fakeQuestionRepository.Setup(m => m.GetQuestionById(questionId))
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuestionService.GetQuestionById(questionId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //UpdateQuestion returns question
        [Fact]
        public async Task UpdateQuestion_WhenCalled_ReturnsQuestionDto()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionDto
            {
                Id = questionId,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var question2 = new Problem
            {
                Id = questionId,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var updateQuestion = new QuestionUpdateDto();

            _fakeMapper.Setup(m => m.Map<Problem>(updateQuestion)).Returns(question2);
            _fakeQuestionRepository.Setup(m => m.UpdateQuestion(question2)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<QuestionDto>(question2)).Returns(question);

            // Act
            var result = await _QuestionService.UpdateQuestion(questionId, updateQuestion);

            // Assert
            Assert.Equal(question, result);
            Assert.NotNull(result);
            Assert.IsType<QuestionDto>(result);
        }

        //UpdateQuestion returns null
        [Fact]
        public async Task UpdateQuestion_WhenCalled_ReturnsNull()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionDto();
            var question2 = new Problem();
            var updateQuestion = new QuestionUpdateDto();

            _fakeMapper.Setup(m => m.Map<Problem>(updateQuestion)).Returns(question2);
            _fakeQuestionRepository.Setup(m => m.UpdateQuestion(question2)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<QuestionDto>(question2)).Returns(question);

            // Act
            var result = await _QuestionService.UpdateQuestion(questionId, updateQuestion);

            // Assert
            Assert.Equal(question, result);
        }

        //UpdateQuestion ThrowsException
        [Fact]
        public async Task UpdateQuestion_WhenCalled_ThrowsException()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            var model = new Problem();
            var updateQuestion = new QuestionUpdateDto();

            _fakeMapper.Setup(m => m.Map<Problem>(updateQuestion)).Returns(model);
            _fakeQuestionRepository.Setup(m => m.UpdateQuestion(model))
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuestionService.UpdateQuestion(questionId, updateQuestion));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //DeleteQuestion returns true
        [Fact]
        public async Task DeleteQuestion_WithValidQuestion_ReturnsTrue()
        {
            // Arrange
            var questionId = It.IsAny<int>();

            _fakeQuestionRepository.Setup(m => m.DeleteQuestion(questionId)).ReturnsAsync(true);

            // Act
            var result = await _QuestionService.DeleteQuestion(questionId);

            // Assert
            Assert.True(result);
        }

        //DeleteQuestion returns false
        [Fact]
        public async Task DeleteQuestion_WithValidQuestion_ReturnsFalse()
        {
            // Arrange
            var questionId = It.IsAny<int>();

            _fakeQuestionRepository.Setup(m => m.DeleteQuestion(questionId)).ReturnsAsync(false);

            // Act
            var result = await _QuestionService.DeleteQuestion(questionId);

            // Assert
            Assert.False(result);
        }

        //DeleteQuestion ThrowsException
        [Fact]
        public async Task DeleteQuestion_WhenCalled_ThrowsException()
        {
            // Arrange
            var questionId = It.IsAny<int>();
            _fakeQuestionRepository.Setup(m => m.DeleteQuestion(questionId))
                                   .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _QuestionService.DeleteQuestion(questionId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }
    }
}
