using QuizApi.Dtos.Question;
using QuizApi.Models;

namespace QuizApi.Services.Questions
{
    public interface IQuestionService
    {
        /// <summary>
        /// Creates a question
        /// </summary>
        /// <returns>QuestionDto with Question details </returns>
        Task<QuestionDto> CreateQuestion(QuestionCreationDto questionToCreate);

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>All questions with QuestionDto details </returns>
        Task<IEnumerable<QuestionDto>> GetAllQuestions();

        /// <summary>
        /// Gets all questions by topic id = <param name="id">Topic id</param>
        /// </summary>
        /// <returns>All questions with the given topicId and QuestionDto details</returns>
        Task<IEnumerable<QuestionDto>> GetAllQuestionsByTopicId(int id);

        /// <summary>
        /// Get question by id = <param name="id">Question id</param>
        /// </summary>
        /// <returns>
        /// Question with the given id and QuestionDto details
        /// Returns null if question is not found
        /// </returns>
        Task<QuestionDto?> GetQuestionById(int id);

        /// <summary>
        /// Updates question <param name="id"> Id of Question to update</param>
        /// </summary>
        /// <param name="questionToUpdate"> Question details </param>
        /// <returns>Newly updated question with QuestionDto details</returns>
        Task<QuestionDto> UpdateQuestion(int id, QuestionUpdateDto questionToUpdate);
        /// <summary>
        /// Deletes question <param name="id"> Id of Question to delete</param>
        /// </summary>
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
