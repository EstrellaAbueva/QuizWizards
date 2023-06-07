CREATE TABLE [dbo].[Questions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [CorrectAnswer] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedDate] DATETIME NULL
);
