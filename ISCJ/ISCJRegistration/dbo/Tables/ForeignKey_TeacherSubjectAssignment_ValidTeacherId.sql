ALTER TABLE [dbo].[TeacherSubjectAssignment]
	ADD CONSTRAINT [TeacherSubjectAssignment_ValidTeacherId]
	FOREIGN KEY (TeacherId)
	REFERENCES [Teachers] (TeacherId)
