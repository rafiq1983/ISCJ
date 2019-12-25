CREATE TABLE [dbo].[Subjects] (
    [SubjectId]          UNIQUEIDENTIFIER NOT NULL,
    [SubjectName]        VARCHAR (100)    NOT NULL,
    [SubjectDescription] VARCHAR (500)    NOT NULL,
    [TenantId]           UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [CreateUser]         VARCHAR (50)     NOT NULL,
    [ModifiedDate]       DATETIME         NULL,
    [ModifiedUser]       VARCHAR (50)     NULL,
    [RowVersion]         ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED ([SubjectId] ASC),
    CONSTRAINT [IX_Subjects] UNIQUE NONCLUSTERED ([SubjectName] ASC, [TenantId] ASC)
);

