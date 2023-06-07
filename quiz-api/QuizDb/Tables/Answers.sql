CREATE TABLE [dbo].[Answers]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TakerId] INT NOT NULL, 
    [QuizId] INT NOT NULL,
    [QuestionId] INT NOT NULL, 
    [Answer] NVARCHAR(50) NOT NULL,
    CONSTRAINT [FK_Answer_Taker] FOREIGN KEY ([TakerId]) REFERENCES [Takers]([Id]),
    CONSTRAINT [FK_Answer_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([Id]) ON DELETE CASCADE
)
