using QuizApi.Dtos.Taker;
using QuizApi.Models;

namespace QuizApi.Services.Takers
{
    public interface ITakerService
    {

#pragma warning disable
        /// <summary>
        /// Creates <param name="takerToCreate">TakerCreationDto Details</param>
        /// </summary>
        /// <returns>TakerDto deatils</returns>
        Task<TakerDto> CreateTaker(TakerCreationDto takerToCreate);

        /// <summary>
        /// Get all takers
        /// </summary>
        /// <returns>All takers with TakerDto details</returns>
        Task<IEnumerable<TakerDto>> GetAllTakers();

        /// <summary>
        /// Get all takers by quiz Id = <param name="quizId">Quiz id</param>
        /// </summary>
        /// <returns>All Takers with TakerQuizDto details with a quiz</returns>
        Task<IEnumerable<TakerQuizDetailsDto>> GetAllTakers(int quizId);


        /// <summary>
        /// Gets taker with id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker with TakerQuizzesDto details</returns>
        Task<TakerUserNameDto?> GetTakerById(int id);

        /// <summary>
        /// Gets taker with username = <param name="username">Username of Taker</param>
        /// </summary>
        /// <returns>Taker</returns>
        Task<TakerUserNameDto?> GetTakerByUsername(string username);

        /// <summary>
        /// Gets taker with quiz by id = <param name="id">Id of Taker</param>
        /// </summary>
        /// <returns>Taker with TakerQuizzesDto details</returns>
        Task<TakerQuizzesDto?> GetTakerWithQuizById(int id);

        /// <summary>
        /// Updates taker <param name="id">Taker to update</param>
        /// </summary>
        /// <param name="takerToUpdate">Taker Details</param>
        /// <returns>Newly updated Taker with TakerDto details</returns>
        Task<TakerUserNameDto> UpdateTaker(int id, TakerCreationDto takerToUpdate);

        /// <summary>
        /// Deletes taker with id <param name="id">Id of taker</param>
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
        Task<TakerAnswersDto?> TakerAnswersQuiz(TakerAnswerCreationDto answer);

        /// <summary>
        /// Gets the score of the Taker
        /// </summary>
        /// <param name="takerId">TakerId to get the Score</param>
        /// <param name="quizId">Quiz that the Taker answered</param>
        /// <returns>TakerQuizDto</returns>
        Task<TakerQuizDto?> GetTakerQuizScore(int takerId, int quizId);

        /// <summary>
        /// Gets all the answers of a specific Taker by Id
        /// </summary>
        /// <param name="id">Id of the answer</param>
        /// <returns>TakerAnswersDto</returns>
        Task<IEnumerable<TakerAnswersDto?>> GetTakerAnswer(int id);

        /// <summary>
        /// Gets all the answers of the Taker
        /// </summary>
        /// <returns>IEnumerable<TakerAnswersDto></returns>
        Task<IEnumerable<TakerAnswersDto>> GetAllAnswers();

        /// <summary>
        /// Get Answer by Id
        /// </summary>
        /// <param name="id">Id of the Answer</param>
        /// <returns>TakerAnswersDto</returns>
        Task<TakerAnswersDto?> GetAnswerById(int id);

        /// <summary>
        ///  Verifies if the login password is the same as the password in the database of a Taker
        ///  </summary>
        ///  <param name="inputtedPassword">Password inputted by user</param>
        ///  <param name="password">Password in the database</param>
        ///  <returns >True if password is the same, otherwise false</returns>
        public bool VerifyPassword(string password, string inputtedPassword);

        /// <summary>
        /// Creates a token for a Taker to use for authentication
        /// </summary>
        /// <param name="taker"></param>
        /// <returns>Generated token</returns>
        public string CreateToken(TakerUserNameDto taker);

        /// <summary>
        /// Taker updates the date when he/she took the quiz
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>TakerQuizDto</returns>
        Task<TakerQuizDto> TakerUpdateTakenDate(int takerId, int quizId);

        /// <summary>
        /// Taker updates the date when he/she finished taking the quiz
        /// </summary>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>TakerQuizDto</returns>
        Task<TakerQuizDto> TakerUpdateFinishedDate(int takerId, int quizId);

        /// <summary>
        /// The taker can retake the quiz if the quiz is retakable
        /// </summary>
        /// <param name="retake">Either 1 or 0</param>
        /// <param name="takerId">Id of the Taker</param>
        /// <param name="quizId">Id of the Quiz</param>
        /// <returns>TakerQuizDto</returns>
        Task<TakerQuizDto> UpdateTakerRetake(int retake , int takerId, int quizId);

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
        /// <returns>TakerAnswersDto</returns>
        Task<TakerAnswersDto?> GetAnswerDetails(int takerId, int quizId, int questionId);

        /// <summary>
        /// Validate the taker details
        /// </summary>
        /// <param name="taker">Details of the Taker</param>
        /// <returns>True if Taker details are valid and False if not</returns>
        Task<bool> ValidateCreate(TakerCreationDto taker);
    }
}