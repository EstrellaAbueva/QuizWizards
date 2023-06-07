using AutoMapper;
using Moq;
using QuizApi.Dtos.Question;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using QuizApi.Repositories.Topics;
using QuizApi.Services.Topics;

namespace QuizAPITests.Services
{
    public class ITopicServiceTests
    {
        private readonly ITopicService _TopicService;
        private readonly Mock<ITopicRepository> _fakeTopicRepository;
        private readonly Mock<IMapper> _fakeMapper;


        public ITopicServiceTests()
        {
            _fakeTopicRepository = new Mock<ITopicRepository>();
            _fakeMapper = new Mock<IMapper>();
            _TopicService = new TopicService(_fakeTopicRepository.Object, _fakeMapper.Object);
        }

        //CreateTopic returns Topic
        [Fact]
        public async Task CreateTopic_WithValidTopic_ReturnsTopic()
        {
            //Arrange
            var id = It.IsAny<int>();
            var topicToCreate = new TopicCreationDto
            {
                Name = "test",
                QuestionId = new List<string> { "1" }
            };

            var model = new Topic
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                QuestionId = new List<string> { "1"}, 
                NumberOfQuestions = 1,
                Questions = new List<Problem> { 
                    new Problem()
                    {
                        Id = id,
                        Question = "Test Question",
                        CorrectAnswer = "Answer",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    } 
                }
            };

            var topic = new TopicDto
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                    new Problem()
                    {
                        Id = id,
                        Question = "Test Question",
                        CorrectAnswer = "Answer",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Topic>(topicToCreate)).Returns(model);
            _fakeTopicRepository.Setup(m => m.CreateTopic(model)).ReturnsAsync(1);
            _fakeMapper.Setup(m => m.Map<TopicDto>(model)).Returns(topic);

            //Act
            var result = await _TopicService.CreateTopic(topicToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(topic, result);
        }

        //CreateTopic returns null
        [Fact]
        public async Task CreateTopic_WhenCalled_ReturnsNull()
        {
            //Arrange
            var id = It.IsAny<int>();
            var topicToCreate = new TopicCreationDto
            {
                Name = "test",
                QuestionId = new List<string> { "1" }
            };

            var model = new Topic
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                QuestionId = new List<string> { "1" },
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                    new Problem()
                    {
                        Id = id,
                        Question = "Test Question",
                        CorrectAnswer = "Answer",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    }
                }
            };

            var topic = new TopicDto
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                    new Problem()
                    {
                        Id = id,
                        Question = "Test Question",
                        CorrectAnswer = "Answer",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Topic>(topicToCreate)).Returns(model);
            _fakeTopicRepository.Setup(m => m.CreateTopic(model)).ReturnsAsync(-2);

            //Act
            var result = await _TopicService.CreateTopic(topicToCreate);

