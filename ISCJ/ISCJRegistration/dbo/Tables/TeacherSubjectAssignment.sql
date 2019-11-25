CREATE TABLE [dbo].[TeacherSubjectAssignment]
(
	[RecordId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TeacherId] UNIQUEIDENTIFIER NOT NULL, 
    [SubjectId] UNIQUEIDENTIFIER NOT NULL, 
    [ProgramId] UNIQUEIDENTIFIER NOT NULL, 
	 [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
   
)
