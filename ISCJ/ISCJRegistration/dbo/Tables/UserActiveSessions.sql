CREATE TABLE [dbo].[UserActiveSessions] (
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [SessionId]      UNIQUEIDENTIFIER NOT NULL,
    [DeviceType]     VARCHAR (100)    NOT NULL,
    [IPAddress]      VARCHAR (50)     NOT NULL,
    [IsActive]       TINYINT          NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastActionDate] DATETIME         NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);

