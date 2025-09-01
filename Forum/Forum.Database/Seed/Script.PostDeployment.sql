/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS (SELECT 1 FROM dbo.tb_UserType)
BEGIN
    INSERT INTO dbo.tb_UserType (Id, Description)
    VALUES 
        (1, N'Admin'),
        (2, N'Moderator'),
        (3, N'User');
END;
GO

-- Seed Admin user
IF NOT EXISTS (SELECT 1 FROM dbo.tb_User WHERE UserTypeId = 1)
BEGIN
    INSERT INTO dbo.tb_User (Id, Name, PasswordHash, Email, UserTypeId)
    VALUES (
        '11111111-1111-1111-1111-111111111111',
        'admin',
        'AQAAAAIAAYagAAAAENDdQbb6BsXH4HvZNvO500loWkz98LLuqdxDanATSrsLH3IjuU+zH4Vi8Q/tTUYcRQ==',
        'admin@example.com',
        1
    );
END;
GO

-- Seed Moderator user
IF NOT EXISTS (SELECT 1 FROM dbo.tb_User WHERE UserTypeId = 2)
BEGIN
    INSERT INTO dbo.tb_User (Id, Name, PasswordHash, Email, UserTypeId)
    VALUES (
        '22222222-2222-2222-2222-222222222222',
        'mod',
         'AQAAAAIAAYagAAAAEFdmOW8OLF4fxQ4ksVgs23rgSkGRtRptc+3a3hsH2xfaV8gJ7ysGkohFZcoV45WL6A==',
        'moderator@example.com',
        2
    );
END;
GO

-- Seed regular User
IF NOT EXISTS (SELECT 1 FROM dbo.tb_User WHERE UserTypeId = 3)
BEGIN
    INSERT INTO dbo.tb_User (Id, Name, PasswordHash, Email, UserTypeId)
    VALUES (
        '33333333-3333-3333-3333-333333333333',
        'user',
        'AQAAAAIAAYagAAAAEHwKl03Fp4x7jPh/Mrb0hdDbAxVkok0cnX3kmrQTe3ZJuj9T7Wf3/X3eu8KqbzacQA==',
        'user@example.com',
        3
    );
END;
GO

BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[tb_Tag] WHERE [Name] = 'Misleading')
    BEGIN
        INSERT INTO [dbo].[tb_Tag] ([Name], [Description])
        VALUES ('Misleading', 'Posts that contain information that is misleading or deceptive.');
    END;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[tb_Tag] WHERE [Name] = 'False Information')
    BEGIN
        INSERT INTO [dbo].[tb_Tag] ([Name], [Description])
        VALUES ('False Information', 'Posts that contain information proven to be false.');
    END;
END;
GO

-- Seed Post: Welcome Post
IF NOT EXISTS (SELECT 1 FROM dbo.tb_Post WHERE Id = '44444444-4444-4444-4444-444444444444')
BEGIN
    INSERT INTO dbo.tb_Post (Id, Title, Content, UserId, CreatedOn)
    VALUES (
        '44444444-4444-4444-4444-444444444444',
        'Welcome Post',
        'This is the first post in the system. Welcome to our platform!',
        '11111111-1111-1111-1111-111111111111', -- Admin User
        SYSUTCDATETIME()
    );
END;
GO

-- Seed Post: Moderator Guidelines
IF NOT EXISTS (SELECT 1 FROM dbo.tb_Post WHERE Id = '55555555-5555-5555-5555-555555555555')
BEGIN
    INSERT INTO dbo.tb_Post (Id, Title, Content, UserId, CreatedOn)
    VALUES (
        '55555555-5555-5555-5555-555555555555',
        'Moderator Guidelines',
        'This post outlines the guidelines for moderators to follow.',
        '22222222-2222-2222-2222-222222222222', -- Moderator User
        SYSUTCDATETIME()
    );
END;
GO

-- Seed Post: User Tips
IF NOT EXISTS (SELECT 1 FROM dbo.tb_Post WHERE Id = '66666666-6666-6666-6666-666666666666')
BEGIN
    INSERT INTO dbo.tb_Post (Id, Title, Content, UserId, CreatedOn)
    VALUES (
        '66666666-6666-6666-6666-666666666666',
        'User Tips',
        'Some helpful tips for regular users on how to navigate the platform.',
        '33333333-3333-3333-3333-333333333333', -- Regular User
        SYSUTCDATETIME()
    );
END;
GO