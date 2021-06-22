CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Passwd] NVARCHAR(50) NOT NULL 
)
