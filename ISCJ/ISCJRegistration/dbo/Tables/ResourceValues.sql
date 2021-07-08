CREATE TABLE [dbo].[ResourceValues]
(
	[ValueID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ResourceDefinitionId] INT NOT NULL, 
	[Locale] VARCHAR(10) NOT NULL DEFAULT 'neutral',
    [ResourceValue] VARCHAR(MAX) NOT NULL, 
    [OrganizationId] UNIQUEIDENTIFIER NOT NULL, 
	[CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(100) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(100) NULL, 
    CONSTRAINT [AK_ResourceValues_ResourceDefinitionId_Locale] UNIQUE (ResourceDefinitionId,Locale), 
   
)
