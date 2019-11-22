CREATE TABLE [dbo].[Users] (
    [UserId]                       UNIQUEIDENTIFIER NOT NULL,
    [UserName]                     VARCHAR (50)     NOT NULL,
    [Password]                     VARCHAR (500)     NOT NULL,
    [IsEncrypted]                  TINYINT          NOT NULL,
    [IsAccountLocked]              TINYINT          NOT NULL,
    [RequirePasswordChangeAtLogin] TINYINT          NOT NULL,
    [ContactId]                    UNIQUEIDENTIFIER NOT NULL,
    [LastLoginIP]                  VARCHAR (50)     NOT NULL,
    [LastLoginDate]                DATETIME         NULL,
    [CreateDate]                   DATETIME         NOT NULL,
    [ModifiedDate]                 DATETIME         NULL,
    [CreateUser]                   VARCHAR (50)     NOT NULL,
    [ModifiedUser]                 VARCHAR (50)     NULL,
    [RowVersion]                   ROWVERSION       NOT NULL,
    [AuthenticationSource] VARCHAR(20) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_User]
    ON [dbo].[Users]([UserId] ASC);

