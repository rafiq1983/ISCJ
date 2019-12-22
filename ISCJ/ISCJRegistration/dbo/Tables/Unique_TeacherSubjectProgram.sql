ALTER TABLE [dbo].[TeacherSubjectAssignment]
	ADD CONSTRAINT [Unique_TeacherSubjectProgram]
	UNIQUE (TeacherId, ProgramId, SubjectId)
GO
ALTER TABLE [dbo].[TeacherSubjectAssignment]
	ADD CONSTRAINT [Unique_TeacherSubjectAssignment_ProgramSubject]
	UNIQUE (ProgramId, SubjectId)
GO