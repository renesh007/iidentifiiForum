CREATE TABLE [dbo].[tb_Like]
(
	[Id] INT IDENTITY(1,1) NOT NULL , 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [PostId] UNIQUEIDENTIFIER NOT NULL,
    
    CONSTRAINT [PK_Like] PRIMARY KEY (Id),
    CONSTRAINT [FK_tb_Like_tb_User] FOREIGN KEY ([UserId]) REFERENCES [tb_User]([Id]), 
    CONSTRAINT [FK_tb_Like_tb_Post] FOREIGN KEY ([PostId]) REFERENCES [tb_Post]([Id])
)
