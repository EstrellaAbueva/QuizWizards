CREATE PROCEDURE [dbo].[spQuiz_GetQuizById]
	@Id INT
AS
BEGIN
	SELECT 
		q.Id, q.Name, q.Description, tp.Id, tp.Name
	FROM 
		Quizzes q, Topics tp 
END

