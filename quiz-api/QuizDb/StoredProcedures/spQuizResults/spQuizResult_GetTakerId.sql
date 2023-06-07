CREATE PROCEDURE [dbo].[spQuizResult_GetTakerId]
	@quizName nvarchar(50),
	@takerName nvarchar(50)
AS
BEGIN
	SELECT DISTINCT 
		ta.TakerId
	From 
		TakersAnswers ta 
	INNER JOIN 
		Quizzes q 
	ON 
		q.Id = ta.QuizId 
	INNER JOIN 
		Takers t 
	ON 
		t.Id = ta.TakerId
	WHERE 
		q.Name = @quizName AND 
		t.Name = @takerName;
END
