USE [QuizDb]
GO

SET IDENTITY_INSERT [dbo].[Takers] ON
INSERT [dbo].[Takers] ([Id], [Name], [Address], [Email], [Username], [Password], [TakerType], [CreatedDate], [UpdatedDate]) 
VALUES 
    (1, 'John Doe', 'N. Bacalso Ave, Cebu City', 'johndoe@gmail.com', 'john2', 'john123', 'T', 10-10-10, NULL),
    (2, 'Jane Doe', 'Somewhere bukid, Cebu City', 'janedoe@gmail.com', 'janeeyyy', 'jane2', 'T', 10-10-10, NULL),
    (3, 'Stephine Doe', 'Cebu City', 'stephine.doe@gmail.com', 'yoroichii', 'sinoynaneng', 'T', 10-10-10, NULL),
    (4, 'Estrella Abueva', 'Carlock', 'estrellaabueva@gmail.com', 'ahoka', 'estrellaAbueva', 'A', 10-10-10, NULL),
    (5, 'admin', 'nowWhere', 'admin@gmail.com', 'admin', '123', 'A', 10-10-10, NULL);


SET IDENTITY_INSERT [dbo].[Takers] OFF

SET IDENTITY_INSERT [dbo].[Quizzes] ON
INSERT [dbo].[Quizzes] ([Id], [Name], [Description], [CreatedDate] , [UpdatedDate] , [MaxScore] , [TopicId] )
VALUES
    (1, 'Math Quiz', 'Calculus quiz', '2022-10-10', NULL, 3 , '1'),
    (2, 'Science Quiz', 'Quiz on life of animals', '2022-10-10', NULL, 2 , '3'),
    (3, 'History Quiz', 'Quiz on the life of PH heroes', '2022-10-10', NULL, 2 , '5'),
    (4, 'Araling Panlipunan Quiz', 'Araling Panlipunan quiz', '2022-10-10', NULL , 3 , '7');


SET IDENTITY_INSERT [dbo].[Quizzes] OFF

SET IDENTITY_INSERT [dbo].[TakerQuiz] ON
INSERT [dbo].[TakerQuiz] ([Id], [TakerId], [QuizId], [AssignedDate], [Score], [TakenDate], [FinishedDate], [CanRetake])
VALUES
    (1, 1, 1, 10-10-10 , 3, NULL, NULL, 0),
    (2, 2, 2, 10-10-10 , 2, NULL, NULL, 0),
    (3, 3, 3, 10-10-10 , 1, NULL, NULL, 0),
    (4, 4, 4, 10-10-10 , 3, NULL, NULL, 0);

SET IDENTITY_INSERT [dbo].[TakerQuiz] OFF

SET IDENTITY_INSERT [dbo].[Questions] ON
INSERT [dbo].[Questions] ([Id], [Question], [CorrectAnswer], [CreatedDate], [UpdatedDate])
VALUES
    (1, '1+1=3', 'False', '2022-10-10', NULL),
    (2, '200+1=201', 'True', '2022-10-10', NULL),
    (3, '∫4x6−2x3+7x−4dx', '2x', '2022-10-10', NULL),
    (4, 'The king of the jungle is the lion', 'True', '2022-10-10', NULL),
    (5, 'Dragonflies, stink bugs, and ladybugs are what?', 'insects', '2022-10-10', NULL),
    (6, 'River is a habitat of a snake', 'True', '2022-10-10', NULL),
    (7, 'Rizal has 100 wives', 'False', '2022-10-10', NULL),
    (8, 'What weapon did Rizal use to fight for freedom?', 'pen and paper', '2022-10-10', NULL),
    (9, 'Photosynthesis is the process of converting', 'light energy into chemical energy', '2022-10-10', NULL),
    (10, 'Who discovered electricity?', 'Benjamin Franklin', '2022-10-10', NULL),
    (11, 'What is the square root of 144?', '12', '2022-10-10', NULL),
    (12, 'What is the chemical symbol for gold?', 'Au', '2022-10-10', NULL),
    (13, 'What is the capital of Australia?', 'Canberra', '2022-10-10', NULL),
    (14, 'Name one programming languages', 'C#', '2022-10-10', NULL),
    (15, 'Who wrote the novel "Pride and Prejudice"?', 'Jane Austen', '2022-10-10', NULL);

SET IDENTITY_INSERT [dbo].[Questions] OFF


SET IDENTITY_INSERT [dbo].[Topics] ON
INSERT [dbo].[Topics] ([Id], [Name], [CreatedDate] , [UpdatedDate] , [NumberOfQuestions] , [QuestionId]  )
VALUES
    (1, 'Differential', 10-10-10 , NULL  , '3' , '1, 2, 3'),
    (2, 'Integral', 10-10-10 , NULL  , '4' , '1, 2, 3, 11'),
    (3, 'Animal Kingdom', 10-10-10, NULL  , '3' , '4, 5, 6'),
    (4, 'Reptile Habitats', 10-10-10, NULL  , '2' , '5, 6'),
    (5, 'Life of Rizal', 10-10-10, NULL  , '2' , '7, 8'),
    (6, 'Chemistry', 2022-10-10, NULL , '1' , '10'),
    (7, 'Araling Panlipunan', 2022-10-10, NULL, '3' , '7, 8, 15');

SET IDENTITY_INSERT [dbo].[Topics] OFF

SET IDENTITY_INSERT [dbo].[Answers] ON

INSERT [dbo].[Answers] ([Id] , [TakerId] , [QuizId], [QuestionId], [Answer]) VALUES
    (1 , 1 , 1, 1, 'False'),
    (2 , 1 , 1, 3, '2x'),
    (3 , 2 , 2, 4, 'False'),
    (4 , 2 , 2, 6, 'True'),
	(5 , 2 , 2, 5, 'insects'),
	(6 , 1 , 1, 2, 'True'),
	(7 , 4 , 4, 7, 'False'),
	(8 , 4 , 4, 15, 'Jane Austen'),
    (9 , 4 , 4, 8, 'pen and paper'),
    (10, 3 , 3, 7, 'True'),
    (11, 3 , 3, 8, 'pen and paper');


SET IDENTITY_INSERT [dbo].[Answers] OFF
