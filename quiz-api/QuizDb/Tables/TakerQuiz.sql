CREATE TABLE [dbo].[TakerQuiz]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TakerId] INT NOT NULL, 
    [QuizId] INT NOT NULL FOREIGN KEY REFERENCES Quizzes(Id) ON DELETE CASCADE, 
    [AssignedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [Score] INT NULL, 
    [TakenDate] DATETIME NULL, 
    [FinishedDate] DATETIME NULL, 
    [CanRetake] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_TakerQuiz_ToTable] FOREIGN KEY ([TakerId]) REFERENCES [Takers]([Id]), 
    CONSTRAINT [FK_TakerQuiz_ToTable_1] FOREIGN KEY ([QuizId]) REFERENCES [Quizzes]([Id])
)
