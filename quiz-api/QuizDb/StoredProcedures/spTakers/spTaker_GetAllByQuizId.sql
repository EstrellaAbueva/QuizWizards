CREATE PROCEDURE [dbo].[spTaker_GetAllByQuizId]
	@quizId INT
AS
BEGIN
    SELECT 
        t.Id, t.Name, t.Address, t.Email, t.Username, q.Id, q.Name, q.Description 
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
    WHERE  
        q.Id = @quizId;
END
