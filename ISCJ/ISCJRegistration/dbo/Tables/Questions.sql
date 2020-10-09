CREATE TABLE [dbo].[Questions]
(
	[QuestionId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [QuestionText] VARCHAR(255) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL
)
