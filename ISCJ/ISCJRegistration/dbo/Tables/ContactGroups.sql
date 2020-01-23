CREATE TABLE [dbo].[ContactGroups]
(
	[GroupId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [GroupName] VARCHAR(100) NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    CONSTRAINT [AK_ContactGroups_Column] UNIQUE ([GroupName], [TenantId])
)

GO