            //Assert
            Assert.Null(result);
        }

        //CreateTopic ThrowsException
        [Fact]
        public async Task CreateTopic_WhenCalled_ThrowsException()
        {
            //Arrange
            var id = It.IsAny<int>();
            var topicToCreate = new TopicCreationDto
            {
                Name = "test",
                QuestionId = new List<string> { "1" }
            };

            var model = new Topic
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                QuestionId = new List<string> { "1" },
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                    new Problem()
                    {
                        Id = id,
                        Question = "Test Question",
                        CorrectAnswer = "Answer",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    }
                }
            };

            var topic = new TopicDto
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                    new Problem()
                    {
                        Id = id,
                        Question = "Test Question",
                        CorrectAnswer = "Answer",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    }
                }
            };

            _fakeMapper.Setup(m => m.Map<Topic>(topicToCreate)).Returns(model);
            _fakeTopicRepository.Setup(m => m.CreateTopic(model))
                                .Throws(new Exception("Database connection error"));

            //Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TopicService.CreateTopic(topicToCreate));

            //Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetAllTopics returns AllTopic
        [Fact]
        public async Task GetAllTopics_WithValidTopic_ReturnsAllTopics()
        {
            // Arrange
            var id = It.IsAny<int>();
            var topics = new List<TopicDto>
            {
                new TopicDto
                {
                    Id = id,
                    Name = "test",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    NumberOfQuestions = 1,
                    Questions = new List<Problem> {
                        new Problem()
                        {
                            Id = id,
                            Question = "Test Question",
                            CorrectAnswer = "Answer",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        }
                    }
                }
            };

            var topic2 = new List<Topic>
            {
                new Topic
                {
                    Id = id,
                    Name = "test",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    QuestionId = new List<string> { "1" },
                    NumberOfQuestions = 1,
                    Questions = new List<Problem> {
                        new Problem()
                        {
                            Id = id,
                            Question = "Test Question",
                            CorrectAnswer = "Answer",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        }
                    }
                }
            };

            _fakeTopicRepository.Setup(m => m.GetAllTopics()).ReturnsAsync(topic2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TopicDto>>(topic2)).Returns(topics);

            //Act
            var result = await _TopicService.GetAllTopics();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TopicDto>>(result);
            Assert.Equal(topics.Count(), result.Count());
        }

        //GetAllTopics returns Empty
        [Fact]
        public async Task GetAllTopics_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var id = It.IsAny<int>();
            var topics = new List<TopicDto>();
            var topic2 = new List<Topic>();

            _fakeMapper.Setup(m => m.Map<IEnumerable<Topic>>(topics)).Returns(topic2);
            _fakeTopicRepository.Setup(m => m.GetAllTopics()).ReturnsAsync(topic2);
            _fakeMapper.Setup(m => m.Map<IEnumerable<TopicDto>>(topics)).Returns(topics);

            //Act
            var result = await _TopicService.GetAllTopics();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TopicDto>>(result);
            Assert.Empty(result);
        }

        //GetAllTopics ThrowsException
        [Fact]
        public async Task GetAllTopics_WhenCalled_ThrowsException()
        {
            // Arrange
            var id = It.IsAny<int>();
            var topics = new List<TopicDto>();
            var topic2 = new List<Topic>();

            _fakeMapper.Setup(m => m.Map<IEnumerable<Topic>>(topics)).Returns(topic2);
            _fakeTopicRepository.Setup(m => m.GetAllTopics())
                                .Throws(new Exception("Database connection error"));


            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TopicService.GetAllTopics());

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //GetTopicById return Topic
        [Fact]
        public async Task GetTopicById_WithValidTopic_ReturnsTopic()
        {
            // Arrange
            var id = It.IsAny<int>();
            var topic = new TopicDto
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                        new Problem()
                        {
                            Id = id,
                            Question = "Test Question",
                            CorrectAnswer = "Answer",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        }
                    }
            };

            var topic2 = new Topic
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                QuestionId = new List<string> { "1" },
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                        new Problem()
                        {
                            Id = id,
                            Question = "Test Question",
                            CorrectAnswer = "Answer",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        }
                    }
            };

            _fakeMapper.Setup(m => m.Map<Topic>(topic)).Returns(topic2);
            _fakeTopicRepository.Setup(m => m.GetTopic(1)).ReturnsAsync(topic2);
            _fakeMapper.Setup(m => m.Map<TopicDto>(topic2)).Returns(topic);

            //Act
            var result = await _TopicService.GetTopicById(1);

            //Assert
            Assert.Equal(topic, result);
            Assert.NotNull(result);
            Assert.IsType<TopicDto>(result);
        }

        //GetTopicById returns Null
        [Fact]
        public async Task GetTopicById_WhenCalled_ReturnsNull()
        {
            // Arrange
            var topic = new TopicDto();
            var topic2 = new Topic();

            _fakeMapper.Setup(m => m.Map<Topic>(topic)).Returns(topic2);
            _fakeTopicRepository.Setup(m => m.GetTopic(1)).ReturnsAsync(topic2);
            _fakeMapper.Setup(m => m.Map<TopicDto>(topic2)).Returns(topic);

            //Act
            var result = await _TopicService.GetTopicById(1);

            //Assert
            Assert.Equal(topic, result);
        }

        //GetTopicById ThrowsException
        [Fact]
        public async Task GetTopicById_WhenCalled_ThrowsException()
        {
            // Arrange
            var id = It.IsAny<int>();
            _fakeTopicRepository.Setup(m => m.GetTopic(id))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TopicService.GetTopicById(id));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //UpdateTopic returns TopicDto
        [Fact]
        public async Task UpdateTopic_WhenCalled_ReturnsTopicDto()
        {
            // Arrange
            var id = It.IsAny<int>();
            var topic = new TopicDto
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                        new Problem()
                        {
                            Id = id,
                            Question = "Test Question",
                            CorrectAnswer = "Answer",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        }
                    }
            };

            var model = new Topic
            {
                Id = id,
                Name = "test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                QuestionId = new List<string> { "1" },
                NumberOfQuestions = 1,
                Questions = new List<Problem> {
                        new Problem()
                        {
                            Id = id,
                            Question = "Test Question",
                            CorrectAnswer = "Answer",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        }
                    }
            };

            var updateTopic = new TopicCreationDto
            {
                Name = "test",
                QuestionId = new List<string> { "1" }
            };

            _fakeMapper.Setup(m => m.Map<Topic>(updateTopic)).Returns(model);
            _fakeTopicRepository.Setup(m => m.UpdateTopic(model)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<TopicDto>(model)).Returns(topic);

            // Act
            var result = await _TopicService.UpdateTopic(id, updateTopic);

            // Assert
            Assert.Equal(topic, result);
            Assert.NotNull(result);
            Assert.IsType<TopicDto>(result);
        }

        //UpdateTopic returns Null
        [Fact]
        public async Task UpdateTopic_WhenCalled_ReturnsNull()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            var topicToUpdate = new TopicDto();
            var topic = new Topic();
            var updateTopic = new TopicCreationDto();

            _fakeMapper.Setup(m => m.Map<Topic>(updateTopic)).Returns(topic);
            _fakeTopicRepository.Setup(m => m.UpdateTopic(topic)).ReturnsAsync(true);
            _fakeMapper.Setup(m => m.Map<TopicDto>(topic)).Returns(topicToUpdate);

            // Act
            var result = await _TopicService.UpdateTopic(topicId, updateTopic);   
                
            // Assert
            Assert.Equal(topicToUpdate, result);
        }

        //UpdateTopic ThrowsException
        [Fact]
        public async Task UpdateTopic_WhenCalled_ThrowsException()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            var topicToUpdate = new TopicDto();
            var topic = new Topic();
            var updateTopic = new TopicCreationDto();

            _fakeMapper.Setup(m => m.Map<Topic>(updateTopic)).Returns(topic);
            _fakeTopicRepository.Setup(m => m.UpdateTopic(topic))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TopicService.UpdateTopic(topicId, updateTopic));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //DeleteTopic returns True
        [Fact]
        public async Task DeleteTopic_WithValidTopic_ReturnsTrue()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            _fakeTopicRepository.Setup(m => m.DeleteTopic(topicId)).ReturnsAsync(true);

            // Act
            var result = await _TopicService.DeleteTopic(topicId);
            
            // Assert
            Assert.True(result);
        }

        //DeleteTopic returns False
        [Fact]
        public async Task DeleteTopic_WithValidTopic_ReturnsFalse()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            _fakeTopicRepository.Setup(m => m.DeleteTopic(topicId)).ReturnsAsync(false);

            // Act
            var result = await _TopicService.DeleteTopic(topicId);

            // Assert
            Assert.False(result);
        }

        //DeleteTopic ThrowsException
        [Fact]
        public async Task DeleteTopic_WhenCalled_ThrowsException()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            _fakeTopicRepository.Setup(m => m.DeleteTopic(topicId))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TopicService.DeleteTopic(topicId));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }

        //ValidateTopic returns True
        [Fact]
        public async Task ValidateTopic_WhenCalled_ReturnsTrue()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            var topic = new TopicCreationDto();

            _fakeTopicRepository.Setup(m => m.ValidateTopic(topic)).ReturnsAsync(true);

            // Act
            var result = await _TopicService.ValidateTopic(topic);

            // Assert
            Assert.True(result);
        }

        //ValidateTopic returns False
        [Fact]
        public async Task ValidateTopic_WhenCalled_ReturnsFalse()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            var topic = new TopicCreationDto();

            _fakeTopicRepository.Setup(m => m.ValidateTopic(topic)).ReturnsAsync(false);

            // Act
            var result = await _TopicService.ValidateTopic(topic);

            // Assert
            Assert.False(result);
        }

        //ValidateTopic ThrowsException
        [Fact]
        public async Task ValidateTopic_WhenCalled_ThrowsException()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            var topic = new TopicCreationDto();

            _fakeTopicRepository.Setup(m => m.ValidateTopic(topic))
                                .Throws(new Exception("Database connection error"));

            // Act
            var result = await Assert.ThrowsAsync<Exception>(() => _TopicService.ValidateTopic(topic));

            // Assert
            Assert.Equal("Database connection error", result.Message);
        }
    }
}
