using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuizApi.Controllers;
using QuizApi.Dtos.Question;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using QuizApi.Services.Questions;
using QuizApi.Services.Quizzes;
using QuizApi.Services.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPITests.Controllers
{
    public class TopicsControllerTests
    {
        private readonly TopicsController _controller;
        private readonly Mock<IQuizService> _fakeQuizService;
        private readonly Mock<ITopicService> _fakeTopicService;
        private readonly Mock<ILogger<TopicsController>> _fakeLogger;

        public TopicsControllerTests()
        {
            _fakeQuizService = new Mock<IQuizService>();
            _fakeTopicService = new Mock<ITopicService>();
            _fakeLogger = new Mock<ILogger<TopicsController>>();
            _controller = new TopicsController(_fakeTopicService.Object, _fakeLogger.Object, _fakeQuizService.Object);
        }

        [Fact]
        public async Task CreateTopic_ReturnsOk()
        {
            //Arrange
            var topic = new TopicCreationDto()
            {
                Name = "Animal-Bio",
                QuestionId = new List<string>() { "1", "2" }
            };
            _fakeTopicService.Setup(service => service.ValidateTopic(topic))
                                .ReturnsAsync(true);
            _fakeTopicService.Setup(service => service.CreateTopic(topic))
                                .ReturnsAsync(new TopicDto());

            //Act
            var result = await _controller.CreateTopic(topic);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateTopic_ReturnsBadRequest()
        {
            // Arrange
            var topic = new TopicCreationDto()
            {
                Name = null,
                QuestionId = new List<string>() { "1", "2" }
            };

            _fakeTopicService.Setup(service => service.ValidateTopic(topic))
                                .ReturnsAsync(false);

            // Act
            var result = await _controller.CreateTopic(topic);

            // Assert
            var objectResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }


        [Fact]
        public async Task CreateTopic_ReturnsInternalServerError()
        {
            //Arrange
            var topic = new TopicCreationDto()
            {
                Name = "Animal-Bio",
                QuestionId = new List<string>() { "1", "2" }
            };

            _fakeTopicService.Setup(service => service.ValidateTopic(topic))
                                .ReturnsAsync(true);
            _fakeTopicService.Setup(service => service.CreateTopic(topic))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.CreateTopic(topic);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllTopic_ReturnOk()
        {
            // Arrange
            _fakeTopicService.Setup(service => service.GetAllTopics())
                             .ReturnsAsync(new List<TopicDto>());

            // Act
            var result = await _controller.GetAllTopics();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllTopic_ReturnNotFound()
        {
            //Arrange
            _fakeTopicService.Setup(service => service.GetAllTopics())
                             .ReturnsAsync((IEnumerable<TopicDto>)null);

            //Act 
            var result = await _controller.GetAllTopics();

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task GetAllTopic_ReturnInternalServerError()
        {
            // Arrange
            _fakeTopicService.Setup(service => service.GetAllTopics())
                             .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllTopics();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTopicById_ReturnOk()
        {
            //Arrange 
            var topicId = It.IsAny<int>();
            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync(new TopicDto());

            //Act
            var result = await _controller.GetTopic(topicId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTopic_ReturnsNotFound()
        {
            // Arrange
            var topicId = It.IsAny<int>();

            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync((TopicDto)null);

            // Act
            var result = await _controller.GetTopic(topicId);

            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Topic with id {topicId} does not exist", objectResult.Value);
        }


        [Fact]
        public async Task GetTopicById_ReturnInternalServerError()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetTopic(topicId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateTopic_ReturnOk()
        {
            // Arrange 
            var topicId = It.IsAny<int>();
            var topic = new TopicCreationDto()
            {
                Name = "Animal-Bio",
                QuestionId = new List<string>() { "1", "2" }
            };
            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync(new TopicDto() );

            _fakeTopicService.Setup(service => service.UpdateTopic(topicId, topic))
                             .ReturnsAsync(new TopicDto() );

            // Act 
            var result = await _controller.UpdateTopic(topicId, topic);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateTopic_ReturnsNotFound()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            var topic = new TopicCreationDto()
            {
                Name = "Animal-Bio",
                QuestionId = new List<string>() { "1", "2" }
            };

            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync((TopicDto)null); // Simulate topic not found

            // Act
            var result = await _controller.UpdateTopic(topicId, topic);

            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Topic with Id = {topicId} is not found", objectResult.Value);
        }


        [Fact]
        public async Task UpdateTopic_ReturnsInternalServerError()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            var topic = new TopicCreationDto()
            {
                Name = "Animal-Bio",
                QuestionId = new List<string>() { "1", "2" }
            };

            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                                .ReturnsAsync(new TopicDto());
            _fakeTopicService.Setup(service => service.UpdateTopic(topicId , topic))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.UpdateTopic(topicId , topic);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteTopic_ReturnsOk()
        {
            // Arrange
            var topicId = It.IsAny<int>();
            var topic = new TopicDto();

            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync(topic);
            _fakeTopicService.Setup(service => service.DeleteTopic(topicId))
                             .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteTopic(topicId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal($"Topic with Id = {topicId} was Successfully Deleted", okResult.Value);
        }

        [Fact]
        public async Task DeleteTopic_ReturnsInternalServerError()
        {
            // Arrange
            var topicId = It.IsAny<int>();

            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync(new TopicDto());
            _fakeTopicService.Setup(service => service.DeleteTopic(topicId))
                             .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.DeleteTopic(topicId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Something went wrong", objectResult.Value);
        }


        [Fact]
        public async Task DeleteTopic_ReturnsNotFound()
        {
            // Arrange
            var topicId = It.IsAny<int>();

            _fakeTopicService.Setup(service => service.GetTopicById(topicId))
                             .ReturnsAsync((TopicDto)null);

            // Act
            var result = await _controller.DeleteTopic(topicId);

            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Topic with Id = {topicId} is not found", objectResult.Value);
        }


    }
}
