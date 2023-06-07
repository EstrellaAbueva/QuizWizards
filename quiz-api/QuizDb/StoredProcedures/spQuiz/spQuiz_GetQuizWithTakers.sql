CREATE PROCEDURE [dbo].[spQuiz_GetQuizWithTakers]
	@Id INT
AS
BEGIN
   SELECT 
        q.Id, q.Name, q.Description, t.Id, t.Name, t.Address, t.Email 
   FROM 
        Quizzes q 
   INNER JOIN 
        TakerQuiz tq ON tq.QuizId = q.Id
   INNER JOIN 
        Takers t ON t.Id = tq.TakerId 
    WHERE 
        q.Id = @Id;
END
