CREATE TABLE [dbo].[StudentAttendance] (
    [StudentId]    UNIQUEIDENTIFIER NOT NULL,
    [ProgramId]    UNIQUEIDENTIFIER NOT NULL,
    [CheckInDate]  DATE             NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   UNIQUEIDENTIFIER NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] UNIQUEIDENTIFIER NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_StudentCheckInHistory] PRIMARY KEY CLUSTERED ([StudentId] ASC, [ProgramId] ASC, [CheckInDate] ASC)
);

