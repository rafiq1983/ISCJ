CREATE TABLE [dbo].[CodeCategoryMapping]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TenantId] UNIQUEIDENTIFIER NULL, 
    [ParentCategory] VARCHAR(50) NOT NULL, 
    [ParentCode] VARCHAR(50) NOT NULL, 
    [ChildCategory] VARCHAR(50) NOT NULL, 
    [ChildCode] NCHAR(10) NOT NULL
)
