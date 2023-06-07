using Dapper;
using QuizApi.Context;
using QuizApi.Dtos.Topic;
using QuizApi.Models;
using System.Data;

namespace QuizApi.Repositories.Topics
{
#pragma warning disable
    public class TopicRepository : ITopicRepository
    {
        /// <summary>
        /// Stores db server connection string initialized in the constructor to map objects
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor where _context is initialized with db server connection string
        /// </summary>
        public TopicRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTopic(Topic topic)
        {
            var sql = "INSERT INTO Topics (Name, CreatedDate, UpdatedDate, QuestionId, NumberOfQuestions) " +
                        "VALUES (@Name, GETDATE(), NULL, @QuestionId, 0);" +
                        "UPDATE Topics SET NumberOfQuestions = (SELECT COUNT(*) FROM Questions " +
                        "WHERE Questions.Id IN (SELECT value FROM STRING_SPLIT(Topics.QuestionId, ','))) " +
                        "WHERE Topics.Name = @Name;" +
                        "SELECT SCOPE_IDENTITY();";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { topic.Name, QuestionId = string.Join(",", topic.QuestionId) });
            }
        }

        public async Task<IEnumerable<Topic>> GetAllTopics()
        {
            var sql = @"SELECT t.* , q.* FROM Topics t
                LEFT JOIN Questions q ON q.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(t.QuestionId, ','))";

            using (var connection = _context.CreateConnection())
            {
                var lookup = new Dictionary<int, Topic>();

                await connection.QueryAsync<Topic, Problem, Topic>(
                    sql,
                    (topic, question) =>
                    {
                        if (!lookup.TryGetValue(topic.Id, out var topicEntry))
                        {
                            topicEntry = topic;
                            topicEntry.Questions = new List<Problem>();
                            lookup.Add(topicEntry.Id, topicEntry);
                        }
                        topicEntry.Questions.Add(question);
                        return topicEntry;
                    },
                    splitOn: "QuestionId"); 

                return lookup.Values;
            }
        }

        public async Task<Topic?> GetTopic(int id)
        {
            var sql = @"SELECT t.*, q.*
                FROM Topics t
                LEFT JOIN Questions q ON q.Id IN (SELECT CAST(value AS int) FROM STRING_SPLIT(t.QuestionId, ','))
                WHERE t.Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var lookup = new Dictionary<int, Topic>();

                await connection.QueryAsync<Topic, Problem, Topic>(
                    sql,
                    (topic, question) =>
                    {
                        if (!lookup.TryGetValue(topic.Id, out var topicEntry))
                        {
                            topicEntry = topic;
                            topicEntry.Questions = new List<Problem>();
                            lookup.Add(topicEntry.Id, topicEntry);
                        }
                        topicEntry.Questions.Add(question);

                        return topicEntry;
                    },
                    new { id },
                    splitOn: "QuestionId");

                return lookup.Values.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateTopic(Topic topic)
        {
            var sql = "UPDATE Topics SET Name = @Name, " +
                    "UpdatedDate = GETDATE() , " +
                    "QuestionId = @QuestionId , " +
                    "NumberOfQuestions = (SELECT COUNT(*) FROM Questions " +
                    "WHERE Questions.Id IN (SELECT value FROM STRING_SPLIT(Topics.QuestionId, ','))) " +
                    "WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(sql, new { Id = topic.Id, Name = topic.Name , QuestionId = string.Join(",", topic.QuestionId) });
                return rowsAffected != 0;
            }
        }

        public async Task<bool> DeleteTopic(int id)
        {
            var sql = "[dbo].[spTopic_DeleteTopic] @id; ";

            using (var connection = _context.CreateConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(
                    sql,
                    new { id },
                    commandType: CommandType.Text);
                return rowsAffected != 0;
            }
        }

        public async Task<bool> ValidateTopic(TopicCreationDto topic)
        {
            if (topic == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(topic.Name))
            {
                return false;
            }


            if (topic.QuestionId == null || !topic.QuestionId.Any())
            {
                return false;
            }

            return true;
        }

        private static Topic MapTopicQuestions(Topic topic, Problem question)
        {
            topic.Questions.Add(question);
            return topic;
        }
    }
}
