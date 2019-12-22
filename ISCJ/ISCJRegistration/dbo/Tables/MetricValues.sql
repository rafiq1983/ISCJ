CREATE TABLE [dbo].[MetricValues]
(
	[MetricValueId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [MetricId] UNIQUEIDENTIFIER NOT NULL, 
	[Data] VARCHAR(500) NOT NULL,
    [MetricPointerId] UNIQUEIDENTIFIER NOT NULL, 
    [MetricPointerType] VARCHAR(50) NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [RowVersion] TIMESTAMP NOT NULL
    
)
