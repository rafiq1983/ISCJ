CREATE TABLE [dbo].[UserLoginHistory] (
    [SessionId]  UNIQUEIDENTIFIER NOT NULL,
    [UserId]     VARCHAR(100) NOT NULL,
    [LoginIP]    VARCHAR (50)     NOT NULL,
    [LoginDate]  DATETIME         NOT NULL,
    [DeviceType] VARCHAR (100)    NOT NULL,
    [TenantId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY ([SessionId])
);

