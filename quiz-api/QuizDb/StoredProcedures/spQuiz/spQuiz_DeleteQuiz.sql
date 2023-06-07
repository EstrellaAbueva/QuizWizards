CREATE PROCEDURE [dbo].[spQuiz_DeleteQuiz]
	@Id INT
AS
BEGIN
    DELETE FROM Quizzes WHERE Id = @id;
END