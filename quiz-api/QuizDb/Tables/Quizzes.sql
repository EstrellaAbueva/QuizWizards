CREATE TABLE [dbo].[Quizzes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedDate] DATETIME NULL, 
        [MaxScore] INT NULL DEFAULT 0, 
    [TopicId] NVARCHAR(50) NULL DEFAULT null, 

)
