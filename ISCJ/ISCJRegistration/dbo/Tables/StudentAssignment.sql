CREATE TABLE [dbo].[StudentAssignment] (
    [StudentId]           UNIQUEIDENTIFIER NOT NULL,
    [AssignmentId]        UNIQUEIDENTIFIER NOT NULL,
    [AssignmentCompleted] TINYINT          NOT NULL,
    [AssignmentDueDate]   DATE             NOT NULL,
    [AssignmentGrade]     FLOAT (53)       NULL,
    [CreateDate]          DATETIME         NOT NULL,
    [CreateUser]          UNIQUEIDENTIFIER NOT NULL,
    [ModifiedDate]        DATETIME         NULL,
    [ModifiedUser]        UNIQUEIDENTIFIER NULL,
    [RowVersion]          ROWVERSION       NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);

