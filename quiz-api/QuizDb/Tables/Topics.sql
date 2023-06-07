CREATE TABLE [dbo].[Topics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedDate] DATETIME NULL,
    [NumberOfQuestions] INT NULL DEFAULT 0 ,
    [QuestionId] NVARCHAR(50) NULL DEFAULT null, 
);
