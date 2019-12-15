CREATE TABLE [dbo].[MetricAssociations]
(
	[MetricId] UNIQUEIDENTIFIER NOT NULL , 
    [EntityId] UNIQUEIDENTIFIER NOT NULL, 
    [EntityType] VARCHAR(50) NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [ModifiedDate] DATETIME NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    PRIMARY KEY ([MetricId], [EntityId])
)
