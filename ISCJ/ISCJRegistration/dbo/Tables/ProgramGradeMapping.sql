CREATE TABLE [dbo].[ProgramGradeMapping] (
    [ProgramId]    UNIQUEIDENTIFIER NOT NULL,
    [GradeId]      VARBINARY (50)   NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARBINARY (50)   NULL,
    [RowVersion]   ROWVERSION       NOT NULL
);

