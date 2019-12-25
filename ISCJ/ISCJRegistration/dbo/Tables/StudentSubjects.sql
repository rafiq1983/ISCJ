CREATE TABLE [dbo].[StudentSubjects]
(

    [RecordId] UNIQUEIDENTIFIER NOT NULL, 
	[StudentId] UNIQUEIDENTIFIER NOT NULL , 
    [EnrollmentId] UNIQUEIDENTIFIER NOT NULL, 
    [SubjectId] UNIQUEIDENTIFIER NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL, 	
    [ProgramId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    [rowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_StudentSubjects] PRIMARY KEY ([RecordId]) ,
	CONSTRAINT [Unique_Student_Subject_Program]  UNIQUE (SubjectId, ProgramId, StudentId)
)
