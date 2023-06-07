CREATE TABLE [dbo].[Takers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [TakerType] NVARCHAR(1) NOT NULL DEFAULT 'T', 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedDate] DATETIME NULL , 
    UNIQUE([Username])
)
