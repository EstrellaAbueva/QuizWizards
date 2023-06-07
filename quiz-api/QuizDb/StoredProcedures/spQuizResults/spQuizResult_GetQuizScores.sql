CREATE PROCEDURE [dbo].[spQuizResult_GetQuizScores]
	@quizName nvarchar(50),
	@takerName nvarchar(50)
AS
BEGIN
	SELECT 
		COUNT(*) 
	FROM 
		TakersAnswers ta 
	INNER JOIN 
		Takers t 
	ON 
		ta.TakerId = t.Id 
	INNER JOIN 
		Quizzes q 
	ON 
		ta.QuizId = q.Id 
	WHERE 
		q.Name = @quizName 
		AND t.Name = @takerName 
		AND ta.Status = 'Correct';
END

