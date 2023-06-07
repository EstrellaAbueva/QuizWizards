using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Services.Topics
{
    public interface ITopicService
    {
        /// <summary>
        /// Create topic
        /// </summary>
        /// <param name="topicToCreate">Topic details</param>
        /// <returns>Newly created topic with TopicDto details</returns>
        Task<TopicDto> CreateTopic(TopicCreationDto topicToCreate);

        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>All topic with TopicDto details</returns>
        Task<IEnumerable<TopicDto>> GetAllTopics();


        /// <summary>
        /// Get topic by Id
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <returns>Topic with TopicDto details</returns>
        Task<TopicDto?> GetTopicById(int id);


        /// <summary>
        /// Update topic
        /// </summary>
        /// <param name="id">Id of topic</param>
        /// <param name="topicToUpdate">Topic to be updated</param>
        /// <returns>newly updated topic with TopicDto details</returns>
        Task<TopicDto> UpdateTopic(int id, TopicCreationDto topicToUpdate);

        /// <summary>
        /// Delete topic
        /// </summary>
        /// <param name="id">Id of topic</param>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteTopic(int id);

        /// <summary>
        /// Validate the Topic
        /// </summary>
        /// <param name="topic">Topic to validate</param>
        /// <returns>True if the topic is valid and False if not</returns>
        Task<bool> ValidateTopic(TopicCreationDto topic);
    }
}