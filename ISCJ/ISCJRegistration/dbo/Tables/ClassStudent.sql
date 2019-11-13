CREATE TABLE [dbo].[ClassStudent] (
    [ClassId]      UNIQUEIDENTIFIER NOT NULL,
    [StudentId]    UNIQUEIDENTIFIER NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [StudentGrade] CHAR (1)         NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_ClassStudent] PRIMARY KEY CLUSTERED ([ClassId] ASC, [StudentId] ASC)
);

