CREATE PROCEDURE [dbo].[spQuiz_GetQuizByIdWithQuizResults]
	@Id INT
AS
BEGIN
	SELECT 
		q.Id, q.Name, q.Description, qr.Id, qr.TakerName, qr.Score, qr.Evaluation		
	FROM 
		Quizzes q
    INNER JOIN 
		QuizResults qr
	ON 
		qr.QuizId = q.Id
    WHERE 
		q.Id = @Id;
END
