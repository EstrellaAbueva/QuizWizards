using AutoMapper;
using Moq;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Mappings;
using QuizApi.Models;

namespace QuizAPITests.Mapping
{
    public class QuizMappingTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public QuizMappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<QuizMappings>();
            });
            _mapper = _configuration.CreateMapper();
        }

        //QuizCreationDto to Quiz
        [Fact]
        public void Map_ValidQuizCreationDto_ReturnQuiz()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quizCreationDto = new QuizCreationDto()
            {
                Name = "Test Quiz",
                Description = "Test Description",
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                }
            };

            var expectedQuiz = new Quiz()
            {
                Id = id,
                Name = "Test Quiz",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            //Act
            var mapping = _mapper.Map<Quiz>(quizCreationDto);

            //Assert
            Assert.Equal(expectedQuiz.Name, mapping.Name);
            Assert.Equal(expectedQuiz.Description, mapping.Description);    
            Assert.Equal(expectedQuiz.TopicId, mapping.TopicId);
            //Assert.Equal(expectedQuiz.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedQuiz.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to QuizDto
        [Fact]
        public void Map_ValidQuiz_ReturnQuizDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Quiz",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedQuizDto = new QuizDto()
            {
                Id = id,
                Name = "Test Quiz",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            //Act
            var mapping = _mapper.Map<QuizDto>(quiz);

            //Assert
            Assert.Equal(expectedQuizDto.Id, mapping.Id);
            Assert.Equal(expectedQuizDto.Name, mapping.Name);
            Assert.Equal(expectedQuizDto.Description, mapping.Description);
            Assert.Equal(expectedQuizDto.MaxScore, mapping.MaxScore);
            Assert.Equal(expectedQuizDto.Topics.Count(), mapping.Topics.Count());
            //Assert.Equal(expectedQuizDto.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedQuizDto.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to TopicDto
        [Fact]
        public void Map_ValidQuiz_ReturnTopicDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedTopicDto = new TopicDto()
            {
                Id = id,
                Name = "Test Name",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 2,
                Questions = new List<Problem> { new Problem() { } }
            };

            //Act
            var mapping = _mapper.Map<TopicDto>(quiz);

            //Assert
            Assert.Equal(expectedTopicDto.Id, mapping.Id);
            Assert.Equal(expectedTopicDto.Name, mapping.Name);
            //Assert.Equal(expectedTopicDto.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedTopicDto.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to Problem
        [Fact]
        public void Map_ValidQuiz_ReturnProblem()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedProblem = new Problem()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            //Act
            var mapping = _mapper.Map<Problem>(quiz);

            //Assert
            Assert.Equal(expectedProblem.Id, mapping.Id);   
            //Assert.Equal(expectedProblem.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedProblem.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to QuizTakerDto
        [Fact]
        public void Map_ValidQuiz_ReturnQuizTakerDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedQuizTakerDto = new QuizTakerDto()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                TakerName = "Test Taker Name",
            };

            //Act
            var mapping = _mapper.Map<QuizTakerDto>(quiz);

            //Assert
            Assert.Equal(expectedQuizTakerDto.Id, mapping.Id);
            Assert.Equal(expectedQuizTakerDto.Name, mapping.Name);
            Assert.Equal(expectedQuizTakerDto.Description, mapping.Description);
            //Assert.Equal(expectedQuizTakerDto.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedQuizTakerDto.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to QuizTakersDto
        [Fact]
        public void Map_ValidQuiz_ReturnQuizTakersDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedQuizTakersDto = new QuizTakersDto()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                Takers = new List<TakerDto> { new TakerDto() { } }
            };

            //Act
            var mapping = _mapper.Map<QuizTakersDto>(quiz);

            //Assert
            Assert.Equal(expectedQuizTakersDto.Id, mapping.Id);
            Assert.Equal(expectedQuizTakersDto.Name, mapping.Name);
            Assert.Equal(expectedQuizTakersDto.Description, mapping.Description);
            //Assert.Equal(expectedQuizTakersDto.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedQuizTakersDto.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to QuizTopicsDto
        [Fact]
        public void Map_ValidQuiz_ReturnQuizTopicsDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedQuizTopicsDto = new QuizTopicsDto()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            //Act
            var mapping = _mapper.Map<QuizTopicsDto>(quiz);

            //Assert
            Assert.Equal(expectedQuizTopicsDto.Id, mapping.Id);
            Assert.Equal(expectedQuizTopicsDto.Name, mapping.Name);
            Assert.Equal(expectedQuizTopicsDto.Description, mapping.Description);
            Assert.Equal(expectedQuizTopicsDto.Topics.Count(), mapping.Topics.Count());
            //Assert.Equal(expectedQuizTopicsDto.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedQuizTopicsDto.UpdatedDate, mapping.UpdatedDate);
        }

        //Quiz to QuizLeaderboard
        [Fact]
        public void Map_ValidQuiz_ReturnQuizLeaderboard()
        {
            //Arrange
            var id = It.IsAny<int>();

            var quiz = new Quiz()
            {
                Id = id,
                Name = "Test Name",
                Description = "Test Description",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TopicId = new List<string>()
                {
                    "1",
                    "2"
                },
                MaxScore = 2,
                Topics = new List<TopicDto> { new TopicDto() { } }
            };

            var expectedQuizLeaderboard = new QuizLeaderboard()
            {
                TakerName = "Test Taker Name",
                QuizName = "Test Quiz Name",
                Score = 2,
            };

            //Act
            var mapping = _mapper.Map<QuizLeaderboard>(quiz);

            //Assert
            Assert.IsType<QuizLeaderboard>(mapping);
        }
    }
}
