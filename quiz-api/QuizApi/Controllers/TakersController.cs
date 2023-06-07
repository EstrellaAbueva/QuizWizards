using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Dtos.Taker;
using QuizApi.Services.Quizzes;
using QuizApi.Services.Takers;
using QuizApi.Dtos.QuizD;

namespace QuizApi.Controllers
{

#pragma warning disable
    [Route("api/takers")]
    [ApiController]
    public class TakersController : ControllerBase
    {
        private readonly ILogger<TakersController> _logger;
        private readonly ITakerService _takerService;
        private readonly IQuizService _quizService;

        public TakersController(
            ILogger<TakersController> logger, 
            ITakerService takerService, 
            IQuizService quizService)
        {
            _logger = logger;
            _takerService = takerService;
            _quizService = quizService;
        }

        /// <summary>
        /// Log in to QuizWizard
        /// </summary>
        /// <param name="takerLogin">The user to login</param>
        /// <returns>Token</returns>
        /// <remarks>
        /// Sample Request:
        ///     
        ///     POST /api/Takers/login
        ///     {
        ///         "username": "ahoka",
        ///         "password": "estrellaAbueva"
        ///     }
        ///     
        /// </remarks>
        /// 
        /// <response code="201">Successfully login an account</response>
        /// <response code="400">Details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] TakerLoginDto takerLogin)
        {
            try
            {
                var taker = await _takerService.GetTakerByUsername(takerLogin.Username);

                if (taker == null)
                    return NotFound($"Taker {takerLogin.Username} is not found");

                if (!_takerService.VerifyPassword(taker.Password, takerLogin.Password))
                {
                    return BadRequest("Wrong password!");
                }
                    

                string token = _takerService.CreateToken(taker);
                return Ok(token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }


        /// <summary>
        /// Creates a taker
        /// </summary>
        /// <param name="taker">Taker Details</param>
        /// <returns>Returns the newly created taker</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Takers
        ///     {
        ///         "name" : "Jhonray Asohedo",
        ///         "address" : "Talisay City, Cebu",
        ///         "email" : "jhonray.asohedo@gmail.com"
        ///         "username" : "john2",
        ///         "password" : "thisisapassword"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a taker</response>
        /// <response code="400">Taker details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("signup")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTaker([FromBody] TakerCreationDto taker)
        {
            try
            {
                bool isValid = await _takerService.ValidateCreate(taker);

                if(!isValid)
                    return BadRequest();

                var newTaker = await _takerService.CreateTaker(taker);
                return CreatedAtRoute("GetTakerById", new { id = newTaker.Id }, newTaker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all takers or Gets all takers assigned to a quiz
        /// </summary>
        /// <param name="quizId">Quiz Id</param>
        /// <returns>Returns all takers</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Takers
        ///     [
        ///         {
        ///             "id": 1,
        ///             "name": "John Doe",
        ///             "address": "N. Bacalso Ave, Cebu City",
        ///             "email": "johndoe@gmail.com",
        ///             "username": "john2",
        ///             "createdDate": "1899-12-22T00:00:00",
        ///             "updatedDate": "0001-01-01T00:00:00"
        ///         }
        ///     ]
        ///     
        ///     GET api/Takers?quizId=1
        ///     {
        ///       "id": 1,
        ///       "name": "John Doe",
        ///       "address": "N. Bacalso Ave, Cebu City",
        ///       "email": "johndoe@gmail.com",
        ///       "username": "john2",
        ///       "createdDate": "1899-12-22T00:00:00",
        ///       "updatedDate": "0001-01-01T00:00:00"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved takers</response>
        /// <response code="204">There are no takers</response>
        /// <response code="404">Quiz with the given quizId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllTakers")]
        [Authorize(Roles = "A")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TakerQuizDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTakers([FromQuery] int quizId)
        {
            try
            {
                var takers = await _takerService.GetAllTakers();
                var quiz = await _takerService.GetAllTakers(quizId);

                if (takers == null)
                {
                    return NotFound($"Quiz with Id = {quizId} is not found");
                }

                if (takers.IsNullOrEmpty())
                {
                    return NoContent();
                }

                if (quizId == 0)
                    return Ok(takers);
                else
                    return Ok(quiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets taker
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <returns>Returns taker</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Takers/2
        ///     {
        ///         "id": 1,
        ///         "name": "John Doe",
        ///         "address": "N. Bacalso Ave, Cebu City",
        ///         "email": "johndoe@gmail.com",
        ///         "username": "john2",
        ///         "takerType": "T",
        ///         "assignedDate": "2023-05-18T13:03:29.1603014+08:00",
        ///         "score": 0,
        ///         "takenDate": null,
        ///         "finishedDate": null,
        ///         "quizzes": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Math Quiz",
        ///                 "description": "Calculus quiz",
        ///                 "createdDate": "2023-05-18T13:03:29.1598183+08:00",
        ///                 "updatedDate": null,
        ///                 "topics": []
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "name": "Science Quiz",
        ///                 "description": "Quiz on life of animals",
        ///                 "createdDate": "2023-05-18T13:03:29.1598261+08:00",
        ///                 "updatedDate": null,
        ///                 "topics": []
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved taker</response>
        /// <response code="404">Taker with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetTakerById")]
        [Authorize(Roles = "A")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizzesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaker(int id)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);

                if (taker == null)
                    return NotFound($"Taker with id {id} does not exist");
                
               return Ok(taker);
            }   
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets taker by username
        /// </summary>
        /// <param name="username">The username to get</param>
        /// <returns>Return a TakerUserNameDto</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/Takers/ahoka/details
        ///     {
        ///       "id": 4,
        ///       "name": "Estrella Abueva",
        ///       "address": "Carlock",
        ///       "email": "estrellaabueva@gmail.com",
        ///       "username": "ahoka",
        ///       "password": "estrellaAbueva",
        ///       "takerType": "A",
        ///       "createdDate": "1899-12-22T00:00:00",
        ///       "updatedDate": "0001-01-01T00:00:00",
        ///       "quizzes": []
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved taker by username</response>
        /// <response code="404">Taker is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{username}/details", Name = "GetTakerByUsername")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerUserNameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTakerByUsername(string username)
        {
            try
            {
                var taker = await _takerService.GetTakerByUsername(username);

                if (taker == null)
                    return NotFound($"Taker with Username = {username} is not found");

                return Ok(taker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates a taker
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <param name="takerToBeUpdated">Taker update details</param>
        /// <returns>Returns the newly updated taker</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Takers
        ///     {
        ///         "name" : "Jhonray Acohedo",
        ///         "address" : "Talisay City, Cebu",
        ///         "email" : "jhonray.acohedo@gmail.com",
        ///         "username" : "john2",
        ///         "password" : "thisisapassword"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully updated a taker</response>
        /// <response code="400">Taker details are invalid</response>
        /// <response code="404">Taker is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "A,T")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerUserNameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTaker(int id, [FromBody] TakerCreationDto takerToBeUpdated)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);
                bool isValid = await _takerService.ValidateCreate(takerToBeUpdated);

                if (!isValid)
                    return BadRequest("Invalid taker details");

                if (taker == null)
                    return NotFound($"Taker with Id = {id} is not found");

                var updatedTaker = await _takerService.UpdateTaker(id, takerToBeUpdated);

                return Ok(updatedTaker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes taker
        /// </summary>
        /// <param name="id">Taker Id</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Takers/1
        ///             Taker with Id = 1 was Successfully Deleted
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted taker</response>
        /// <response code="404">Taker with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "A")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTaker(int id)
        {
            try
            {
                var taker = await _takerService.GetTakerById(id);
               
                if (taker == null)
                    return NotFound($"Taker with Id = {id} is not found");

                await _takerService.DeleteTaker(id);
                return Ok($"Taker with Id = {id} was Successfully Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Lets taker take quizzes
        /// </summary>
        /// <param name="takerId">Taker Id</param>
        /// <param name="quizId">Quiz Id</param>
        /// <returns>Returns taker with newly taken quiz</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Takers/takeQuiz?takerId=3quizId=2
        ///     {
        ///         "id": 3,
        ///         "name": "Stephine Doe",
        ///         "address": "Cebu City",
        ///         "email": "stephine.doe@gmail.com",
        ///         "quizzes": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Math Quiz",
        ///                 "description": "Calculus quiz",
        ///             }
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully assigned taker to a quiz</response>
        /// <response code="400">Taker has already taken the Quiz</response>
        /// <response code="404">Taker with the given takerId or Quiz with the given quizId is not found or both are not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("assign", Name = "LetTakerTakeQuiz")]
        [Authorize(Roles = "A")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizzesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LetTakerTakeQuiz([FromQuery] int takerId, int quizId)
        {
            try
            {
                var taker = await _takerService.GetTakerById(takerId);
                var quiz = await _quizService.GetQuizById(quizId);
                var hasTakerTakenQuiz = await _takerService.HasTakerTakenQuiz(takerId, quizId);

                if(taker == null && quiz == null)
                    return NotFound($"Taker with Id = {takerId} and Quiz with Id = {quizId} are not found");
                else if (taker == null)
                    return NotFound($"Taker with Id = {takerId} is not found");
                else if (quiz == null)
                    return NotFound($"Quiz with Id = {quizId} is not found");

                if (hasTakerTakenQuiz)
                    return BadRequest($"Taker with Id = {takerId} has already taken Quiz with Id = {quizId}");

                await _takerService.LetTakerTakeQuiz(takerId, quizId);
                var takerWithQuiz = await _takerService.GetTakerWithQuizById(takerId);

                return Ok(takerWithQuiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all answers of all taker or a specific taker
        /// </summary>
        /// <param name="takerId">Specific taker to get answer</param>
        /// <param name="quizId">Specific quiz to get answer</param>
        /// <param name="questionId">Specific Question that the Taker answered</param>
        /// <returns>Returns TakerAnswersDto</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/Takers/answer
        ///     
        ///     GET /api/Takers/answer?takerId=1
        ///     [
        ///          {
        ///            "id": 1,
        ///            "takerId": 1,
        ///            "questionId": 1,
        ///            "answer": "True"
        ///          },
        ///          {
        ///            "id": 6,
        ///            "takerId": 1,
        ///            "questionId": 4,
        ///            "answer": "hello"
        ///          }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved answer/s</response>
        /// <response code="404">There are no answers is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("answer", Name = "GetAllAnswers")]
        [Authorize(Roles = "A")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerAnswersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnswers([FromQuery] int takerId, int quizId, int questionId)
        {
            try
            {
                var answers = await _takerService.GetAllAnswers();
                var taker = await _takerService.GetAnswerDetails(takerId, quizId, questionId);

                if (taker == null && takerId != 0)
                    return NotFound($"TakerAnswer with Id = {takerId} is not found");

                if (takerId != 0)
                    return Ok(taker);
                else 
                    return Ok(answers);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Taker answers a quiz
        /// </summary>
        /// <param name="answer">Answer of the Taker</param>
        /// <returns>Returns a TakerAnswersDto</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /api/Takers/recordAnswer
        ///     {
        ///         "takerId": 1,
        ///         "quizId":  1
        ///         "questionId": 1,
        ///         "answer": "True"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Successfully saved answer/s</response>
        /// <response code="404">Answer details invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("recordAnswer", Name = "TakerAnswersQuiz")]
        [Authorize(Roles = "T")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerAnswersDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TakerAnswersQuiz([FromBody] TakerAnswerCreationDto answer)
        {
            try
            {
                if(answer == null)
                    return BadRequest("Answer details invalid");

                var ans = await _takerService.TakerAnswersQuiz(answer);

                var indiv = await _takerService.GetAnswerDetails(answer.TakerId, answer.QuizId, answer.QuestionId);
                return Ok (indiv);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets the score of the taker in a specific quiz.
        /// </summary>
        /// <param name="takerId">Id of the Taker.</param>
        /// <param name="quizId">Id of the Quiz.</param>
        /// <returns>Returns TakerQuizDto.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /api/Takers/quizScore?takerId=1&amp;quizId=1
        ///     {
        ///         "id": 1,
        ///         "takerId": 1,
        ///         "quizId": 1,
        ///         "assignedDate": "1899-12-22T00:00:00",
        ///         "score": 0,
        ///         "takenDate": "2023-05-18T10:10:35.263",
        ///         "finishedDate": "2023-05-17T12:55:27.97",
        ///         "canRetake": 1
        ///     }
        /// 
        /// </remarks>  
        /// <response code="200">Successfully retrieved TakerQuiz details.</response>
        /// <response code="404">Taker with the specific quiz does not exist.</response>
        /// <response code="500">Internal server error.</response>

        [HttpGet("quizScore", Name = "GetTakerQuizScore")]
        [Authorize(Roles = "A,T")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTakerQuizScore([FromQuery] int takerId, int quizId)
        {
            try
            {
               
                var taker = await _takerService.GetTakerQuizScore(takerId, quizId);

                if (taker == null)
                {
                    return NotFound($"Taker with Id = {takerId} is not found");
                }

                return Ok(taker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates the Taken Date of the Taker
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>Returns TakerQuizDto</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     PUT /api/Takers/startquiz?takerId=1&amp;quizId=1
        ///     
        /// </remarks>
        /// <response code="200">Successfully updated a TakenDate</response>
        /// <response code="404">Taker with specific quizId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("startQuiz", Name = "TakerUpdateTakenDate")]
        [Authorize(Roles = "T")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TakerUpdateTakenDate([FromQuery] int takerId, int quizId)
        {
            try
            {
                var takers = await _takerService.GetTakerQuizScore(takerId, quizId);

                if (takers == null)
                {
                    return NotFound($"Taker with Id = {takerId} is not found");
                }


                var taker = await _takerService.TakerUpdateTakenDate(takerId, quizId);

                return Ok($"Taker with Id = {takerId} was Successfully Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates the Finished Date of the Taker
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>Returns TakerQuizDto</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     PUT /api/Takers/endquiz?takerId=1&amp;quizId=1
        ///     
        /// </remarks>
        /// <response code="200">Successfully updated a FinishedDate</response>
        /// <response code="404">Taker with specific quizId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("endQuiz", Name = "TakerUpdateFinishedDate")]
        [Authorize(Roles = "T")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TakerUpdateFinishedDate([FromQuery] int takerId, int quizId)
        {
            try
            {

                var takerQuizScore = await _takerService.GetTakerQuizScore(takerId, quizId);
                if (takerQuizScore == null)
                    return NotFound($"Taker with Id = {takerId} is not found");


                if (takerId == 0 || quizId == 0)
                    return BadRequest($"Taker with Id = {takerId} is not found");

                var taker = await _takerService.TakerUpdateFinishedDate(takerId, quizId);

                return Ok($"Taker with Id = {takerId} was Successfully Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Taker can now retake the quiz
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>Returns TakerQuizDto</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     PUT /api/Takers/retake?retake=1&amp;takerId=1&amp;quizId=1
        ///     
        /// </remarks>
        /// <response code="200">Taker can now retake a specific quiz</response>
        /// <response code="404">Taker with specific quizId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("retake", Name = "UpdateTakerRetake")]
        [Authorize(Roles = "T , A")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTakerRetake([FromQuery] int retake, int takerId, int quizId)
        {
            try
            {
                var score = await _takerService.GetTakerQuizScore(takerId, quizId);

                if (score == null)
                    return NotFound($"Taker with Id = {takerId} is not found");

                var taker = await _takerService.UpdateTakerRetake(retake, takerId, quizId);

                return Ok($"Taker with Id = {takerId} was Successfully Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }



        [HttpDelete("deleteAnswer")]
        [Authorize(Roles = "A , T")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAnswer([FromQuery] int id)
        {
            try
            {
                var answer = await _takerService.GetAnswerById(id);

                if (answer == null)
                    return NotFound($"Taker with Id = {id} is not found");

                await _takerService.DeleteAnswer(id);
                return Ok($"Taker with Id = {id} was Successfully Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("answer/{id}", Name = "GetAnswerById")]
        [Authorize(Roles = "A, T")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TakerUserNameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            try
            {
                var taker = await _takerService.GetAnswerById(id);

                if (taker == null)
                    return NotFound($"Taker with Id = {id} is not found");

                return Ok(taker);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
