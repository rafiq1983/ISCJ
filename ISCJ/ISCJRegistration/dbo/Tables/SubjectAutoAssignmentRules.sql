CREATE TABLE [dbo].[SubjectAutoAssignmentRules] (
    [WhenProgramId]          UNIQUEIDENTIFIER NOT NULL,
    [WhenIslamicSchoolGrade] VARCHAR (50)     NOT NULL,
    [ThenSubjectId]          UNIQUEIDENTIFIER NOT NULL,
    [CreateUser]             VARCHAR (50)     NOT NULL,
    [CreateDate]             DATETIME         NOT NULL,
    [ModifiedDate]           DATETIME         NULL,
    [ModifiedUser]           VARCHAR (50)     NULL,
    [RowVersion]             ROWVERSION       NOT NULL,
    [TenantId]               UNIQUEIDENTIFIER NULL
);

