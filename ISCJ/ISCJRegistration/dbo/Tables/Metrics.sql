CREATE TABLE [dbo].[Metrics]
(
	[MetricId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [MetricName] VARCHAR(50) NOT NULL, 
    [MetricDescription] VARCHAR(200) NOT NULL, 
    [MetricValueDefintion] VARCHAR(500) NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [RowVersion] TIMESTAMP NULL
)
