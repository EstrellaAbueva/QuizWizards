using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuizApi.Controllers;
using QuizApi.Dtos.Question;
using QuizApi.Models;
using QuizApi.Services.Questions;
using QuizApi.Services.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAPITests.Controllers
{
    public class QuestionControllerTests
    {
        private readonly QuestionsController _controller;
        private readonly Mock<IQuestionService> _fakeQuestionService;
        private readonly Mock<ITopicService> _fakeTopicService;
        private readonly Mock<ILogger<QuestionsController>> _fakeLogger;

        public QuestionControllerTests()
        {
            _fakeQuestionService = new Mock<IQuestionService>();
            _fakeTopicService = new Mock<ITopicService>();
            _fakeLogger = new Mock<ILogger<QuestionsController>>();
            _controller = new QuestionsController(_fakeQuestionService.Object, _fakeLogger.Object, _fakeTopicService.Object);
        }

        //CreateQuestion Ok
        [Fact]
        public async Task CreateQuestion_Question_ReturnsOk()
        { 
            //Arrange
            var question = new QuestionCreationDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer =  "Paris"
            };

            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(true);

            _fakeQuestionService.Setup(service => service.CreateQuestion(question))
                                .ReturnsAsync(new QuestionDto());

            //Act
            var result = await _controller.CreateQuestion(question);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        //Create Question BadRequest
        [Fact]
        public async Task CreateQuestion_ReturnsBadRequest()
        {
            // Arrange
            var question = new QuestionCreationDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer = ""
            };

            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(false);

            _fakeQuestionService.Setup(service => service.CreateQuestion(question))
                                .ReturnsAsync((QuestionDto?)null);

            // Act
            var result = await _controller.CreateQuestion(question);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }


        //Create Question InternalServerError
        [Fact]
        public async Task CreateQuestion_ServerError_ReturnsInternalServerError()
        {
            //Arrange
            var question = new QuestionCreationDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer = "Paris"
            };

            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(true);

            _fakeQuestionService.Setup(service => service.CreateQuestion(question))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.CreateQuestion(question);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        //GetAllQuestions Ok
        [Fact]
        public async Task GetAllQuestions_HasQuestions_ReturnsOk()
        {
            //Arrange
            var topicId = It.IsAny<int>();

            _fakeQuestionService.Setup(service => service.GetAllQuestions())
                                .ReturnsAsync(new List<QuestionDto> { new QuestionDto() });

            //Act
            var result = await _controller.GetAllQuestion(topicId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }


        //GetAllQuestions No Content
        [Fact]
        public async Task GetAllQuestions_NoQuestionsFound_ReturnsNoContent()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            _fakeQuestionService.Setup(service => service.GetAllQuestionsByTopicId(topicId))
                                .ReturnsAsync((List<QuestionDto>)null);

            //Act
            var result = await _controller.GetAllQuestion(topicId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        //GetAllQuestions Not Found

        //GetAllQuestions InternalServerError
        [Fact]
        public async Task GetAllQuestions_ServerError_ReturnsInternalServerError()
        {
            //Arrange
            var topicId = It.IsAny<int>();
            _fakeQuestionService.Setup(service => service.GetAllQuestions())
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetAllQuestion(topicId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        //GetQuestionById Ok
        [Fact]
        public async Task GetQuestionById_QuestionExists_ReturnsOk()
        {
            //Arrange
            var questionId = It.IsAny<int>();

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync(new QuestionDto());

            //Act
            var result = await _controller.GetQuestionById(questionId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //GetQuestionById NotFound
        [Fact]
        public async Task GetQuestionById_NonExistingQuestion_ReturnsNotFound()
        {
            //Arrange
            var questionId = It.IsAny<int>();
            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync((QuestionDto)null);

            //Act
            var result = await _controller.GetQuestionById(questionId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        //GetQuestionById InternalServerError
        [Fact]
        public async Task GetQuestionById_ServerError_ReturnsInternalServerError()
        {
            //Arrange
            var questionId = It.IsAny<int>();
            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetQuestionById(questionId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        //UpdateQuestion Ok
        [Fact]
        public async Task UpdateQuestion_UpdatedQuestion_ReturnsOk()
        {
            //Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionUpdateDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer = "Paris"
            };

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync(new QuestionDto());

            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(true);

            _fakeQuestionService.Setup(service => service.UpdateQuestion(questionId, question))
                                .ReturnsAsync(new QuestionDto());

            //Act
            var result = await _controller.UpdateQuestion(questionId, question);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //UpdateQuestion BadRequest
        [Fact]
        public async Task UpdateQuestion_ReturnsBadRequest()
        {
            //Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionUpdateDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer = ""
            };

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync(new QuestionDto());

            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(false);

            _fakeQuestionService.Setup(service => service.UpdateQuestion(questionId, question))
                                .ReturnsAsync((QuestionDto)null);

            //Act
            var result = await _controller.UpdateQuestion(questionId, question);

            //Assert
            var objectResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        //UpdateQuestion NotFound
        [Fact]
        public async Task UpdateQuestion_QuestionNotFound_ReturnsNotFound()
        {
            //Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionUpdateDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer = "Paris"
            };
            
            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(true);

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync((QuestionDto)null);

            //Act
            var result = await _controller.UpdateQuestion(questionId, question);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }

        //UpdateQuestion InternalServerError
        [Fact]
        public async Task UpdateQuestion_ServerError_ReturnsInternalServerError()
        {
            //Arrange
            var questionId = It.IsAny<int>();
            var question = new QuestionUpdateDto
            {
                Question = "What is the capital of France?",
                CorrectAnswer = "Paris"
            };
            
            _fakeQuestionService.Setup(service => service.ValidateQuestion(question))
                                .ReturnsAsync(true);

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ThrowsAsync(new Exception());

            _fakeQuestionService.Setup(service => service.UpdateQuestion(questionId, question))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.UpdateQuestion(questionId, question);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        //DeleteQuestion Ok
        [Fact]
        public async Task DeleteQuestion_HasQuestion_ReturnsOk()
        {
            //Arrange
            var questionId = It.IsAny<int>();

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync(new QuestionDto()); ;

            //Act
            var result = await _controller.DeleteQuestion(questionId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //DeleteQuestion NotFound
        [Fact]
        public async Task DeleteQuestion_DeletedQuestion_ReturnsNotFound()
        {
            //Arrange
            var questionId = It.IsAny<int>();

            _fakeQuestionService.Setup(service => service.DeleteQuestion(questionId))
                                .ReturnsAsync(false);

            //Act
            var result = await _controller.DeleteQuestion(questionId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ((NotFoundObjectResult)result).StatusCode);
        }

        //DeleteQuestion InternalServerError
        [Fact]
        public async Task DeleteQuestion_ServerError_ReturnsInternalServerError()
        {
            //Arrange
            var questionId = It.IsAny<int>();

            _fakeQuestionService.Setup(service => service.GetQuestionById(questionId))
                                .ReturnsAsync(new QuestionDto());
            _fakeQuestionService.Setup(service => service.DeleteQuestion(questionId))
                                .ThrowsAsync(new Exception());

            //Act
            var result = await _controller.DeleteQuestion(questionId);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
