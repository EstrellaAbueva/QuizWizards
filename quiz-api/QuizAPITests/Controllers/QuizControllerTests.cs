using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuizApi.Controllers;
using QuizApi.Dtos.QuizD;
using QuizApi.Services.Quizzes;

namespace QuizAPITests.Controllers
{
    public class QuizControllerTests
    {
        private readonly QuizzesController _controller;
        private readonly Mock<IQuizService> _fakeQuizService;
        private readonly Mock<ILogger<QuizzesController>> _fakeLogger;

        public QuizControllerTests()
        {
            _fakeQuizService = new Mock<IQuizService>();
            _fakeLogger = new Mock<ILogger<QuizzesController>>();
            _controller = new QuizzesController(_fakeQuizService.Object, _fakeLogger.Object);
        }

        [Fact]
        public async Task CreateQuiz_ReturnOk()
        {
            //Arrange
            var quiz = new QuizCreationDto()
            {
                Name = "Animal-Bio",
                Description = "Animal Biology",
                TopicId = new List<string>() { "1", "2" }
            };
            _fakeQuizService.Setup(service => service.CheckQuizValidation(quiz))
                            .ReturnsAsync(true);

            _fakeQuizService.Setup(service => service.CreateQuiz(quiz))
                                .ReturnsAsync(new QuizDto());

            //Act
            var result = await _controller.CreateQuiz(quiz);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);

        }

        [Fact]
        public async Task CreateQuiz_ReturnBadRequest()
        {
            // Arrange
            var invalidQuiz = new QuizCreationDto()
            {
                Name = "",
                Description = "",
                TopicId = new List<string>() { "1", "2" }
            };

            _fakeQuizService.Setup(service => service.CheckQuizValidation(invalidQuiz))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.CreateQuiz(invalidQuiz);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            var badRequestResult = result as BadRequestResult;
            Assert.Equal(400, badRequestResult.StatusCode);
        }


        [Fact]
        public async Task CreateQuiz_ReturnInternalServerError()
        {
            //Arrange
            var quiz = new QuizCreationDto()
            {
                Name = "Animal-Bio",
                Description = "Animal Biology",
                TopicId = new List<string>() { "1", "2" }
            };
            _fakeQuizService.Setup(service => service.CheckQuizValidation(quiz))
                            .ReturnsAsync(true);

            _fakeQuizService.Setup(service => service.CreateQuiz(quiz))
                            .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateQuiz(quiz);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllQuizzes_ReturnOk()
        {
            // Arrange
            _fakeQuizService.Setup(service => service.GetAllQuizzes())
                             .ReturnsAsync(new List<QuizDto>());

            // Act
            var result = await _controller.GetAllQuizzes();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllQuizzes_ReturnNotFound()
        {
            //Arrange
            _fakeQuizService.Setup(service => service.GetAllQuizzes())
                             .ReturnsAsync((IEnumerable<QuizDto>)null);

            //Act 
            var result = await _controller.GetAllQuizzes();

            //Assert
            var objectResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, objectResult.StatusCode);

        }

        [Fact]
        public async Task GetAllQuizzes_ReturnInternalServerError()
        {
            // Arrange
            _fakeQuizService.Setup(service => service.GetAllQuizzes())
                             .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllQuizzes();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetQuizzesById_ReturnOk()
        {
            //Arrange 
            var quizId = It.IsAny<int>();
               
            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                            .ReturnsAsync(true);
            _fakeQuizService.Setup(service => service.GetQuizById(quizId))
                            .ReturnsAsync(new QuizDto());

            //Act
            var result = await _controller.GetQuizbyId(quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetQuizzesById_ReturnNotFound()
        {
            // Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(false);

            // Act
            var result = await _controller.GetQuizbyId(quizId);

            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Quiz with Id {quizId} not found", objectResult.Value);
        }


        [Fact]
        public async Task GetQuizzesById_ReturnInternalServerError()
        {
            //Arrange
            var quizId = It.IsAny<int>();
            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                            .ReturnsAsync(true);
            _fakeQuizService.Setup(service => service.GetQuizById(quizId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetQuizbyId(quizId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateQuiz_ReturnOk()
        {
            //Arrange

            var quizId = It.IsAny<int>();
            var quiz = new QuizCreationDto()
            {
                Name = "Animal-Bio",
                Description = "Animal Biology",
                TopicId = new List<string>() { "1", "2" }
            };

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(true);
            _fakeQuizService.Setup(service => service.UpdateQuiz(quizId, quiz))
                             .ReturnsAsync(new QuizDto());

            //Act 
            var result = await _controller.UpdateQuiz(quizId, quiz);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task UpdateQuiz_ReturnsNotFound()
        {
            // Arrange
            var quizId = It.IsAny<int>();
            var quiz = new QuizCreationDto()
            {
                Name = "Animal-Bio",
                Description = "Animal Biology",
                TopicId = new List<string>() { "1", "2" }
            };

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateQuiz(quizId, quiz);

            // Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Quiz with Id = {quizId} not found", objectResult.Value);
        }


        [Fact]
        public async Task UpdateQuiz_ReturnsInternalServerError()
        {
            //Arrange
            var quizId = It.IsAny<int>();
            var quiz = new QuizCreationDto()
            {
                Name = "Animal-Bio",
                Description = "Animal Biology",
                TopicId = new List<string>() { "1", "2" }
            };

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                                .ReturnsAsync(true);

            _fakeQuizService.Setup(service => service.UpdateQuiz(quizId, quiz))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.UpdateQuiz(quizId , quiz);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteQuiz_ReturnOk()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteQuiz(quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteQuiz_ReturnNotFound()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(false);

            //Act
            var result = await _controller.DeleteQuiz(quizId);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Quiz with Id = {quizId} not found", objectResult.Value);
        }

        [Fact]
        public async Task DeleteQuiz_ReturnInternalError()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(true);
            _fakeQuizService.Setup(service => service.DeleteQuiz(quizId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.DeleteQuiz(quizId);
            
            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnOk()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(true);
            _fakeQuizService.Setup(service => service.GetQuizLeaderboardById(quizId))
                .ReturnsAsync(new List<QuizLeaderboard>());

            //Act
            var result = await _controller.GetLeaderboard(quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnNotFound()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(false);

            //Act
            var result = await _controller.GetLeaderboard(quizId);

            //Assert
            var objectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal($"Quiz with Id = {quizId} not found", objectResult.Value);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnInternalServerError()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeQuizService.Setup(service => service.CheckQuizById(quizId))
                             .ReturnsAsync(true);

            _fakeQuizService.Setup(service => service.GetQuizLeaderboardById(quizId))
                                    .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetLeaderboard(quizId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
