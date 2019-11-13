CREATE TABLE [dbo].[Rooms] (
    [RoomId]       INT              IDENTITY (1, 1) NOT NULL,
    [RoomName]     VARCHAR (75)     NOT NULL,
    [TenantId]     UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]   DATETIME         NOT NULL,
    [CreateUser]   VARCHAR (50)     NOT NULL,
    [ModifiedDate] DATETIME         NULL,
    [ModifiedUser] VARCHAR (50)     NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED ([RoomId] ASC),
    CONSTRAINT [IX_Rooms] UNIQUE NONCLUSTERED ([TenantId] ASC, [RoomName] ASC)
);

