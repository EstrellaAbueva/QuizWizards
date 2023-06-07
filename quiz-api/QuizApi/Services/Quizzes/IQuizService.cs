using QuizApi.Dtos.QuizD;
using QuizApi.Models;

namespace QuizApi.Services.Quizzes
{
    /// <summary>
    /// Interface for Quiz service
    /// </summary>
    public interface IQuizService
    {
        /// <summary>
        /// Calls QuizRepository to create a Quiz.
        /// </summary>
        /// <param name="quizToCreate">Quiz Details</param>
        /// <returns>Id of newly created quiz</returns>
        Task<QuizDto> CreateQuiz(QuizCreationDto quizToCreate);

        /// <summary>
        /// Calls QuizRepository to get quiz by id. 
        /// </summary>
        /// <param name="id">id of quiz</param>
        /// <returns>QuizDto</returns>
        Task<QuizDto?> GetQuizById(int id);

        /// <summary>
        /// Calls QuizRepository to get all Quizzes
        /// </summary>
        /// <returns>All Quizzes with topics</returns>
        Task<IEnumerable<QuizDto>> GetAllQuizzes();

        /// <summary>
        /// Calls QuizRepository to update a quiz given its id.
        /// </summary>
        /// <param name="id">Id of Quiz to update</param>
        /// <param name="quizToUpdate">Quiz to update</param>
        /// <returns>QuizDto of the updatd quiz</returns>
        Task<QuizDto> UpdateQuiz(int id, QuizCreationDto quizToUpdate);

        /// <summary>
        /// Calls QuizRepository to delete quiz with id
        /// and deletes connection between quiz and taker.
        /// </summary>
        /// <param name="id">Id of quiz to delete</param>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteQuiz(int id);

        /// <summary>
        /// Calls QuizRepository to check quiz if it exists given its id.
        /// </summary>
        /// <param name="id">Id of quiz to check</param>
        /// <returns>True if quiz is exists, otherwise false</returns>
        Task<bool> CheckQuizById(int id);

        /// <summary>
        /// Gets a quiz leaderboard by quiz id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A List of Quiz and Taker</returns>
        Task<List<QuizLeaderboard>> GetQuizLeaderboardById(int id);

        /// <summary>
        /// Checks if quiz is valid
        /// </summary>
        /// <param name="quiz">Quiz to check</param>
        /// <returns>True if the Quiz is valid and False if not.</returns>
        Task<bool> CheckQuizValidation(QuizCreationDto quiz);
    }
}
