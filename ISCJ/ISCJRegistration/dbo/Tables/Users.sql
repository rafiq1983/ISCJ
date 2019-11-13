CREATE TABLE [dbo].[Users] (
    [UserId]                       UNIQUEIDENTIFIER NOT NULL,
    [UserName]                     VARCHAR (50)     NOT NULL,
    [Password]                     VARCHAR (50)     NOT NULL,
    [IsEncrypted]                  TINYINT          NOT NULL,
    [IsAccountLocked]              TINYINT          NOT NULL,
    [RequirePasswordChangeAtLogin] TINYINT          NOT NULL,
    [ContactId]                    UNIQUEIDENTIFIER NOT NULL,
    [LastLoginIP]                  VARCHAR (50)     NOT NULL,
    [TenantId]                     UNIQUEIDENTIFIER NOT NULL,
    [LastLoginDate]                DATETIME         NULL,
    [CreateDate]                   DATETIME         NOT NULL,
    [ModifiedDate]                 DATETIME         NULL,
    [CreateUser]                   VARCHAR (50)     NOT NULL,
    [ModifiedUser]                 VARCHAR (50)     NULL,
    [RowVersion]                   ROWVERSION       NOT NULL,
    CONSTRAINT [IX_User_1] UNIQUE NONCLUSTERED ([UserName] ASC, [TenantId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_User]
    ON [dbo].[Users]([UserId] ASC);

