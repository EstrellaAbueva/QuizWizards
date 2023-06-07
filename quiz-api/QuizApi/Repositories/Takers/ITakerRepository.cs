using QuizApi.Dtos.Taker;
using QuizApi.Models;
using System.Threading.Tasks;

namespace QuizApi.Repositories.Takers
{

#pragma warning disable
    /// <summary>
    /// Interface for taker repository
    /// </summary>
    public interface ITakerRepository
    {
        /// <summary>
        /// Creates <param name="taker">Taker Details</param>
        /// </summary>
        /// <returns>Id of newly created taker</returns>
        Task<int> CreateTaker(Taker taker);

        /// <summary>
        /// Get all takers
        /// </summary>
        /// <returns>All takers with quizzes</returns>
        Task<IEnumerable<Taker>> GetAll();

        /// <summary>
        /// Get all takers by quiz Id = <param name="quizId">Quiz id</param>
        /// </summary>
        /// <returns>All takers with a quiz given quizId</returns>
        Task<IEnumerable<Taker>> GetAllByQuizId(int quizId);

        /// <summary>
        /// Gets taker with id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker</returns>
        Task<Taker?> GetTaker(int id);

        /// <summary>
        /// Gets taker with username = <param name="username">Username of Taker</param>
        /// </summary>
        /// <returns>Taker</returns>
        Task<Taker?> GetTakerByUsername(string username);

        /// <summary>
        /// Gets taker with quiz by id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker with quizzes</returns>
        Task<Taker?> GetTakerWithQuiz(int id);

        /// <summary>
        /// Updates taker <param name="taker">Taker to update</param>
        /// </summary>
        /// <returns>True if update is successful, otherwise false</returns>
        Task<bool> UpdateTaker(Taker taker);

        /// <summary>
        /// Deletes taker with id <param name="id">Id of taker to delete</param>
        /// and Deletes connection between TakerQuiz, TakersAnswers, and TakerQuizResults
        /// </summary>
        /// <returns>True if delete is successful, otherwise false</returns>
        Task<bool> DeleteTaker(int id);

        /// <summary>
        /// Lets taker take a quiz
        /// </summary>
        /// <param name="takerId">Id of Taker</param>
        /// <param name="quizId">Id of a Quiz</param>
        /// <returns>Id of the connection between Taker and Quiz </returns>
        Task<int> LetTakerTakeQuiz(int takerId, int quizId);

        /// <summary>
        /// Checks if Taker has already taken quiz with id = <param name="quizId">Id of a Quiz</param>
        /// </summary>
        /// <param name="takerId">Id of Taker</param>
        /// <returns>True if Taker has taken quiz, otherwise false</returns>
        Task<bool> HasTakerTakenQuiz(int takerId, int quizId);

        /// <summary>
        /// Records answer of the Taker
        /// </summary>
        /// <param name="answer">Answer of the Taker</param>
        /// <returns>Returns TakerAnswersDto containing the details of the Answer.</returns>
        Task<int> TakerAnswersQuiz(TakerAnswer answer);

        /// <summary>
        /// Gets the score of the Taker
        /// </summary>
        /// <param name="takerId">TakerId to get the Score</param>
        /// <param name="quizId">Quiz that the Taker answered</param>
        /// <returns>TakerQuizDto</returns>
        Task<TakerQuiz?> GetTakerQuizScore(int takerId, int quizId);

        /// <summary>
        /// Gets all the answers of a specific Taker by Id
        /// </summary>
        /// <param name="id">Id of the answer</param>
        Task<IEnumerable<TakerAnswer?>> GetTakerAnswer(int id);

        /// <summary>
        /// Gets all the answers of the Taker
        /// </summary>
        /// <returns>IEnumerable<TakerAnswers></returns>
        Task<IEnumerable<TakerAnswer>> GetAllAnswers();

        /// <summary>
        /// Get Answer by Id
        /// </summary>
        /// <param name="id">Id of the Answer</param>
        /// <returns>TakerAnswer</returns>
        Task<TakerAnswer?> GetAnswerById(int id);

        /// <summary>
        /// Taker updates the date when he/she took the quiz
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>TakerQuiz</returns>
        Task<TakerQuiz?> TakerUpdateTakenDate(int takerId, int quizId);

        /// <summary>
        /// Taker updates the date when he/she finished taking the quiz
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>TakerQuiz</returns>
        Task<TakerQuiz?> TakerUpdateFinishedDate(int takerId, int quizId);

        /// <summary>
        /// The taker can retake the quiz if the quiz is retakable
        /// </summary>
        /// <param name="retake">Either 1 or 0</param>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>TakerQuiz</returns>
        Task<TakerQuiz> SetQuizRetake(int retake ,  int takerId, int quizId);

        /// <summary>
        /// Deletes the answer of the Taker
        /// </summary>
        /// <param name="id">Id of the Answer</param>
        /// <returns>True if deleted succesfully and False if not.</returns>
        Task<bool> DeleteAnswer(int id);

        /// <summary>
        /// Get the details of the Answer
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <param name="questionId">Id of the Question</param>
        /// <returns>TakerAnswers</returns>
        Task<TakerAnswer?> GetAnswerDetails(int takerId, int quizId, int questionId);

        /// <summary>
        /// Validate the taker details
        /// </summary>
        /// <param name="taker">Details of the Taker</param>
        /// <returns>True if Taker details are valid and False if not</returns>
        Task<bool> ValidateCreate (TakerCreationDto taker);
    }
}
