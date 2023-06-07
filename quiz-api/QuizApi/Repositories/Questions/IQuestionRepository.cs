using QuizApi.Dtos.Question;
using QuizApi.Models;

namespace QuizApi.Repositories
{
    /// <summary>
    /// Interface for question repository
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Creates a question
        /// </summary>
        /// <returns>Id of the newly created question</returns>
        Task<int> CreateQuestion(Problem question);
        
        /// <summary>
        /// Gets all questions
        /// </summary>
        /// <returns>All questions across all topics</returns>
        Task<IEnumerable<Problem>> GetAllQuestions();
        /// <summary>
        /// Gets a question by id = <param name="id">Question id</param>
        /// </summary>
        /// <returns>Question with the given id</returns>
        Task<Problem> GetQuestionById(int id);

        /// <summary>
        /// Gets all questions by topic id = <param name="id">Topic id</param>
        /// </summary>
        /// <returns>All questions with the given topicId</returns>
        Task<IEnumerable<Problem>> GetAllQuestionsByTopicId(int id);

        /// <summary>
        /// Updates question details <param name="question">Question to update</param>
        /// </summary>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool> UpdateQuestion(Problem question);
        /// <summary>
        /// Deletes a question 
        /// </summary>
        /// <param name="id">Id of the question to delete</param>
        /// <returns>True if deletion is successful, otherwise false</returns>
        Task<bool> DeleteQuestion(int id);

        /// <summary>
        /// Validate Question
        /// </summary>
        /// <param name="question">Question to validate</param>
        /// <returns>True if valid and False if not.</returns>
        Task<bool> ValidateQuestion(QuestionCreationDto question);

        /// <summary>
        /// Validate Question
        /// </summary>
        /// <param name="question">Question to validate</param>
        /// <returns>True if valid and False if not.</returns>
        Task<bool> ValidateQuestion(QuestionUpdateDto question);

    }
}
