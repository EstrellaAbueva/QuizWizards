using AutoMapper;
using Moq;
using QuizApi.Dtos.Question;
using QuizApi.Mappings;
using QuizApi.Models;

namespace QuizAPITests.Mapping
{
    public class QuestionMappingTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public QuestionMappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<QuestionMappings>();
            });
            _mapper = _configuration.CreateMapper();
        }


        //Problem to QuestionDto
        [Fact]
        public void Map_ValidProblem_ReturnQuestionDto()
        {
            //Arrange
            DateTime dateTime = new DateTime(2023, 5, 24, 15, 30, 0);

            var problem = new Problem()
            {
                Id = 1,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = dateTime,
                UpdatedDate = dateTime
            };

            var expectedQuestionDto = new QuestionDto()
            {
                Id = 1,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = dateTime,
                UpdatedDate = dateTime
            };

            //Act
            var mapping = _mapper.Map<QuestionDto>(problem);

            //Assert
            Assert.Equal(expectedQuestionDto.Id, mapping.Id);
            Assert.Equal(expectedQuestionDto.Question, mapping.Question);
            Assert.Equal(expectedQuestionDto.CorrectAnswer, mapping.CorrectAnswer);
            Assert.Equal(expectedQuestionDto.CreatedDate, mapping.CreatedDate);
            Assert.Equal(expectedQuestionDto.UpdatedDate, mapping.UpdatedDate);
        }

        //QuestionUpdateDto to Problem
        [Fact]
        public void Map_ValidQuestionUpdateDto_ReturnProblem()
        {
            //Arrange
            DateTime dateTime = new DateTime(2023, 5, 24, 15, 30, 0);
            var id = It.IsAny<int>();
            var questionUpdateDto = new QuestionUpdateDto()
            {
                Question = "Test Question",
                CorrectAnswer = "Test Answer"
            };

            var expectedProblem = new Problem()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate =  dateTime,
                UpdatedDate = dateTime
            };

            //Act
            var mapping = _mapper.Map<Problem>(questionUpdateDto);

            //Assert
            Assert.Equal(expectedProblem.Id, mapping.Id);
            Assert.Equal(expectedProblem.Question, mapping.Question);
            Assert.Equal(expectedProblem.CorrectAnswer, mapping.CorrectAnswer);
            //Assert.Equal(expectedProblem.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedProblem.UpdatedDate, mapping.UpdatedDate);
        }

        //QuestionCreationDto to Problem
        [Fact]
        public void Map_ValidQuestionCreationDto_ReturnProblem()
        {
            //Arrange
            DateTime dateTime = new DateTime(2023, 5, 24, 15, 30, 0);
            var id = It.IsAny<int>();

            var questionCreationDto = new QuestionCreationDto()
            {
                Question = "Test Question",
                CorrectAnswer = "Test Answer"
            };

            var expectedProblem = new Problem()
            {
                Id = id,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = dateTime,
                UpdatedDate = dateTime
            };

            //Act
            var mapping = _mapper.Map<Problem>(questionCreationDto);

            //Assert
            Assert.Equal(expectedProblem.Question, mapping.Question);
            Assert.Equal(expectedProblem.CorrectAnswer, mapping.CorrectAnswer);
            //Assert.Equal(expectedProblem.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedProblem.UpdatedDate, mapping.UpdatedDate);
        }
    }
}
