using QuizApi.Context;
using QuizApi.Models;
using Dapper;
using System.Data;
using QuizApi.Dtos.Question;

namespace QuizApi.Repositories
{
#pragma warning disable
    public class QuestionRepository : IQuestionRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public QuestionRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> CreateQuestion(Problem question)
        {
            var sql = "INSERT INTO Questions (Question, CorrectAnswer, CreatedDate , UpdatedDate) VALUES (@Question, @CorrectAnswer, GETDATE(), NULL) " +
                      "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Question = question.Question, CorrectAnswer = question.CorrectAnswer });
            }
        }

        public async Task<IEnumerable<Problem>> GetAllQuestions()
        {
            var sql = "SELECT * FROM Questions";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Problem>(sql);
            }
        }
        public async Task<IEnumerable<Problem>> GetAllQuestionsByTopicId(int id)
        {
            var sql = "SELECT q.* FROM Questions q " +
                      "INNER JOIN Topics ts ON q.Id = ts.QuestionId " +
                      "WHERE ts.Id = @Id;";

            using (var con = _context.CreateConnection())
            {
                return await con.QueryAsync<Problem>(sql, new { id });
            }
        }

        public async Task<Problem> GetQuestionById(int id)
        {
            var sql = "SELECT * FROM Questions WHERE Id = @id";

            using (var con = _context.CreateConnection())
            {
                return await con.QuerySingleOrDefaultAsync<Problem>(sql, new { id });
            }
        }

        public async Task<bool> UpdateQuestion(Problem question)
        {
            var sql = "UPDATE Questions SET Question = @Question, CorrectAnswer = @CorrectAnswer, UpdatedDate = @UpdatedDate WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, new { Id = question.Id, Question = question.Question, CorrectAnswer = question.CorrectAnswer, UpdatedDate = DateTime.Now }) > 0;
            }
        }
        public async Task<bool> DeleteQuestion(int id)
        {
            var sql = "[dbo].[spQuestion_Delete] @id";

            using (var con = _context.CreateConnection())
            {
                int rowAffected = await con.ExecuteAsync(
                    sql,
                    new { id },
                    commandType: CommandType.Text);
                return rowAffected > 0 ? true : false;
            }
        }

        public async Task<bool> ValidateQuestion(QuestionCreationDto question)
        {
            if (question == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(question.Question))
            {
                return false;
            }

            if (string.IsNullOrEmpty(question.CorrectAnswer))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateQuestion(QuestionUpdateDto question)
        {
            if (question == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(question.Question))
            {
                return false;
            }

            if (string.IsNullOrEmpty(question.CorrectAnswer))
            {
                return false;
            }

            return true;
        }

    }
}
