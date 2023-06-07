using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Dtos.QuizD;
using QuizApi.Services.Quizzes;

namespace QuizApi.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizzesController : Controller
    {
        private readonly IQuizService _quizService;
        private readonly ILogger<QuizzesController> _logger;

        public QuizzesController(
            IQuizService QuizService, 
            ILogger<QuizzesController> logger)
        {
            _quizService = QuizService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a quiz
        /// </summary>
        /// <param name="quiz">Quiz Details</param>
        /// <returns>Returns the newly created quiz</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Quizzes
        ///     {
        ///         "name" : "Math Quiz",
        ///         "description" : "Simple adding and subtracting quiz!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a quiz</response>
        /// <response code="400">Quiz details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/JSON")]
        [Produces("application/JSON")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuizDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizCreationDto quiz)
        {
            try
            {
                bool isValid = await _quizService.CheckQuizValidation(quiz);
                if (!isValid)
                {
                    return BadRequest();
                }

                var newQuiz = await _quizService.CreateQuiz(quiz);
                return CreatedAtRoute("GetQuizById", new { id = newQuiz.Id }, newQuiz);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        /// <summary>
        /// Gets all quizzes or gets all Quizzes that a Topic is assigned to or gets all Quizzes that a Taker has answered
        /// </summary>
        /// <returns>Returns all quizzes or returns all quizzes that a topic is assigned to or
        /// returns all quizzes that a taker has answered</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Quizzes
        ///     {
        ///         "id": 1,
        ///         "name" : "Quiz1",
        ///         "description" : "Hard Quiz",
        ///     }
        ///     GET api/Quizzes?TopicId=2
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",     
        ///         "topicName": "Bio"
        ///     }
        ///     
        ///     GET api/Quizzes?TakerId=2
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",     
        ///         "takerName": "John Quiz"
        ///     }
        /// </remarks>
        /// <response code="200">Successfully retrieved quizzes</response>
        /// <response code="204">There are no quizzes available</response>
        /// <response code="400">Both Topic id and Taker Id are supplied</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllQuizzes")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(QuizTopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllQuizzes()
        {
            try
            {
                var quizzes = await _quizService.GetAllQuizzes();

                if (quizzes == null)
                {
                    return NotFound();
                }

                return Ok(quizzes);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets quiz by Id only or with topics
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <returns>Returns quiz only or with topics</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Quizzes/1
        ///     {
        ///         "id": 1,
        ///         "name" : "Quiz1",
        ///         "description" : "Hard Quiz"
        ///     }
        ///     
        ///     GET /api/Quizzes/2
        ///     {
        ///         "id": 2,
        ///         "name": "Quiz 2",
        ///         "description": "Hard quiz",
        ///         "topics": [
        ///              {
        ///                "id": 1,
        ///                "name": "Math"
        ///              },
        ///              {
        ///                "id": 2,
        ///                "name": "Bio"
        ///              }
        ///           ]   
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved quiz</response>
        /// <response code="204">Quiz with the given id is not found</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetQuizById")] 
        [Produces("application/json")]
        [Authorize(Roles = "A,T")]
        [ProducesResponseType(typeof(QuizTopicsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuizbyId(int id)
        {
            try
            {
                bool doesQuizExist = await _quizService.CheckQuizById(id);
                if (!doesQuizExist)
                {
                    return NotFound($"Quiz with Id {id} not found");
                }
                    var quiz = await _quizService.GetQuizById(id);
                        return Ok(quiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates a quiz
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <param name="quiz">Quiz to update details</param>
        /// <returns>Returns the newly updated quiz</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Quizzes
        ///     {
        ///         "name" : "Quiz1",
        ///         "description" : "Hard Quiz"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully updated a quiz</response>
        /// <response code="204">Quiz is not found</response>
        /// <response code="400">Quiz details are invalid</response>
        /// <response code="404">Quiz is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizCreationDto quiz)
        {
            try
            {
                bool doesQuizExist = await _quizService.CheckQuizById(id);
                if (!doesQuizExist)
                {
                    return NotFound($"Quiz with Id = {id} not found");
                }

                var updatedQuiz = await _quizService.UpdateQuiz(id, quiz);
                return Ok(updatedQuiz);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500,  "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a quiz
        /// </summary>
        /// <param name="id">Quiz Id</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Quizzes/1
        ///            Delete Successful!
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted quiz</response>
        /// <response code="204">Quiz is not found</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                bool doesQuizExist = await _quizService.CheckQuizById(id);
                if (!doesQuizExist)
                {
                    return NotFound($"Quiz with Id = {id} not found");
                }
                await _quizService.DeleteQuiz(id);
                return Ok("Delete Successful!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("leaderboard")]
        [Produces("application/json")]
        [Authorize(Roles = "A,T")]
        [ProducesResponseType(typeof(QuizLeaderboard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLeaderboard(int id)
        {
            try
            {
                bool doesQuizExist = await _quizService.CheckQuizById(id);
                if (!doesQuizExist)
                {
                    return NotFound($"Quiz with Id = {id} not found");
                } 
                else
                {
                    var leaderboard = await _quizService.GetQuizLeaderboardById(id);
                    return Ok(leaderboard);
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
