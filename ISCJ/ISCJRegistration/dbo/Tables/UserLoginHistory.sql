CREATE TABLE [dbo].[UserLoginHistory] (
    [SessionId]  UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [LoginIP]    VARCHAR (50)     NOT NULL,
    [LoginDate]  DATETIME         NOT NULL,
    [DeviceType] VARCHAR (100)    NOT NULL,
    [CreateDate] DATETIME         NOT NULL,
    [CreateUser] UNIQUEIDENTIFIER NOT NULL, 
    [TenantId] UNIQUEIDENTIFIER NOT NULL
);

