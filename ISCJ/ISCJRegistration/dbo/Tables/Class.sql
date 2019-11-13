CREATE TABLE [dbo].[Class] (
    [ClassId]      UNIQUEIDENTIFIER NOT NULL,
    [ProgramId]    UNIQUEIDENTIFIER NOT NULL,
    [SubjectId]    UNIQUEIDENTIFIER NOT NULL,
    [InstructorId] UNIQUEIDENTIFIER NOT NULL,
    [ScheduleId]   UNIQUEIDENTIFIER NOT NULL,
    [RoomId]       INT              NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [ModifiedDate] DATETIME         NULL,
    CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED ([ClassId] ASC),
    CONSTRAINT [FK_Class_ClassSchedule] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[ClassSchedule] ([scheduleId]),
    CONSTRAINT [FK_Class_Programs] FOREIGN KEY ([ProgramId]) REFERENCES [dbo].[Programs] ([ProgramId]),
    CONSTRAINT [FK_Class_Subjects] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([SubjectId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Class]
    ON [dbo].[Class]([ClassId] ASC);

