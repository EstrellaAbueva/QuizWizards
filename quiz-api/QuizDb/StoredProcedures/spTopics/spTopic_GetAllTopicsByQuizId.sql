CREATE PROCEDURE [dbo].[spTopic_GetAllTopicsByQuizId]
	@quizId INT
AS
BEGIN
	SELECT 
		t.Id, t.Name
	FROM 
		Topics t
END