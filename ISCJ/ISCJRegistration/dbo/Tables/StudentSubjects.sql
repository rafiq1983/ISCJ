CREATE TABLE [dbo].[StudentSubjects]
(
	[StudentId] UNIQUEIDENTIFIER NOT NULL , 
    [EnrollmentId] UNIQUEIDENTIFIER NOT NULL, 
    [SubjectId] UNIQUEIDENTIFIER NOT NULL, 
	[ProgramId] UNIQUEIDENTIFIER NOT NULL,
    [SubjectGrade] CHAR NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [CreateUser] VARCHAR(50) NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedUser] VARCHAR(50) NULL, 
    CONSTRAINT [PK_StudentSubjects] PRIMARY KEY ([StudentId], [SubjectId], [EnrollmentId]) 
)
