using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Dtos.Question;
using QuizApi.Services.Questions;
using QuizApi.Services.Topics;

namespace QuizApi.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionsController> _logger;
        private readonly ITopicService _topicService;

        public QuestionsController(
            IQuestionService QuestionService, 
            ILogger<QuestionsController> logger,
            ITopicService topicService)
        {
            _questionService = QuestionService;
            _logger = logger;
            _topicService = topicService;
        }

        /// <summary>
        /// Creates a question
        /// </summary>
        /// <param name="question">Question Details</param>
        /// <returns>Returns the newly created question</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/questions
        ///        {
        ///          "id": 1,
        ///          "topicId": 1,
        ///          "question": "1+1=3",
        ///          "correctAnswer": "false"
        ///        }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a question</response>
        /// <response code="400">Question details are invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost] 
        [Consumes("application/JSON")]
        [Produces("application/JSON")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreationDto question)
        {
            try
            {
                bool isValid = await _questionService.ValidateQuestion(question);
                if (!isValid)
                    return BadRequest();

                var newQuestion = await _questionService.CreateQuestion(question);
                return CreatedAtRoute("GetQuestionById", new { id = newQuestion }, newQuestion);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all questions or Gets all questions by topic id
        /// </summary>
        /// <param name="topicId">Topic Id</param>
        /// <returns>Returns all questions</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Questions
        ///     {
        ///         "id": 1,
        ///         "topicId": 1,
        ///         "question": "1+1=3",
        ///         "correctAnswer": "false"
        ///     },
        ///     {
        ///         "id": 2,
        ///         "topicId": 2,
        ///         "question": "Earth is oblate spheroid",
        ///         "correctAnswer": "true"
        ///     },
        ///     {
        ///         "id": 3,
        ///         "topicId": 3,
        ///         "question": "Solid is hard",
        ///         "correctAnswer": "true"
        ///     }
        ///     GET api/Questions?topicId=2
        ///     {
        ///         "id": 1,
        ///         "topicId": 1,
        ///         "question": "1+1=3",
        ///         "correctAnswer": "false"
        ///     },
        ///     {
        ///         "id": 4,
        ///         "topicId": 1,
        ///         "question": "2+2=22",
        ///         "correctAnswer": "false"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Successfully retrieved questions</response>
        /// <response code="204">There is no questions</response>
        /// <response code="404">Topic with the given topicId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllQuestions")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllQuestion([FromQuery] int topicId)
        {
            try
            {
                var questions = await _questionService.GetAllQuestions();
                var topic = await _topicService.GetTopicById(topicId);
                var questionsWithTopicId = await _questionService.GetAllQuestionsByTopicId(topicId);

                if (topic == null && topicId != 0)
                    return NotFound($"Topic id {topicId} is not found");

                if (topicId == 0 && questions.IsNullOrEmpty())
                    return NoContent();

                if (topicId == 0)
                    return Ok(questions);
                else
                    return Ok(questionsWithTopicId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets question
        /// </summary>
        /// <returns>Returns question with the given id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Questions/2
        ///     {
        ///         "id": 2,
        ///         "topicId": 2,
        ///         "question": "Earth is oblate spheroid",
        ///         "correctAnswer": "true"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Successfully retrieved question</response>
        /// <response code="404">Question with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetQuestionById")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);

                if(question == null)
                    return NotFound($"Question with id {id} does not exist");
                
                return Ok(question);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="id">Question id to update</param>
        /// <param name="questionToUpdate"> Question updated details</param>
        /// <returns>Returns newly updated question</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Questions/2
        ///     {
        ///         "question": "Earth is gaseous",
        ///         "correctAnswer": "False"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Successfully updated a question</response>
        /// <response code="400">Question details are invalid</response>
        /// <response code="404">Question is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateQuestion")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(QuestionUpdateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionUpdateDto questionToUpdate)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);

                bool isValid = await _questionService.ValidateQuestion(questionToUpdate);
                bool idFound = await _questionService.GetQuestionById(id) != null;

                if(!isValid)
                {
                    return BadRequest();
                }

                if (!idFound)
                {
                    return NotFound();
                }

                var updatedQuestion = await _questionService.UpdateQuestion(id, questionToUpdate);
                return Ok(updatedQuestion);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ">>> catch " + e.ToString());
            }
        }

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="id">Question Id</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Takers/1
        ///            Question with Id = 1 was Successfully Deleted
        /// 
        /// </remarks>
        /// <response code="200">Successfully deleted question</response>
        /// <response code="404">Question with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);

                if (question == null)
                    return NotFound($"Question with Id = {id} is not found");

                await _questionService.DeleteQuestion(id);
                return Ok($"Question with Id = {id} was Successfully Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
