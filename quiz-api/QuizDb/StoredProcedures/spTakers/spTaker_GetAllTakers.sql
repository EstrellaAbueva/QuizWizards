CREATE PROCEDURE [dbo].[spTaker_GetAllTakers]
AS
BEGIN
	SELECT 
        t.Id, t.Name, t.Address, t.Email, t.Username, t.TakerType, t.CreatedDate, t.UpdatedDate
    FROM 
        Takers t 
    WHERE 
        t.TakerType = 'T';
   
END
