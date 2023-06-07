using QuizApi.Dtos.QuizD;
using QuizApi.Models;

namespace QuizApi.Repositories.Quizzes
{
    /// <summary>
    /// Interface for Quiz repository
    /// </summary>
    public interface IQuizRepository
    {
        /// <summary>
        /// Creates a Quiz.
        /// </summary>
        /// <param name="quiz">Quiz Details</param>
        /// <returns>Id of newly created quiz</returns>
        Task<int> CreateQuiz(Quiz quiz);

        /// <summary>
        /// Get all Quizzes with topic and questions
        /// </summary>
        /// <returns>All Quizzes with topics and question</returns>
        Task<IEnumerable<Quiz>> GetAllQuiz();

        /// <summary>
        /// Get Quiz topic and question by Quiz Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Quiz with topics and question</returns>
        Task<Quiz> GetQuizById(int id);

        /// <summary>
        /// Updates a quiz given its id, also updates other rows that references
        /// the given Quiz.
        /// </summary>
        /// <param name="quiz">Quiz to update</param>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool> UpdateQuiz(Quiz quiz);

        /// <summary>
        /// Deletes quiz with id
        /// and deletes connection between quiz and taker.
        /// </summary>
        /// <param name="id">Id of quiz to delete</param>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteQuiz(int id);

        /// <summary>
        /// Checks quiz if it exists given its id.
        /// </summary>
        /// <param name="id">Id of quiz to check</param>
        /// <returns>True if quiz is exists, otherwise false</returns>
        Task<bool> CheckQuizId(int id);

        /// <summary>
        /// Checks if quiz is valid
        /// </summary>
        /// <param name="quiz">Quiz to check</param>
        /// <returns>True if the Quiz is valid and False if not.</returns>
        Task<bool> CheckQuizValidation(QuizCreationDto quiz);

        /// <summary>
        /// Get Specific Quiz Leaderboard for a quiz
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A List of Quiz and Takers</returns>
        Task<List<QuizLeaderboard>> GetQuizLeaderboardById(int id);

    }
}
