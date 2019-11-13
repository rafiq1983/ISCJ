CREATE TABLE [dbo].[Assignment] (
    [AssignmentId]    UNIQUEIDENTIFIER NOT NULL,
    [SubjectId]       UNIQUEIDENTIFIER NULL,
    [AssignementType] VARCHAR (50)     NOT NULL,
    [ProgramId]       UNIQUEIDENTIFIER NOT NULL,
    [Description]     VARCHAR (500)    NULL,
    [CreateDate]      DATETIME         NOT NULL,
    [CreateUser]      UNIQUEIDENTIFIER NOT NULL,
    [ModifiedDate]    DATETIME         NULL,
    [ModifiedUser]    UNIQUEIDENTIFIER NULL,
    [RowVersion]      ROWVERSION       NOT NULL
);

