CREATE TABLE [dbo].[UserTenants]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [RoleCd] VARCHAR(20) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    PRIMARY KEY ([UserId], [TenantId])
)
