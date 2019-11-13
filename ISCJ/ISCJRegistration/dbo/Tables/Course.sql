CREATE TABLE [dbo].[Course] (
    [CourseId]          UNIQUEIDENTIFIER NOT NULL,
    [CourseDescription] VARCHAR (500)    NOT NULL,
    [CreateDate]        DATETIME         NOT NULL,
    [CreateUser]        VARCHAR (50)     NOT NULL,
    [ModifiedDate]      DATETIME         NULL,
    [ModifiedUser]      VARBINARY (50)   NULL,
    [RowVersion]        ROWVERSION       NOT NULL
);

