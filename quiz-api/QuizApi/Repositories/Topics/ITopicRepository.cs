using QuizApi.Context;
using QuizApi.Dtos.Topic;
using QuizApi.Models;

namespace QuizApi.Repositories.Topics
{
    /// <summary>
    /// Interface for topic repository
    /// </summary>
    public interface ITopicRepository
    {
        /// <summary>
        /// Creates topics
        /// </summary>
        /// <param name="topic">Topic details</param>
        /// <returns>The newly created topic and quizId</returns>
        Task<int> CreateTopic(Topic topic);

        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>All topics</returns>
        Task<IEnumerable<Topic>> GetAllTopics();

        /// <summary>
        /// Get topics
        /// </summary>
        /// <param name="id">Topic details</param>
        /// <returns>Topic</returns>
        Task<Topic?> GetTopic(int id);

        /// <summary>
        /// Updates topics
        /// </summary>
        /// <param name="topic">Topic details</param>
        /// <returns>The newly updated topics</returns>
        Task<bool> UpdateTopic(Topic topic);

        /// <summary>
        /// Deletes topics
        /// </summary>
        /// <param name="id">DeleteTopic Id</param>
        /// <returns>True if deletion successful otherwise false </returns>
        Task<bool> DeleteTopic(int id);

        /// <summary>
        /// Validate the Topic
        /// </summary>
        /// <param name="topic">Topic to validate</param>
        /// <returns>True if the topic is valid and False if not</returns>
        Task<bool> ValidateTopic(TopicCreationDto topic);
    }
}
