CREATE TABLE [dbo].[SubjectMapping] (
    [SubjectId]            UNIQUEIDENTIFIER NOT NULL,
    [ProgramId]            UNIQUEIDENTIFIER NOT NULL,
    [IslamicSchoolGradeId] VARCHAR (50)     NOT NULL,
    [TenantId]             UNIQUEIDENTIFIER NOT NULL,
    [CreateUser]           VARCHAR (50)     NOT NULL,
    [CreateDate]           DATETIME         NOT NULL,
    [ModifiedDate]         DATETIME         NULL,
    [ModifiedUser]         VARCHAR (50)     NULL,
    [RowVersion]           ROWVERSION       NOT NULL,
    CONSTRAINT [PK_SubjectMapping] PRIMARY KEY CLUSTERED ([SubjectId] ASC, [ProgramId] ASC)
);

