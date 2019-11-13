CREATE TABLE [dbo].[ClassSessionAttendance] (
    [AttendanceId]   UNIQUEIDENTIFIER NOT NULL,
    [StudentId]      UNIQUEIDENTIFIER NOT NULL,
    [ClassSessionId] UNIQUEIDENTIFIER NOT NULL,
    [ClassId]        UNIQUEIDENTIFIER NOT NULL,
    [Attendance]     VARCHAR (50)     NOT NULL,
    [TenantId]       UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [CreateUser]     VARCHAR (50)     NOT NULL,
    [ModifiedDate]   DATETIME         NULL,
    [ModifiedUser]   VARCHAR (50)     NULL,
    [RowVersion]     ROWVERSION       NOT NULL,
    CONSTRAINT [PK_ClassSessionAttendance] PRIMARY KEY CLUSTERED ([AttendanceId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ClassSessionAttendance]
    ON [dbo].[ClassSessionAttendance]([StudentId] ASC, [ClassSessionId] ASC);

