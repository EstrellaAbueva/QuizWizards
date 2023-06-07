CREATE PROCEDURE [dbo].[spTaker_DeleteTakerById]
	@Id INT
AS
BEGIN
    DELETE FROM Answers WHERE TakerId = @Id
    DELETE FROM TakerQuiz WHERE TakerId = @Id
    DELETE FROM Takers WHERE Id = @Id;
END