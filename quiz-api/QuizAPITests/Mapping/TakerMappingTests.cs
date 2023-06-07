using AutoMapper;
using Moq;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Mappings;
using QuizApi.Models;

namespace QuizAPITests.Mapping
{
    public class TakerMappingTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public TakerMappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TakerMappings>();
            });
            _mapper = _configuration.CreateMapper();
        }

        //TakerCreationDto to Taker
        [Fact]
        public void Map_ValidTakerCreationDto_RetursTaker()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerCreationDto = new TakerCreationDto
            {
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
            };

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } },
            };

            //Act
            var result = _mapper.Map<Taker>(takerCreationDto);

            //Assert
            Assert.Equal(taker.Id, result.Id);
            Assert.Equal(taker.Name, result.Name);
            Assert.Equal(taker.Address, result.Address);
            Assert.Equal(taker.Email, result.Email);
            Assert.Equal(taker.Username, result.Username);
            Assert.Equal(taker.Password, result.Password);
        }

        //Taker to TakerDto
        [Fact]
        public void Map_ValidTaker_RetursTakerDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } },
            };

            var takerDto = new TakerDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            //Act
            var result = _mapper.Map<TakerDto>(taker);

            //Assert
            Assert.Equal(takerDto.Id, result.Id);
            Assert.Equal(takerDto.Name, result.Name);
            Assert.Equal(takerDto.Address, result.Address);
            Assert.Equal(takerDto.Email, result.Email);
            Assert.Equal(takerDto.Username, result.Username);
            //Assert.Equal(takerDto.CreatedDate, result.CreatedDate);
            //Assert.Equal(takerDto.UpdatedDate, result.UpdatedDate);
        }

        //TakerQuiz to TakerQuizDto
        [Fact]
        public void Map_ValidTakerQuiz_RetursTakerQuizDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerQuiz = new TakerQuiz
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            var takerQuizDto = new TakerQuizDto
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            //Act
            var result = _mapper.Map<TakerQuizDto>(takerQuiz);

            //Assert
            Assert.Equal(takerQuizDto.Id, result.Id);
            Assert.Equal(takerQuizDto.TakerId, result.TakerId);
            Assert.Equal(takerQuizDto.QuizId, result.QuizId);
            //Assert.Equal(takerQuizDto.AssignedDate, result.AssignedDate);
            Assert.Equal(takerQuizDto.Score, result.Score);
            //Assert.Equal(takerQuizDto.TakenDate, result.TakenDate);
            //Assert.Equal(takerQuizDto.FinishedDate, result.FinishedDate);
            Assert.Equal(takerQuizDto.CanRetake, result.CanRetake);
        }

        //Taker to TakerQuizDto
        [Fact]
        public void Map_ValidTaker_RetursTakerQuizDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } }
            };

            var takerQuizDto = new TakerQuizDto
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            //Act
            var result = _mapper.Map<TakerQuizDto>(taker);

            //Assert
            Assert.Equal(takerQuizDto.Id, result.Id);
            Assert.Equal(takerQuizDto.TakerId, result.TakerId);
            Assert.Equal(takerQuizDto.QuizId, result.QuizId);
            //Assert.Equal(takerQuizDto.AssignedDate, result.AssignedDate);
            Assert.Equal(takerQuizDto.Score, result.Score);
            //Assert.Equal(takerQuizDto.TakenDate, result.TakenDate);
            //Assert.Equal(takerQuizDto.FinishedDate, result.FinishedDate);
            Assert.Equal(takerQuizDto.CanRetake, result.CanRetake);
        }

        //Taker to TakerQuizDetailsDto
        [Fact]
        public void Map_ValidTaker_RetursTakerQuizDetailsDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } }
            };

            var expectedTaker = new TakerQuizDetailsDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                QuizName = "TestQuizName",
            };

            //Act
            var result = _mapper.Map<TakerQuizDetailsDto>(taker);

            //Assert
            Assert.Equal(expectedTaker.Id, result.Id);
            Assert.Equal(expectedTaker.Name, result.Name);
            Assert.Equal(expectedTaker.Address, result.Address);
            Assert.Equal(expectedTaker.Email, result.Email);
            Assert.Equal(expectedTaker.Username, result.Username);
        }

        //Taker to TakerQuizzesDto
        [Fact]
        public void Map_ValidTaker_RetursTakerQuizzesDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } }
            };

            var expectedTaker = new TakerQuizzesDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                TakerType = "T",
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            //Act
            var result = _mapper.Map<TakerQuizzesDto>(taker);

            //Assert
            Assert.Equal(expectedTaker.Id, result.Id);
            Assert.Equal(expectedTaker.Name, result.Name);
            Assert.Equal(expectedTaker.Address, result.Address);
            Assert.Equal(expectedTaker.Email, result.Email);
            Assert.Equal(expectedTaker.Username, result.Username);
            Assert.Equal(expectedTaker.TakerType, result.TakerType);
            //Assert.Equal(expectedTaker.AssignedDate, result.AssignedDate);
            Assert.Equal(expectedTaker.Score, result.Score);
            //Assert.Equal(expectedTaker.TakenDate, result.TakenDate);
            //Assert.Equal(expectedTaker.FinishedDate, result.FinishedDate);
            Assert.Equal(expectedTaker.CanRetake, result.CanRetake);
        }

        //TakerAnswer to TakerAnswersDto
        [Fact]
        public void Map_ValidTakerAnswer_RetursTakerAnswersDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerAnswer = new TakerAnswer
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "TestAnswer"
            };

            var expectedTakerAnswer = new TakerAnswersDto
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "TestAnswer"
            };

            //Act
            var result = _mapper.Map<TakerAnswersDto>(takerAnswer);

            //Assert
            Assert.Equal(expectedTakerAnswer.Id, result.Id);
            Assert.Equal(expectedTakerAnswer.TakerId, result.TakerId);
            Assert.Equal(expectedTakerAnswer.QuizId, result.QuizId);
            Assert.Equal(expectedTakerAnswer.QuestionId, result.QuestionId);
            Assert.Equal(expectedTakerAnswer.Answer, result.Answer);
        }

        //TakerAnswer to TakerAnswerCreationDto
        [Fact]
        public void Map_ValidTakerAnswer_RetursTakerAnswerCreationDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerAnswer = new TakerAnswer
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "TestAnswer"
            };

            var expectedTakerAnswer = new TakerAnswerCreationDto
            {
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "TestAnswer"
            };

            //Act
            var result = _mapper.Map<TakerAnswerCreationDto>(takerAnswer);

            //Assert
            Assert.Equal(expectedTakerAnswer.TakerId, result.TakerId);
            Assert.Equal(expectedTakerAnswer.QuizId, result.QuizId);
            Assert.Equal(expectedTakerAnswer.QuestionId, result.QuestionId);
            Assert.Equal(expectedTakerAnswer.Answer, result.Answer);
        }

        //TakerAnswerCreationDto to TakerAnswer
        [Fact]
        public void Map_ValidTakerAnswerCreationDto_RetursTakerAnswer()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerAnswer = new TakerAnswerCreationDto
            {
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "TestAnswer"
            };

            var expectedTakerAnswer = new TakerAnswer
            {
                Id = id,
                TakerId = id,
                QuizId = id,
                QuestionId = id,
                Answer = "TestAnswer"
            };

            //Act
            var result = _mapper.Map<TakerAnswer>(takerAnswer);

            //Assert
            Assert.Equal(expectedTakerAnswer.Id, result.Id);
            Assert.Equal(expectedTakerAnswer.TakerId, result.TakerId);
            Assert.Equal(expectedTakerAnswer.QuizId, result.QuizId);
            Assert.Equal(expectedTakerAnswer.QuestionId, result.QuestionId);
            Assert.Equal(expectedTakerAnswer.Answer, result.Answer);
        }

        //Taker to TakerUserNameDto
        [Fact]
        public void Map_ValidTaker_RetursTakerAnswerCreationDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } }
            };

            var expectedTaker = new TakerUserNameDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
            };

            //Act
            var result = _mapper.Map<TakerUserNameDto>(taker);

            //Assert
            Assert.Equal(expectedTaker.Id, result.Id);
            Assert.Equal(expectedTaker.Name, result.Name);
            Assert.Equal(expectedTaker.Address, result.Address);
            Assert.Equal(expectedTaker.Email, result.Email);
            Assert.Equal(expectedTaker.Username, result.Username);
            Assert.Equal(expectedTaker.Password, result.Password);
            Assert.Equal(expectedTaker.TakerType, result.TakerType);
            //Assert.Equal(expectedTaker.CreatedDate, result.CreatedDate);
            //Assert.Equal(expectedTaker.UpdatedDate, result.UpdatedDate);
            Assert.Equal(expectedTaker.Quizzes.Count(), result.Quizzes.Count());
        }

        //Taker to TakerUpdateDto
        [Fact]
        public void Map_ValidTaker_RetursTakerUpdateDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } }
            };

            var expectedTaker = new TakerUpdateDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            //Act
            var result = _mapper.Map<TakerUpdateDto>(taker);

            //Assert
            Assert.Equal(expectedTaker.Id, result.Id);
            Assert.Equal(expectedTaker.Name, result.Name);
            Assert.Equal(expectedTaker.Address, result.Address);
            Assert.Equal(expectedTaker.Email, result.Email);
            Assert.Equal(expectedTaker.Username, result.Username);
            //Assert.Equal(expectedTaker.CreatedDate, result.CreatedDate);
            //Assert.Equal(expectedTaker.UpdatedDate, result.UpdatedDate);
        }

        //TakerQuizTaker to TakerQuizTakerDto
        [Fact]
        public void Map_ValidTakerQuizTaker_RetursTakerQuizTakerDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerQuizTaker = new TakerQuizTaker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            var expectedTakerQuizTaker = new TakerQuizTakerDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            //Act
            var result = _mapper.Map<TakerQuizTakerDto>(takerQuizTaker);

            //Assert
            Assert.Equal(expectedTakerQuizTaker.Id, result.Id);
            Assert.Equal(expectedTakerQuizTaker.Name, result.Name);
            Assert.Equal(expectedTakerQuizTaker.Address, result.Address);
            Assert.Equal(expectedTakerQuizTaker.Email, result.Email);
            Assert.Equal(expectedTakerQuizTaker.Username, result.Username);
            Assert.Equal(expectedTakerQuizTaker.Password, result.Password);
            Assert.Equal(expectedTakerQuizTaker.TakerType, result.TakerType);
            //Assert.Equal(expectedTakerQuizTaker.CreatedDate, result.CreatedDate);
            //Assert.Equal(expectedTakerQuizTaker.UpdatedDate, result.UpdatedDate);
            Assert.Equal(expectedTakerQuizTaker.Quizzes.Count(), result.Quizzes.Count());
            //Assert.Equal(expectedTakerQuizTaker.AssignedDate, result.AssignedDate);
            Assert.Equal(expectedTakerQuizTaker.Score, result.Score);
            //Assert.Equal(expectedTakerQuizTaker.TakenDate, result.TakenDate);
            //Assert.Equal(expectedTakerQuizTaker.FinishedDate, result.FinishedDate);
            Assert.Equal(expectedTakerQuizTaker.CanRetake, result.CanRetake);
        }

        //TakerQuizTaker to TakerUserNameDto
        [Fact]
        public void Map_ValidTakerQuizTaker_RetursTakerUserNameDto()
        {
            //Arrange
            var id = It.IsAny<int>();

            var takerQuizTaker = new TakerQuizTaker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };


            var expectedTaker = new TakerUserNameDto
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } }
            };

            //Act
            var result = _mapper.Map<TakerUserNameDto>(takerQuizTaker);

            //Assert
            Assert.Equal(expectedTaker.Id, result.Id);
            Assert.Equal(expectedTaker.Name, result.Name);
            Assert.Equal(expectedTaker.Address, result.Address);
            Assert.Equal(expectedTaker.Email, result.Email);
            Assert.Equal(expectedTaker.Username, result.Username);
            Assert.Equal(expectedTaker.Password, result.Password);
            Assert.Equal(expectedTaker.TakerType, result.TakerType);
            Assert.Equal(expectedTaker.Quizzes.Count(), result.Quizzes.Count());
        }

        //Taker to TakerQuizTaker
        [Fact]
        public void Map_ValidTaker_RetursTakerQuizTaker()
        {
            //Arrange
            var id = It.IsAny<int>();

            var taker = new Taker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                TakerAnswers = new List<TakerAnswer> { new TakerAnswer() { } }
            };

            var expectedTaker = new TakerQuizTaker
            {
                Id = id,
                Name = "TestName",
                Address = "TestAddress",
                Email = "TestEmail@gmail.com",
                Username = "TestUsername",
                Password = "TestPassword",
                TakerType = "T",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Quizzes = new List<QuizDto> { new QuizDto() { } },
                AssignedDate = DateTime.Now,
                Score = 0,
                TakenDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                CanRetake = 0
            };

            //Act
            var result = _mapper.Map<TakerQuizTaker>(taker);

            //Assert
            Assert.Equal(expectedTaker.Id, result.Id);
            Assert.Equal(expectedTaker.Name, result.Name);
            Assert.Equal(expectedTaker.Address, result.Address);
            Assert.Equal(expectedTaker.Email, result.Email);
            Assert.Equal(expectedTaker.Username, result.Username);
            Assert.Equal(expectedTaker.Password, result.Password);
            Assert.Equal(expectedTaker.TakerType, result.TakerType);
            Assert.Equal(expectedTaker.Quizzes.Count(), result.Quizzes.Count());
            Assert.Equal(expectedTaker.Score, result.Score);
            Assert.Equal(expectedTaker.CanRetake, result.CanRetake);
        }
    }
}
