﻿CREATE TABLE [dbo].[StudentMetrics]
(
	[StudentId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [EnrollmentId] UNIQUEIDENTIFIER NOT NULL, 
    [MetricId] UNIQUEIDENTIFIER NOT NULL, 
    [MetricValue] VARCHAR(50) NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] UNIQUEIDENTIFIER NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [RowVersion] TIMESTAMP NOT NULL
)
