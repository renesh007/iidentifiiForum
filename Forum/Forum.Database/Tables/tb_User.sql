CREATE TABLE [dbo].[tb_User]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Email] VARCHAR(255) NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [UserTypeId] INT NOT NULL,
    
    CONSTRAINT [FK_tb_User_UserType] FOREIGN KEY ([UserTypeId]) REFERENCES [tb_UserType]([Id])
)
