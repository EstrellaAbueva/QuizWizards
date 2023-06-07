using Dapper;
using QuizApi.Context;
using QuizApi.Dtos.QuizD;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using System.Data;

namespace QuizApi.Repositories.Quizzes
{
#pragma warning disable
    public class QuizRepository : IQuizRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public QuizRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateQuiz(Quiz quiz)
        {
            var sql = "INSERT INTO Quizzes (Name, Description, CreatedDate, UpdatedDate , TopicId , MaxScore) " +
                "VALUES (@Name , @Description , GETDATE() , NULL , @TopicId , " +
                "(SELECT SUM(NumberofQuestions) FROM Topics t " +
                "WHERE t.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(@TopicId, ','))))" +
                "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { quiz.Name, quiz.Description , TopicId = string.Join(",", quiz.TopicId) });
            }
        }
        
        public async Task<IEnumerable<Quiz>> GetAllQuiz()
        {
            var sql = @"SELECT qu.*, t.*, q.*
                        FROM Quizzes qu
                        LEFT JOIN Topics t ON t.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(qu.TopicId, ','))
                        LEFT JOIN Questions q ON q.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(t.QuestionId, ','))";

            using (var connection = _context.CreateConnection())
            {
                var lookup = new Dictionary<int, Quiz>();

                await connection.QueryAsync<Quiz, TopicDto, Problem, Quiz>(
                    sql,
                    (quiz, topic, question) =>
                    {
                        if (!lookup.TryGetValue(quiz.Id, out var quizEntry))
                        {
                            quizEntry = quiz;
                            quizEntry.Topics = new List<TopicDto>();
                            lookup.Add(quizEntry.Id, quizEntry);
                        }

                        if (quiz != null && !quizEntry.Topics.Any(q => q.Id == quiz.Id))
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

                            if (topic != null && quizEntry != null && !quizEntry.Topics.Any(t => t.Id == topic.Id))
                            {
                                if (question != null && !topic.Questions.Any(q => q.Id == question.Id))
                                {
                                    topic.Questions.Add(question);
                                }
                            }
                        }
                        return quizEntry;
                    },
                    splitOn: "TopicId , QuestionId");

                return lookup.Values;
            }
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            var sql = @"SELECT qu.*, t.*, q.*
                        FROM Quizzes qu
                        LEFT JOIN Topics t ON t.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(qu.TopicId, ','))
                        LEFT JOIN Questions q ON q.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(t.QuestionId, ','))
                        WHERE qu.Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var lookup = new Dictionary<int, Quiz>();

                await connection.QueryAsync<Quiz, TopicDto, Problem, Quiz>(
                    sql,
                    (quiz, topic, question) =>
                    {
                        if (!lookup.TryGetValue(quiz.Id, out var quizEntry))
                        {
                            quizEntry = quiz;
                            quizEntry.Topics = new List<TopicDto>();
                            lookup.Add(quizEntry.Id, quizEntry);
                        }

                        if (topic != null)
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

                        return quizEntry;
                    },
                    new { id },
                    splitOn: "TopicId, QuestionId");

                return lookup.Values.FirstOrDefault();
            }
        }


        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            var sql = "UPDATE Quizzes SET" +
                " Name = @Name, " +
                "Description = @Description, " +
                "UpdatedDate = GETDATE() , " +
                "TopicId = @TopicId , " +
                "MaxScore = (SELECT SUM(NumberofQuestions) FROM Topics t " +
                "WHERE t.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(@TopicId, ',')))" +
                "WHERE Id = @Id";

            using (var con = _context.CreateConnection())
            {
                var rowsAffected = await con.ExecuteAsync(sql, new { Id = quiz.Id , Name = quiz.Name, Description = quiz.Description, UpdatedDate = DateTime.Now, TopicId = string.Join(",", quiz.TopicId) });
                return rowsAffected > 0 ? true : false;
            }
        }

        public async Task<bool> CheckQuizId(int id)
        {
            var sql = "SELECT * FROM Quizzes WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { id });
            }
        }

        public async Task<bool> DeleteQuiz(int id)
        {
            var sp = "[spQuiz_DeleteQuiz]";

            using (var con = _context.CreateConnection())
            {
                int rowAffected = await con.ExecuteAsync(sp, new { id }, commandType: CommandType.StoredProcedure);
                return rowAffected > 0 ? true : false;
            }
        }

        private static Quiz MapQuizTopic(Quiz quiz, TopicDto topic)
        {
            quiz.Topics.Add(topic);
            return quiz;
        }

        public async Task<List<QuizLeaderboard>> GetQuizLeaderboardById(int id)
        {
            var sql = @"SELECT t.Name AS TakerName, q.Name AS QuizName, qt.score 
                FROM TakerQuiz qt
                INNER JOIN Quizzes q ON qt.QuizId = q.Id
                INNER JOIN Takers t ON qt.TakerId = t.Id
                WHERE qt.QuizId = @Id AND t.TakerType != 'A'
                ORDER BY qt.score DESC";

            using (var con = _context.CreateConnection())
            {
                var result = await con.QueryAsync<QuizLeaderboard>(
                    sql,
                    new { Id = id });

                return result.ToList();
            }
        }

        public async Task<bool> CheckQuizValidation(QuizCreationDto quiz)
        {
            if (quiz == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(quiz.Name))
            {
                return false;
            }

            if (string.IsNullOrEmpty(quiz.Description))
            {
                return false;
            }

            if (quiz.TopicId == null || !quiz.TopicId.Any())
            {
                return false;
            }

            return true;
        }


    }
}
