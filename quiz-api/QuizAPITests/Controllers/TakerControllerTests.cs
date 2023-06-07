using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Frameworks;
using QuizApi.Controllers;
using QuizApi.Dtos.Question;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Models;
using QuizApi.Services.Quizzes;
using QuizApi.Services.Takers;


#pragma warning disable
namespace QuizAPITests.Controllers
{
    public class TakerControllerTests
    {
        private readonly TakersController _controller;
        private readonly Mock<ITakerService> _fakeTakerService;
        private readonly Mock<IQuizService> _fakeQuizService;
        private readonly Mock<ILogger<TakersController>> _fakeLogger;

        public TakerControllerTests()
        {
            _fakeTakerService = new Mock<ITakerService>();
            _fakeQuizService = new Mock<IQuizService>();
            _fakeLogger = new Mock<ILogger<TakersController>>();
            _controller = new TakersController(_fakeLogger.Object, _fakeTakerService.Object, _fakeQuizService.Object);
        }

        [Fact]
        public async Task Login_ReturnOk()
        {
            // Arrange 
            var taker = new TakerLoginDto
            {
                Username = "testtaker",
                Password = "testtaker"
            };

            var fakeTaker = new TakerUserNameDto
            {
                Name = "Test Taker",
                Address = "Test Address",
                Email = "test@example.com",
                Username = "testtaker",
                TakerType = "Test Type",
                Quizzes = new List<QuizDto> { new QuizDto { Name = "Quiz 1" }, new QuizDto { Name = "Quiz 2" } }
            };

            _fakeTakerService.Setup(service => service.GetTakerByUsername(taker.Username))
                .ReturnsAsync(fakeTaker);

            _fakeTakerService.Setup(service => service.VerifyPassword(fakeTaker.Password, taker.Password))
                .Returns(true);

            _fakeTakerService.Setup(service => service.CreateToken(It.IsAny<TakerUserNameDto>()))
                .Returns("Token"); 

            // Act
            var result = await _controller.Login(taker);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task Login_ReturnBadRequest()
        {
            //Arrange

            var taker = new TakerLoginDto
            {
                Username = "testtaker",
                Password = "testtaker"
            };

            _fakeTakerService.Setup(service => service.GetTakerByUsername(taker.Username))
                           .ReturnsAsync(new TakerUserNameDto());

            _fakeTakerService.Setup(service => service.VerifyPassword(taker.Password, It.IsAny<string>()))
                            .Returns(false);

            _fakeTakerService.Setup(service => service.CreateToken(It.IsAny<TakerUserNameDto>()));

            //Act
            var result = await _controller.Login(taker);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnNotFound()
        {

            //Arrange
            var taker = new TakerLoginDto
            {
                Username = "testtaker",
                Password = "testtaker"
            };

            _fakeTakerService.Setup(service => service.GetTakerByUsername(taker.Username))
                           .ReturnsAsync((TakerUserNameDto?)null);

            _fakeTakerService.Setup(service => service.VerifyPassword(taker.Password, It.IsAny<string>()))
                            .Returns(false);

            _fakeTakerService.Setup(service => service.CreateToken(It.IsAny<TakerUserNameDto>()));

            //Act
            var result = await _controller.Login(taker);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Login_ServerError_ReturnInternalServerError()
        {

            //Arrange
            var taker = new TakerLoginDto
            {
                Username = "testtaker",
                Password = "testtaker"
            };

            _fakeTakerService.Setup(service => service.GetTakerByUsername(taker.Username))
                             .ThrowsAsync(new Exception());

            _fakeTakerService.Setup(service => service.VerifyPassword(taker.Password , "testtaker"))
                            .Returns(true);

            _fakeTakerService.Setup(service => service.CreateToken(It.IsAny<TakerUserNameDto>()))
                             .Throws(new Exception());

            //Act
            var result = await _controller.Login(taker);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        //Create Taker returns ok
        [Fact]
        public async Task CreateTaker_Taker_ReturnsOk()
        {
            //Arrange
            var taker = new TakerCreationDto
            {
                Name = "Test Taker",
                Address = "Test Address",
                Email = "testtaker@gmail.com",
                Username = "testtaker",
                Password = "testtaker"
            };


            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                                .ReturnsAsync(true);

            _fakeTakerService.Setup(service => service.CreateTaker(taker))
                                .ReturnsAsync(new TakerDto());

            //Act
            var result = await _controller.CreateTaker(taker);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        //Create Taker with the same Username


        //Create Taker returns bad request
        [Fact]
        public async Task CreateTaker_BadRequest_ReturnsBadRequest()
        {
            //Arrange
            var taker = new TakerCreationDto
            {
                Name = "",
                Address = "Test Address",
                Email = "testtaker@gmail.com",
                Username = "testtaker",
                Password = "testtaker"
            };

            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                             .ReturnsAsync(false);

            _fakeTakerService.Setup(service => service.CreateTaker(taker))
                                .ReturnsAsync((TakerDto)null);

            //Act
            var result = await _controller.CreateTaker(taker);

            //Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(Assert.IsType<BadRequestResult>(result).StatusCode, 400);
        }

        //Create Taker returns internal server error
        [Fact]
        public async Task CreateTaker_ServerError_ReturnsInternalServerError()
        {
            //Arrange
            var taker = new TakerCreationDto
            {
                Name = "Test Taker",
                Address = "Test Address",
                Email = "testtaker@gmail.com",
                Username = "testtaker",
                Password = "testtaker"
            };

            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                            .ReturnsAsync(true);

            _fakeTakerService.Setup(service => service.CreateTaker(taker))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.CreateTaker(taker);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        //Get All Takers returns ok
        [Fact]
        public async Task GetAllTakers_HasTakers_ReturnsOk()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAllTakers())
                             .ReturnsAsync(new List<TakerDto> { new TakerDto() });

            //Act
            var result = await _controller.GetAllTakers(quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //Get All Takers no content
        [Fact]
        public async Task GetAllTakers_ReturnsNoContent()
        {
            //Arrange
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAllTakers())
                  .ReturnsAsync(new List<TakerDto>());


            //Act
            var result = await _controller.GetAllTakers(quizId);
            //Assert

            Assert.IsType<NoContentResult>(result);
        }

        //Get All Takers returns Not Found
        [Fact]
        public async Task GetAllTakers_NoTakersExist_ReturnsNotFound()
        {
            // Arrange
            var quizId = It.IsAny<int>();


            _fakeTakerService.Setup(service => service.GetAllTakers())
                             .ReturnsAsync((List<TakerDto>)null);

            // Act
            var result = await _controller.GetAllTakers(quizId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        //Get All Takers returns internal server error
        [Fact]
        public async Task GetAllTakers_ReturnsInternalServerError()
        {
            //Arrange
            var quizId = It.IsAny<int>();
            _fakeTakerService.Setup(service => service.GetAllTakers())
                             .ThrowsAsync(new Exception());
            //Act
            var result = await _controller.GetAllTakers(quizId);
            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        //GetTakerById returns ok
        [Fact]
        public async Task GetTakerById_TakerExists_ReturnsOk()
        {
            //Arrange
            var takerId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            //Act
            var result = await _controller.GetTaker(takerId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //GetTakerById returns not found
        [Fact]
        public async Task GetTakerById_TakerDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var takerId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync((TakerUserNameDto?)null);
            //Act
            var result = await _controller.GetTaker(takerId);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        //GetTakerById returns internal server error
        [Fact]
        public async Task GetTakerById_ReturnsInternalServerError()
        {
            //Arrange
            var takerId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ThrowsAsync(new Exception());
            //Act
            var result = await _controller.GetTaker(takerId);
            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        //GetTakerByUsername returns ok
        [Fact]
        public async Task GetTakerByUsername_TakerExists_ReturnsOk()
        {
            //Arrange
            var username = It.IsAny<string>();

            _fakeTakerService.Setup(service => service.GetTakerByUsername(username))
                             .ReturnsAsync(new TakerUserNameDto());

            //Act
            var result = await _controller.GetTakerByUsername(username);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //GetTakerByUsername returns not found
        [Fact]
        public async Task GetTakerByUsername_TakerDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var username = It.IsAny<string>();

            _fakeTakerService.Setup(service => service.GetTakerByUsername(username))
                             .ReturnsAsync((TakerUserNameDto?)null);

            //Act
            var result = await _controller.GetTakerByUsername(username);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        //GetTakerByUsername returns internal server error
        [Fact]
        public async Task GetTakerByUsername_ReturnsInternalServerError()
        {
            //Arrange
            var username = It.IsAny<string>();

            _fakeTakerService.Setup(service => service.GetTakerByUsername(username))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetTakerByUsername(username);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        //UpdateTaker returns ok
        [Fact]
        public async Task UpdateTaker_TakerExists_ReturnsOk()
        {
            //Arrange
            var takerId = It.IsAny<int>();
            var taker = new TakerCreationDto
            {
                Name = "Test Taker",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "test",
                Password = "test"
            };
            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                            .ReturnsAsync(true);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeTakerService.Setup(service => service.UpdateTaker(takerId, taker))
                             .ReturnsAsync(new TakerUserNameDto());

            //Act
            var result = await _controller.UpdateTaker(takerId, taker);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //UpdateTaker returns bad request
        [Fact]
        public async Task UpdateTaker_TakerIdDoesNotMatch_ReturnsBadRequest()
        {
            //Arrange
            var takerId = It.IsAny<int>();
            var taker = new TakerCreationDto
            {
                Name = "",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "test",
                Password = "test"
            };

            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                            .ReturnsAsync(false);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                            .ReturnsAsync( new TakerUserNameDto() );

            _fakeTakerService.Setup(service => service.UpdateTaker(takerId, taker))
                            .ReturnsAsync( new TakerUserNameDto() );

            //Act
            var result = await _controller.UpdateTaker(takerId, taker);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //UpdateTaker returns not found
        [Fact]
        public async Task UpdateTaker_TakerDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var takerId = It.IsAny<int>();
            var taker = new TakerCreationDto
            {
                Name = "Test Taker",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "test",
                Password = "test"
            };
            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                            .ReturnsAsync(true);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync((TakerUserNameDto?)null);

            _fakeTakerService.Setup(service => service.UpdateTaker(takerId, taker))
                             .ReturnsAsync(new TakerUserNameDto());

            //Act
            var result = await _controller.UpdateTaker(takerId, taker);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        //UpdateTaker returns internal server error
        [Fact]
        public async Task UpdateTaker_ReturnsInternalServerError()
        {
            //Arrange
            var takerId = It.IsAny<int>();
            var taker = new TakerCreationDto
            {
                Name = "Test Taker",
                Address = "Test Address",
                Email = "test@gmail.com",
                Username = "test",
                Password = "test"
            };

            _fakeTakerService.Setup(service => service.ValidateCreate(taker))
                            .ReturnsAsync(true);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeTakerService.Setup(service => service.UpdateTaker(takerId, taker))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.UpdateTaker(takerId, taker);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        //DeleteTaker returns ok
        [Fact]
        public async Task DeleteTaker_TakerExists_ReturnsOk()
        {
            //Arrange
            var takerId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeTakerService.Setup(service => service.DeleteTaker(takerId))
                             .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteTaker(takerId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //DeleteTaker returns not found
        [Fact]
        public async Task DeleteTaker_TakerDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var takerId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync((TakerUserNameDto?)null);

            _fakeTakerService.Setup(service => service.DeleteTaker(takerId))
                             .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteTaker(takerId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        //DeleteTaker returns internal server error
        [Fact]
        public async Task DeleteTaker_ReturnsInternalServerError()
        {
            //Arrange
            var takerId = It.IsAny<int>();
            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeTakerService.Setup(service => service.DeleteTaker(takerId))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.DeleteTaker(takerId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        [Fact]
        public async Task LetTakerTakeQuiz_ReturnOk()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();  

            _fakeTakerService.Setup(service => service.HasTakerTakenQuiz(takerId, quizId))
                             .ReturnsAsync(false);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeQuizService.Setup(service => service.GetQuizById(quizId))
                            .ReturnsAsync(new QuizDto());

            //Act
            var result = await _controller.LetTakerTakeQuiz(takerId, quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]  
        public async Task LetTakerTakeQuiz_ReturnBadRequest()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.HasTakerTakenQuiz(takerId, quizId))
                             .ReturnsAsync(true);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeQuizService.Setup(service => service.GetQuizById(quizId))
                            .ReturnsAsync(new QuizDto());

            //Act
            var result = await _controller.LetTakerTakeQuiz(takerId, quizId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task LetTakerTakeQuiz_ReturnNotFound()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.HasTakerTakenQuiz(takerId, quizId))
                             .ReturnsAsync(false);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync((TakerUserNameDto?)null);

            _fakeQuizService.Setup(service => service.GetQuizById(quizId))
                            .ReturnsAsync(new QuizDto());

            //Act
            var result = await _controller.LetTakerTakeQuiz(takerId, quizId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task LetTakerTakeQuiz_ReturnInernalServerError()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.HasTakerTakenQuiz(takerId, quizId))
                             .ReturnsAsync(false);

            _fakeTakerService.Setup(service => service.GetTakerById(takerId))
                             .ReturnsAsync(new TakerUserNameDto());

            _fakeQuizService.Setup(service => service.GetQuizById(quizId))
                            .ReturnsAsync(new QuizDto());

            _fakeTakerService.Setup(service => service.LetTakerTakeQuiz(takerId, quizId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.LetTakerTakeQuiz(takerId, quizId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        [Fact]
        public async Task GetAllAnswers_ReturnOk()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var questionId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerDetails(takerId, quizId, questionId))
                             .ReturnsAsync(new TakerAnswersDto());

            //Act
            var result = await _controller.GetAllAnswers(takerId, quizId, questionId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAnswers_ReturnNotFound()
        {
            //Arrange

            var takerId = 1;
            var quizId = It.IsAny<int>();
            var questionId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerDetails(takerId, quizId, questionId))
                             .ReturnsAsync((TakerAnswersDto) null);

            //Act
            var result = await _controller.GetAllAnswers(takerId, quizId, questionId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAnswer_ReturnInternalServerError()
        {

            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();
            var questionId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerDetails(takerId, quizId, questionId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetAllAnswers(takerId, quizId, questionId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        [Fact]
        public async Task TakerAnswersQuiz_ReturnOk()
        {

            // Arrange 

            var ans = new TakerAnswerCreationDto
            {
                TakerId = 1,
                QuestionId = 1,
                QuizId = 1
            };

            _fakeTakerService.Setup(service => service.GetAnswerDetails(ans.TakerId , ans.QuizId , ans.QuestionId) )
                                            .ReturnsAsync(new TakerAnswersDto());

            //Act
            var result = await _controller.TakerAnswersQuiz(ans);

            //Assert 
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TakerAnswersQuiz_ReturnBadRequest()
        {
            // Arrange 
            var ans = new TakerAnswerCreationDto();

            _fakeTakerService.Setup(service => service.GetAnswerDetails(ans.TakerId, ans.QuizId, ans.QuestionId))
                             .ReturnsAsync((TakerAnswersDto)null);

            // Act
            var result = await _controller.TakerAnswersQuiz(null);

            // Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task TakerAnswersQuiz_ReturnInternalServerError()
        {

            // Arrange 

            var ans = new TakerAnswerCreationDto
            {
                TakerId = 1,
                QuestionId = 1,
                QuizId = 1
            };

            _fakeTakerService.Setup(service => service.GetAnswerDetails(ans.TakerId, ans.QuizId, ans.QuestionId))
                                            .ThrowsAsync(new Exception() );

            //Act
            var result = await _controller.TakerAnswersQuiz(ans);

            //Assert 
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetTakerQuizScore_ReturnOk()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto());

            //Act
            var result = await _controller.GetTakerQuizScore(takerId, quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTakerQuizScore_ReturnNotFound()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync((TakerQuizDto) null);

            //Act
            var result = await _controller.GetTakerQuizScore(takerId, quizId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetTakerQuizScore_ReturnInternalServerError()
        {
            //Arrange

            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetTakerQuizScore(takerId, quizId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult) result).StatusCode);
        }

        [Fact]
        public async Task TakerUpdateTakenDate_ReturnOk()
        {

            //Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto());

            _fakeTakerService.Setup(service => service.TakerUpdateTakenDate(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto() );

            //Act
            var result = await _controller.TakerUpdateTakenDate(takerId, quizId);

            //Assert 
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TakerUpdateTakenDate_ReturnNotFound()
        {
            // Arrange 

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(0, 0))
                            .ReturnsAsync(null as TakerQuizDto);



            _fakeTakerService.Setup(service => service.TakerUpdateTakenDate(0, 0))
                             .ReturnsAsync( new TakerQuizDto() );

            // Act
            var result = await _controller.TakerUpdateTakenDate(0, 0);

            // Assert 
            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task TakerUpdateTakenDate_ReturnInternalServerError()
        {

            //Arrange
            var takerId = It.IsAny<int>();
            var quizId = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync(new  TakerQuizDto() );

            _fakeTakerService.Setup(service => service.TakerUpdateTakenDate(takerId, quizId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.TakerUpdateTakenDate(takerId, quizId);

            //Assert 
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task TakerUpdateFinishedDate_ReturnOk()
        {

            //Arrange 

            var takerId = 1;
            var quizId = 1;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto());

            _fakeTakerService.Setup(service => service.TakerUpdateFinishedDate(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto());

            //Act
            var result = await _controller.TakerUpdateFinishedDate(takerId, quizId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TakerUpdateFinishedDate_ReturnBadRequest ()
        {

            //Arrange 

            var takerId = 0;
            var quizId = 0;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto());

            _fakeTakerService.Setup(service => service.TakerUpdateFinishedDate(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto());

            //Act
            var result = await _controller.TakerUpdateFinishedDate(takerId, quizId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TakerUpdateFinishedDate_ReturnNotFound()
        {
            // Arrange 
            var takerId = 1;
            var quizId = 1;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync((TakerQuizDto)null);

            _fakeTakerService.Setup(service => service.TakerUpdateFinishedDate(takerId, quizId))
                             .ReturnsAsync((TakerQuizDto)null);

            // Act
            var result = await _controller.TakerUpdateFinishedDate(takerId, quizId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Taker with Id = {takerId} is not found", notFoundResult.Value);
        }




        [Fact]
        public async Task TakerUpdateFinishedDate_ReturnInternalServerError()
        {
            //Arrange 

            var takerId = 1;
            var quizId = 1;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                             .ReturnsAsync(new TakerQuizDto() );

            _fakeTakerService.Setup(service => service.TakerUpdateFinishedDate(takerId, quizId))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.TakerUpdateFinishedDate(takerId, quizId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateTakerRetake_ReturnOk()
        {
            // Arrange
            var takerId = 1;
            var quizId = 1;
            var retake = 0;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                            .ReturnsAsync(new TakerQuizDto());

            _fakeTakerService.Setup(service => service.UpdateTakerRetake(takerId, quizId, retake))
                            .ReturnsAsync(new TakerQuizDto());

            // Act 
            var result = await _controller.UpdateTakerRetake(retake, takerId, quizId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task UpdateTakerRetake_ReturnNotFound()
        {
            //Arrange
            var takerId = 1;
            var quizId = 1;
            var retake = 0;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                            .ReturnsAsync( (TakerQuizDto) null);

            _fakeTakerService.Setup(service => service.UpdateTakerRetake(takerId, quizId, retake))
                            .ReturnsAsync(new TakerQuizDto());

            //Act 
            var result = await _controller.UpdateTakerRetake(takerId, quizId, retake);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateTakerRetake_ReturnsInternalServerError()
        {
            // Arrange
            var takerId = 1;
            var quizId = 1;
            var retake = 1;

            _fakeTakerService.Setup(service => service.GetTakerQuizScore(takerId, quizId))
                            .ReturnsAsync(new TakerQuizDto());

            _fakeTakerService.Setup(service => service.UpdateTakerRetake(takerId, quizId, retake))
                            .ThrowsAsync(new Exception());

            // Act 
            var result = await _controller.UpdateTakerRetake(retake, takerId, quizId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }




        [Fact]
        public async Task DeleteAnswer_ReturnOk()
        {
            //Arrange 

            var id = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerById(id))
                             .ReturnsAsync(new TakerAnswersDto());

            _fakeTakerService.Setup(service => service.DeleteAnswer(id))
                            .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteAnswer(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAnswer_ReturnNotFound()
        {
            //Arrange 

            var id = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerById(id))
                             .ReturnsAsync( (TakerAnswersDto) null);

            _fakeTakerService.Setup(service => service.DeleteAnswer(id))
                            .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteAnswer(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAnswer_ReturnInternalServerError()
        {
            //Arrange 

            var id = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerById(id))
                             .ReturnsAsync(new TakerAnswersDto());

            _fakeTakerService.Setup(service => service.DeleteAnswer(id))
                            .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.DeleteAnswer(id);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAnswerById_ReturnOk()
        {

            //Arrange

            var id = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerById(id))
                             .ReturnsAsync(new TakerAnswersDto());

            //Act
            var result = await _controller.GetAnswerById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAnswerById_ReturnNotFound()
        {

            //Arrange

            var id = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerById(id))
                             .ReturnsAsync( (TakerAnswersDto) null);

            //Act
            var result = await _controller.GetAnswerById(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAnswerById_ReturnInternalServerError()
        {

            //Arrange

            var id = It.IsAny<int>();

            _fakeTakerService.Setup(service => service.GetAnswerById(id))
                             .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetAnswerById(id);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
