CREATE PROCEDURE [dbo].[spTaker_GetTakerAnswerById]
	@Id INT
AS
BEGIN
	SELECT 
	t.Id, t.Name, t.Address, t.Email, qa.Id, qa.Question
	FROM 
		Takers t 
		INNER JOIN 
		Questions qa ON t.Id = qa.Id
	WHERE  
		t.Id = 1;
END
