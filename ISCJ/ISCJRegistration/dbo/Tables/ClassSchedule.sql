CREATE TABLE [dbo].[ClassSchedule] (
    [scheduleId]   UNIQUEIDENTIFIER NOT NULL,
    [Frequency]    VARCHAR (25)     NOT NULL,
    [DaysTime]     VARCHAR (500)    NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    CONSTRAINT [PK_ClassSchedule] PRIMARY KEY CLUSTERED ([scheduleId] ASC)
);

