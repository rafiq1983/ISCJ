﻿CREATE TABLE [dbo].[ClassSession] (
    [ClassSessionId] UNIQUEIDENTIFIER NOT NULL,
    [ClassId]        UNIQUEIDENTIFIER NOT NULL,
    [InstructorId]   UNIQUEIDENTIFIER NOT NULL,
	[SessionDate] DATETIME NOT NULL,
    [CreateUser]     VARCHAR (50)     NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [ModifiedUser]   VARCHAR (50)     NULL,
    [ModifiedDate]   DATETIME         NULL,
    [RowVersion]     ROWVERSION       NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    
    CONSTRAINT [PK_ClassSession] PRIMARY KEY CLUSTERED ([ClassSessionId] ASC)
);

