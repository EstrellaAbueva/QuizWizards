using AutoMapper;
using QuizApi.Dtos.Topic;
using QuizApi.Mappings;
using QuizApi.Models;

namespace QuizAPITests.Mapping
{
    public class TopicMappingTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public TopicMappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TopicMappings>();
            });
            _mapper = _configuration.CreateMapper();
        }

        //TopicCreationDto to Topic
        [Fact]
        public void Map_ValidTopicCreationDto_ReturnTopic()
        {
            //Arrange
            var topicCreationDto = new TopicCreationDto()
            {
                Name = "Test Topic",
                QuestionId = new List<string>()
                {
                    "1",
                    "2"
                }
            };

            var expectedTopic = new Topic()
            {
                Id = 1,
                Name = "Test Topic",
                QuestionId = new List<string>()
                {
                    "1",
                    "2"
                },
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 2,
                Questions = new List<Problem> { new Problem() { } }
            };

            //Act
            var mapping = _mapper.Map<Topic>(topicCreationDto);

            //Assert
            Assert.Equal(expectedTopic.Name, mapping.Name);
            Assert.Equal(expectedTopic.QuestionId, mapping.QuestionId);
        }

        //Topic to TopicDto
        [Fact]
        public void Map_ValidTopic_ReturnTopicDto()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 5, 24, 15, 30, 0);

            var topic = new Topic()
            {
                Id = 1,
                Name = "Test Topic",
                QuestionId = new List<string>()
        {
            "1",
            "2"
        },
                CreatedDate = dateTime,
                UpdatedDate = dateTime,
                NumberOfQuestions = 2,
                Questions = new List<Problem> { new Problem() { } }
            };

            var expectedTopicDto = new TopicDto()
            {
                Id = 1,
                Name = "Test Topic",
                CreatedDate = dateTime,
                UpdatedDate = dateTime,
                NumberOfQuestions = 2,
                Questions = new List<Problem> { new Problem() { } }
            };

            // Act
            var mapping = _mapper.Map<TopicDto>(topic);

            // Assert
            Assert.Equal(expectedTopicDto.Id, mapping.Id);
            Assert.Equal(expectedTopicDto.Name, mapping.Name);
            //Assert.Equal(expectedTopicDto.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedTopicDto.UpdatedDate, mapping.UpdatedDate);
            Assert.Equal(expectedTopicDto.NumberOfQuestions, mapping.NumberOfQuestions);
            Assert.Equal(expectedTopicDto.Questions.Count(), mapping.Questions.Count());
        }


        ////Topic to Problem
        [Fact]
        public void Map_ValidTopic_ReturnProblem()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 5, 24, 15, 30, 0);

            var topic = new Topic()
            {
                Id = 1,
                Name = "Test Name",
                QuestionId = new List<string>()
                {
                    "1",
                    "2"
                },
                CreatedDate = dateTime,
                UpdatedDate = dateTime,
                NumberOfQuestions = 2,
                Questions = new List<Problem> { new Problem() }
            };

            var expectedProblem = new Problem()
            {
                Id = 1,
                Question = "Test Question",
                CorrectAnswer = "Test Answer",
                CreatedDate = dateTime,
                UpdatedDate = dateTime,
            };

            // Act
            var mapping = _mapper.Map<Problem>(topic);

            // Assert
            Assert.Equal(expectedProblem.Id, mapping.Id);
            //Assert.Equal(expectedProblem.CreatedDate, mapping.CreatedDate);
            //Assert.Equal(expectedProblem.UpdatedDate, mapping.UpdatedDate);
        }
    }
}
