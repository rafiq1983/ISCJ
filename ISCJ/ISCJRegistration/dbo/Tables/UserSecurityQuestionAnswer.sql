CREATE TABLE [dbo].[UserSecurityQuestionAnswer]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [QuestionId] UNIQUEIDENTIFIER NOT NULL, 
    [Answer] VARCHAR(50) NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([UserId], [QuestionId])
)
