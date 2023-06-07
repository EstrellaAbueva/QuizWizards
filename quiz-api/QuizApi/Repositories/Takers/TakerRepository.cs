using System.Data;
using Dapper;
using QuizApi.Context;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Taker;
using QuizApi.Dtos.Topic;
using QuizApi.Models;


#pragma warning disable
namespace QuizApi.Repositories.Takers
{
    public class TakerRepository : ITakerRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public TakerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTaker(Taker taker)
        {
            var sql = "INSERT INTO Takers (Name, Address, Email, Username, Password, CreatedDate , UpdatedDate) VALUES (@Name, @Address, @Email, @Username, @Password, GETDATE(), NULL); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, taker);
            }
        }

        public async Task<IEnumerable<Taker>> GetAll()
        {
            var sql = "[spTaker_GetAllTakers]";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Taker>(sql);
            }
        }

        public async Task<IEnumerable<Taker>> GetAllByQuizId(int quizId)
        {
            var sql = "[spTaker_GetAllByQuizId]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Taker, QuizDto, Taker>(
                    sql,
                    MapTakerQuiz,
                    new { quizId },
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.Quizzes = TakerGroup.SelectMany(taker => taker.Quizzes).ToList();
                    return firstTaker;
                });
            }
        }

        public async Task<Taker?> GetTaker(int id)
        {
            var sql = @"SELECT t.*, q.*, ts.*, qs.* FROM Takers t
                        LEFT JOIN TakerQuiz tq ON t.Id = tq.TakerId 
                        LEFT JOIN Quizzes q ON q.Id = tq.QuizId 
                        LEFT JOIN Topics ts ON ts.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(q.TopicId, ',') WHERE value != '') 
                        LEFT JOIN Questions qs ON qs.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(ts.QuestionId, ',') WHERE value != '') 
                        WHERE t.Id = @Id
                        ORDER BY t.Id, q.Id, ts.Id, qs.Id;";

            using (var connection = _context.CreateConnection())
            {
                var lookup = new Dictionary<int, Taker>();

                await connection.QueryAsync<Taker, QuizDto, TopicDto, Problem, Taker>(
                    sql,
                    (taker, quiz, topic, question) =>
                    {
                        if (!lookup.TryGetValue(taker.Id, out var takerEntry))
                        {
                            takerEntry = taker;
                            takerEntry.Quizzes = new List<QuizDto>();
                            lookup.Add(takerEntry.Id, takerEntry);
                        }

                        if (quiz != null && !takerEntry.Quizzes.Any(q => q.Id == quiz.Id))
                        {
                            quiz.Topics = new List<TopicDto>();
                            takerEntry.Quizzes.Add(quiz);
                        }

                        if (takerEntry.Quizzes.Any())
                        {
                            var quizEntry = takerEntry.Quizzes.FirstOrDefault(q => q.Id == quiz.Id);
                            if (topic != null && quizEntry != null && !quizEntry.Topics.Any(t => t.Id == topic.Id))
                            {
                                topic.Questions = new List<Problem>();
                                quizEntry.Topics.Add(topic);
                            }

                            if (question != null && topic != null && quizEntry != null)
                            {
                                var existingTopic = quizEntry.Topics.FirstOrDefault(t => t.Id == topic.Id);
                                if (existingTopic == null)
                                {
                                    topic.Questions = new List<Problem>();
                                    quizEntry.Topics.Add(topic);
                                }
                                else
                                {
                                    topic = existingTopic;
                                }

                                if (question != null && !topic.Questions.Any(q => q.Id == question.Id))
                                {
                                    topic.Questions.Add(question);
                                }
                            }
                        }

                        return takerEntry;
                    },
                    new { id },
                    splitOn: "Id, Id, Id, Id , Id");

                return lookup.Values.FirstOrDefault();
            }
        }

        public async Task<Taker?> GetTakerByUsername(string username)
        {
            var sql = @"SELECT t.*, q.*, ts.*, qs.* FROM Takers t
                        LEFT JOIN TakerQuiz tq ON t.Id = tq.TakerId 
                        LEFT JOIN Quizzes q ON q.Id = tq.QuizId 
                        LEFT JOIN Topics ts ON ts.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(q.TopicId, ',') WHERE value != '') 
                        LEFT JOIN Questions qs ON qs.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(ts.QuestionId, ',') WHERE value != '') 
                        WHERE t.Username = @Username
                        ORDER BY t.Id, q.Id, ts.Id, qs.Id;";

            using (var connection = _context.CreateConnection())
            {
                var lookup = new Dictionary<int, Taker>();

                await connection.QueryAsync<Taker, QuizDto, TopicDto, Problem, Taker>(
                    sql,
                    (taker, quiz, topic, question) =>
                    {
                        if (!lookup.TryGetValue(taker.Id, out var takerEntry))
                        {
                            takerEntry = taker;
                            takerEntry.Quizzes = new List<QuizDto>();
                            lookup.Add(takerEntry.Id, takerEntry);
                        }

                        if (quiz != null && !takerEntry.Quizzes.Any(q => q.Id == quiz.Id))
                        {
                            quiz.Topics = new List<TopicDto>();
                            takerEntry.Quizzes.Add(quiz);
                        }

                        if (takerEntry.Quizzes.Any())
                        {
                            var quizEntry = takerEntry.Quizzes.FirstOrDefault(q => q.Id == quiz.Id);
                            if (topic != null && quizEntry != null && !quizEntry.Topics.Any(t => t.Id == topic.Id))
                            {
                                topic.Questions = new List<Problem>();
                                quizEntry.Topics.Add(topic);
                            }

                            if (question != null && topic != null && quizEntry != null)
                            {
                                var existingTopic = quizEntry.Topics.FirstOrDefault(t => t.Id == topic.Id);
                                if (existingTopic == null)
                                {
                                    topic.Questions = new List<Problem>();
                                    quizEntry.Topics.Add(topic);
                                }
                                else
                                {
                                    topic = existingTopic;
                                }

                                if (question != null && !topic.Questions.Any(q => q.Id == question.Id))
                                {
                                    topic.Questions.Add(question);
                                }
                            }
                        }

                        return takerEntry;
                    },
                    new { username },
                    splitOn: "Id, Id, Id, Id , Id");

                return lookup.Values.FirstOrDefault();
            }
        }

        public async Task<Taker?> GetTakerWithQuiz(int id)
        {
            var sql = "[spTaker_GetTakerWithQuizById]";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Taker, QuizDto, Taker>(
                    sql,
                    MapTakerQuiz,
                    new { id },
                    commandType: CommandType.StoredProcedure);

                return result.GroupBy(s => s.Id).Select(TakerGroup =>
                {
                    var firstTaker = TakerGroup.First();
                    firstTaker.Quizzes = TakerGroup.SelectMany(taker => taker.Quizzes).ToList();
                    return firstTaker;
                }).SingleOrDefault();
            }
        }

        public async Task<bool> UpdateTaker(Taker taker)
        {
            //update taker and set UpdatedDate to current date
            var sql = "UPDATE Takers SET Name = @Name, Address = @Address, Email = @Email, Username = @Username, Password = @Password, UpdatedDate = @UpdatedDate WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(sql, new { taker.Name, taker.Address, taker.Email, taker.Username, taker.Password, UpdatedDate = DateTime.Now, taker.Id });
                return rowsAffected != 0;
            }
        }

        public async Task<bool> DeleteTaker(int id)
        {
            var sql = "[spTaker_DeleteTakerById]";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(
                    sql,
                    new { id },
                    commandType: CommandType.StoredProcedure);

                return rowsAffected != 0;
            }
        }

        public async Task<int> LetTakerTakeQuiz(int takerId, int quizId)
        {
            var sql = "INSERT INTO TakerQuiz (TakerId, QuizId, AssignedDate, Score, TakenDate, FinishedDate) VALUES (@TakerId, @QuizId, GETDATE(), NULL, NULL, NULL); " +
                      "SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        public async Task<bool> HasTakerTakenQuiz(int takerId, int quizId)
        {
            var sql = "SELECT * FROM TakerQuiz tq WHERE tq.TakerId = @TakerId AND tq.QuizId = @QuizId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        private static Taker MapTakerQuiz(Taker taker, QuizDto quiz)
        {
            taker.Quizzes.Add(quiz);
            return taker;
        }

        public async Task<int> TakerAnswersQuiz(TakerAnswer answer)
        {
            var sqlCheck = @"SELECT Id FROM Answers
                             WHERE TakerId = @TakerId AND QuizId = @QuizId AND QuestionId = @QuestionId";

            var sqlInsert = @"INSERT INTO Answers (TakerId, QuizId, QuestionId, Answer)
                              VALUES (@TakerId, @QuizId, @QuestionId, @Answer);
                              SELECT SCOPE_IDENTITY();";

            var sqlUpdate = @"UPDATE Answers
                             SET Answer = @Answer
                             WHERE TakerId = @TakerId AND QuizID = @QuizId AND QuestionId = @QuestionId;
                             SELECT Id FROM Answers WHERE TakerId = @TakerId AND QuizID = @QuizId AND QuestionId = @QuestionId";

            using (var connection = _context.CreateConnection())
            {
               var isExist = await connection.ExecuteScalarAsync<bool>(sqlCheck, new { TakerId = answer.TakerId, QuizId = answer.QuizId, QuestionId = answer.QuestionId });

                if (!isExist)
                {
                    return await connection.ExecuteScalarAsync<int>(sqlInsert, answer);
                }
                else
                {
                    return await connection.ExecuteAsync(sqlUpdate, new { Answer = answer.Answer, TakerId = answer.TakerId, QuizId = answer.QuizId, QuestionId = answer.QuestionId });
                }
            }

            return -1;
        }

        public async Task<TakerQuiz?> GetTakerQuizScore(int takerId, int quizId)
        {
            var sql = @"UPDATE TakerQuiz 
                        SET Score = (  
                            SELECT COUNT(*) 
                            FROM Answers a 
                            LEFT JOIN Questions q ON q.Id = a.QuestionId 
                            WHERE a.TakerId = TakerQuiz.TakerId AND a.Answer = q.CorrectAnswer AND a.QuizId = TakerQuiz.QuizId
                        ); 
                        SELECT* FROM TakerQuiz tq WHERE  tq.TakerId = @TakerId AND tq.QuizId = @QuizId;";

            using (var connection = _context.CreateConnection()) 
            {
                return await connection.QuerySingleOrDefaultAsync<TakerQuiz>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        public async Task<IEnumerable<TakerAnswer?>> GetTakerAnswer(int id)
        {
            var sql = "SELECT a.* FROM Answers a " +
                      "WHERE a.TakerId = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<TakerAnswer>(sql, new { Id = id });
            }

        }

        public async Task<IEnumerable<TakerAnswer>> GetAllAnswers()
        {
            var sql = "SELECT * FROM Answers";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<TakerAnswer>(sql);
            }
        }

        public async Task<TakerQuiz?> TakerUpdateTakenDate(int takerId, int quizId)
        {
            var sql = "UPDATE TakerQuiz SET TakenDate = GETDATE() WHERE TakerId = @TakerId AND QuizId = @QuizId; ";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<TakerQuiz>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        public async Task<TakerQuiz?> TakerUpdateFinishedDate(int takerId, int quizId)
        {
            var sql = "UPDATE TakerQuiz SET FinishedDate = GETDATE() WHERE TakerId = @TakerId AND QuizId = @QuizId; ";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<TakerQuiz>(sql, new { TakerId = takerId, QuizId = quizId });
            }
        }

        public async Task<TakerQuiz> SetQuizRetake(int retake , int takerId, int quizId)
        {
            var sql = "UPDATE TakerQuiz SET CanRetake = @CanRetake WHERE TakerId = @TakerId AND QuizId = @QuizId; ";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<TakerQuiz>(sql, new { CanRetake = retake, TakerId = takerId, QuizId = quizId });
            }
        }

        public async Task<bool> DeleteAnswer(int id)
        {
            var sql = "DELETE FROM Answers WHERE Id = @Id; ";

            using (var connection = _context.CreateConnection())
            { 
                int rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
                return rowsAffected != 0;
            }
        }

        public async Task<TakerAnswer?> GetAnswerById(int id)
        {
            var sql = "SELECT * FROM Answers WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<TakerAnswer>(sql, new { Id = id });
            }
        }

        public async Task<TakerAnswer?> GetAnswerDetails(int takerId, int quizId, int questionId)
        {
            var sql = "SELECT * FROM Answers WHERE TakerId = @TakerId AND QuizId = @QuizId AND QuestionId = @QuestionId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<TakerAnswer>(sql, new { TakerId = takerId, QuizId = quizId, QuestionId = questionId });
            }
        }

        public async Task<bool> ValidateCreate(TakerCreationDto taker)
        {
            if (taker == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(taker.Name))
            {
                return false;
            }

            if(string.IsNullOrEmpty(taker.Address))
            {
                return false;
            }

            if (string.IsNullOrEmpty(taker.Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(taker.Username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(taker.Password))
            {
                return false;
            }

            return true;
        }
    }
}
