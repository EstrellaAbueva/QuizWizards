using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Dtos.Topic;
using QuizApi.Services.Quizzes;
using QuizApi.Services.Topics;

namespace QuizApi.Controllers
{

#pragma warning disable
    [Route("api/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ILogger<TopicsController> _logger;
        private readonly ITopicService _topicService;
        private readonly IQuizService _quizService;

        public TopicsController(
             ITopicService topicService,
             ILogger<TopicsController> logger,
             IQuizService quizService)
        {
            _logger = logger;
            _topicService = topicService;
            _quizService = quizService;
        }

        /// <summary>
        /// Creates a Topic
        /// </summary>
        /// <param name="topic">Topic details</param>
        /// <param name="date">Date of creation</param>
        /// <returns>Returns the newly created topics/quizId</returns>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/Topics
        ///     {
        ///         "id" : "1".
        ///         "name" : "Animal-Bio",
        ///         "CreatedDate" : 12/2/2023 12:00:00,
        ///         "UpdatedDate" : null,
        ///         "Questions" { 1 , 2 };
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a topic</response>
        /// <response code="400">Topic details are invalid</response>
        /// <response code="404">Quiz with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTopic([FromBody] TopicCreationDto topic)
        {
            try
            {

                bool isValid = await _topicService.ValidateTopic(topic);
                if (!isValid)
                {
                    return BadRequest();
                }

                var newTopic = await _topicService.CreateTopic(topic);
                return CreatedAtRoute("GetTopicById", new { id = newTopic.Id }, newTopic);
            }
            catch (Exception e)
            {
               
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets All Topics
        /// </summary>
        /// <returns>All topics with quiz id</returns>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/Topics
        ///     {
        ///         "id" : "1".
        ///         "name" : "Animal-Bio",
        ///         "CreatedDate" : 12/2/2023 12:00:00,
        ///         "UpdatedDate" : null,
        ///         "Questions" { 1 , 2 };
        ///     },
        ///     {
        ///         "id" : "2".
        ///         "name" : "Animal-Kingdom",
        ///         "CreatedDate" : 12/2/2023 12:00:00,
        ///         "UpdatedDate" : null,
        ///         "Questions" { 3 , 5 };
        ///     },
        ///         
        ///         
        /// </remarks>
        /// <response code="200">Successfully retrieved topics</response>
        /// <response code="204">There are no topics</response>
        /// <response code="404">Quiz with the given quizId is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllTopics")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTopics()
        {
            try
            {
                var topics = await _topicService.GetAllTopics();

                if (topics == null)
                {
                    return NotFound();
                }

                return Ok(topics);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets a Topic by Id
        /// </summary>
        /// <param name="id">Topic Id</param>
        /// <returns>Topic</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Takers/2
        ///     {
        ///         "id" : "1".
        ///         "name" : "Animal-Bio",
        ///         "CreatedDate" : 12/2/2023 12:00:00,
        ///         "UpdatedDate" : null,
        ///         "Questions" { 1 , 2 };
        ///     }
        ///         
        /// </remarks>
        /// <returns>Topic by the id </returns>
        /// <response code="200">Successfully retreived topic</response>
        /// <response code="404">Topic with the given Id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetTopicById")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopic(int id)
        {
            try
            {
                var topic = await _topicService.GetTopicById(id);

                if (topic == null)
                    return NotFound($"Topic with id {id} does not exist");

                return Ok(topic);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Upates a Topic
        /// </summary>
        /// <param name="id">Topic Id</param>
        /// <param name="topicToBeUpdated">Topic to be updated details</param>
        /// <returns>Returns the newly updated topic</returns>
        /// <remarks>
        /// Sample Request:
        ///     
        ///     PUT /api/Topic
        ///     {
        ///         "quizId" : "1",
        ///         "topic" : "Plant-Bio";
        ///             
        ///     }
        /// </remarks>
        /// <response code="200">Successfully updated a topic</response>
        /// <response code="400">Topic details are invalid</response>
        /// <response code="404">Topic is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicCreationDto topicToBeUpdated)
        {
            try
            {
                var topic = await _topicService.GetTopicById(id);

                if (topic == null)
                    return NotFound($"Topic with Id = {id} is not found");

                var updatedTopic = await _topicService.UpdateTopic(id, topicToBeUpdated);
                return Ok(updatedTopic);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong" + e.Message);
            }
        }

        /// <summary>
        /// Deletes a Topic
        /// </summary>
        /// <param name="id">Id of Topic</param>
        /// <returns>Successful deletion message</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Topic/1
        ///            Topic with Id = 1 was Successfully Deleted
        ///                 
        /// </remarks>
        /// <response code="200">Successfully deleted a topic</response>
        /// <response code="404">Topic with the given id is not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(Roles = "A")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            try
            {
                var topic = await _topicService.GetTopicById(id);

                if (topic == null)
                    return NotFound($"Topic with Id = {id} is not found");

                await _topicService.DeleteTopic(id);
                return Ok($"Topic with Id = {id} was Successfully Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}