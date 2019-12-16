CREATE TABLE [dbo].[ContactTenants]
(
	[ContactId] UNIQUEIDENTIFIER NOT NULL , 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 	
    [AssociationName] VARCHAR(50) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [rowVersion] TIMESTAMP NOT NULL, 
    PRIMARY KEY ([ContactId], [TenantId]) 
)
