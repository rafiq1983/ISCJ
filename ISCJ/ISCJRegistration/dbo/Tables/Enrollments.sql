CREATE TABLE [dbo].[Enrollments] (
    [EnrollmentId]              UNIQUEIDENTIFIER NOT NULL,
    [FatherId]                  UNIQUEIDENTIFIER NOT NULL,
    [MotherId]                  UNIQUEIDENTIFIER NOT NULL,
    [IslamicSchoolGradeId]      VARCHAR (10)     NULL,
    [PublicSchoolGradeId]       VARCHAR (10)     NULL,
    [StudentContactId]          UNIQUEIDENTIFIER NOT NULL,
    [ProgramId]                 UNIQUEIDENTIFIER NOT NULL,
    [RegistrationApplicationId] UNIQUEIDENTIFIER NULL,
    [CreateDate]                DATETIME         NOT NULL,
    [CreateUser]                VARCHAR (50)     NOT NULL,
    [ModifiedDate]              DATETIME         NULL,
    [ModifiedUser]              VARCHAR (50)     NULL,
    [RowVersion]                ROWVERSION       NOT NULL,
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED ([EnrollmentId] ASC),
    CONSTRAINT [IX_Enrollments_1] UNIQUE NONCLUSTERED ([StudentContactId] ASC, [ProgramId] ASC)
);

