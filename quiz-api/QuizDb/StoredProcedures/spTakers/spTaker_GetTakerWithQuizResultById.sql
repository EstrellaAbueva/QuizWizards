CREATE PROCEDURE [dbo].[spTaker_GetTakerWithQuizResultById]
	@Id INT
AS
BEGIN
	SELECT
    t.Id, t.Name, t.Address, t.Email, q.Id, q.Name, q.Description, qr.Id, qr.QuizName, qr.TakerName, qr.Score, qr.Evaluation 
	FROM 
		Takers t 
	INNER JOIN 
		TakerQuiz tq 
	ON 
		tq.TakerId = t.Id
	INNER JOIN 
		Quizzes q 
	ON 
		q.Id = tq.QuizId
	INNER JOIN 
		QuizResults qr 
	ON 
		qr.TakerId = t.Id AND qr.QuizId = q.Id 
	WHERE  
		t.Id = @Id;
END
