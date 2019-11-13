CREATE TABLE [dbo].[SubjectGradeMapping] (
    [SubjectId]    UNIQUEIDENTIFIER NOT NULL,
    [GradeId]      VARCHAR (50)     NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] NCHAR (10)       NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SubjectGradeMapping] PRIMARY KEY CLUSTERED ([SubjectId] ASC, [GradeId] ASC)
);

