ALTER TABLE [dbo].[StudentSubjects]
	ADD CONSTRAINT [UniqueKey_SubjectProgram]
	UNIQUE (SubjectId, ProgramId)
