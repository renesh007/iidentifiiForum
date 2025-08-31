CREATE TABLE [dbo].[tb_PostTag]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PostId] UNIQUEIDENTIFIER NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [TagId] INT NOT NULL, 
    CONSTRAINT [FK_tb_PostTag_tb_User] FOREIGN KEY ([UserId]) REFERENCES [tb_User]([Id]),
    CONSTRAINT [FK_tb_PostTag_tb_Post] FOREIGN KEY ([PostId]) REFERENCES [tb_Post]([Id]),
    CONSTRAINT [FK_tb_PostTag_tb_Tag] FOREIGN KEY ([TagId]) REFERENCES [tb_Tag]([Id])
)
